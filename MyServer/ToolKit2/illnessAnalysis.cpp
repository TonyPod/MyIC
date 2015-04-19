#include "illnessAnalysis.h"
#include "SingleTooth.h"

using namespace std;
using namespace cv;

_declspec(dllexport) Illnesses _stdcall analyze(const char *fileName, const char *outputFileName)
{
	bool inner;
	std::vector<int> vec;
	int i, j;
	IplImage *img0 = cvLoadImage(fileName);

	Illnesses *illnesses = (Illnesses *)calloc(1, sizeof(Illnesses));

#ifdef DEBUG
	cvNamedWindow("image", CV_WINDOW_NORMAL);
	cvShowImage("image", img0);
#endif // DEBUG

	/************************************************************************/
	/* 预处理                                                               */
	/************************************************************************/

#pragma region Preprocessing

	//平滑处理
	IplConvKernel *kernel = cvCreateStructuringElementEx(5, 5, 2, 2, CV_SHAPE_ELLIPSE, NULL);
	cvMorphologyEx(img0, img0, NULL, kernel, CV_MOP_OPEN, 1);
	cvMorphologyEx(img0, img0, NULL, kernel, CV_MOP_CLOSE, 1);
	cvReleaseStructuringElement(&kernel);

#ifdef DEBUG
	cvShowImage("image", img0);
	cvWaitKey();
#endif // DEBUG
	//直方图均衡
	//IplImage *b, *g, *r;
	//b = cvCreateImage(cvGetSize(img0), IPL_DEPTH_8U, 1);
	//g = cvCreateImage(cvGetSize(img0), IPL_DEPTH_8U, 1);
	//r = cvCreateImage(cvGetSize(img0), IPL_DEPTH_8U, 1);
	//cvSplit(img0, b, g, r, NULL);
	//cvEqualizeHist(b, b);
	//cvEqualizeHist(g, g);
	//cvEqualizeHist(r, r);
	//cvMerge(b, g, r, NULL, img0);
	//cvShowImage("image", img0);
	//cvWaitKey();

#pragma endregion

	/************************************************************************/
	/* 使用Haar分类器作为种子点                                             */
	/************************************************************************/

#pragma region HaarAsSeeds

	//Haar牙齿识别
	CvHaarClassifierCascade *cascade = (CvHaarClassifierCascade *)cvLoad("cascade_tooth.xml");
	if (cascade == NULL)
	{
		printf("Haar Classifier Not Exist!");
		illnesses->status = ERR_HAAR_CLASSIFIER_NOT_FOUND;
		vec.push_back(ERR_HAAR_CLASSIFIER_NOT_FOUND);
	}

	CvMemStorage *storage = cvCreateMemStorage(0);
	cvClearMemStorage(storage);
	CvSeq *seq = cvHaarDetectObjects(img0, cascade, storage, 1.1, 2, CV_HAAR_DO_CANNY_PRUNING, cvSize(30, 30));
	cvReleaseHaarClassifierCascade(&cascade);

	//一切正常，状态置0
	vec.push_back(0);

#pragma endregion

	//将识别的矩形框按x坐标选择排序
	CvRect *xminRect = NULL;
	for (i = 0; i < seq->total; i++)
	{
		CvRect *r1 = (CvRect *)cvGetSeqElem(seq, i);
		xminRect = r1;
		for (j = i + 1; j < seq->total; j++)
		{
			CvRect *r2 = (CvRect *)cvGetSeqElem(seq, j);
			xminRect = xminRect->x < r2->x ? xminRect : r2;
		}

		CvRect rTemp = CvRect(*r1);
		*r1 = *xminRect;
		*xminRect = rTemp;
	}


	//根据矩形的面积大小去除不太可能是牙齿的区域
	int rectArea;
	float imgArea = img0->width * img0->height;
	for (i = seq->total - 1; i >= 0; i--)
	{
		CvRect *rect = (CvRect *)cvGetSeqElem(seq, i);
		//如果矩形面积小于图片大小的0.5%则不认为是牙齿
		rectArea = rect->width * rect->height;
		if (rectArea / imgArea < 0.005)
		{
			cvSeqRemove(seq, i);
			continue;
		}
	}

	//如果没有识别任何门牙，则认为是磨牙照片：
	if (seq->total == 0)
	{
		//0.磨牙
		inner = true;

#pragma region INNER_TEETH

		//1.Ostu
		IplImage *binary = cvCreateImage(cvGetSize(img0), IPL_DEPTH_8U, 1);
		IplImage *gray = cvCreateImage(cvGetSize(img0), IPL_DEPTH_8U, 1);
		cvCvtColor(img0, gray, CV_BGR2GRAY);
		cvThreshold(gray, binary, 0, 255, CV_THRESH_OTSU);

#ifdef DEBUG
		cvNamedWindow("Ostu", CV_WINDOW_NORMAL);
		cvShowImage("Ostu", binary);
#endif // DEBUG

		//2.最大连通域
		IplImage *maxRegionImg = getMaxRegion(binary);

#ifdef DEBUG
		cvNamedWindow("MaxRegion", CV_WINDOW_NORMAL);
		cvShowImage("MaxRegion", maxRegionImg);
		cvWaitKey();
#endif // DEBUG

		//2.? 最大连通域的边界
		CvMemStorage *storageMaxRegion = cvCreateMemStorage();
		CvSeq *maxRegionContour;
		assert(cvFindContours(cvCloneImage(maxRegionImg), storageMaxRegion, &maxRegionContour, 88, CV_RETR_CCOMP, CV_CHAIN_APPROX_SIMPLE) > 0);

		//3.圆形腐蚀
		//int radius = maxRegionImg->width * 0.4;
		int radius = 5;
		int iter = 30;
		IplConvKernel *diskSE = cvCreateStructuringElementEx(radius * 2 + 1, radius * 2 + 1, radius, radius, CV_SHAPE_ELLIPSE, NULL);
		cvMorphologyEx(maxRegionImg, maxRegionImg, NULL, diskSE, CV_MOP_ERODE, iter);

#ifdef DEBUG
		cvNamedWindow("MaxRegion", CV_WINDOW_NORMAL);
		cvShowImage("MaxRegion", maxRegionImg);
		cvWaitKey();
#endif // DEBUG

		//4.中心当做种子点
		//4.1 轮廓提取
		vector<CvPoint> seeds;
		CvSeq *contours;
		CvMemStorage *storage = cvCreateMemStorage();
		int nbComp = cvFindContours(cvCloneImage(maxRegionImg), storage, &contours, 88, CV_RETR_CCOMP, CV_CHAIN_APPROX_SIMPLE);
		for (CvSeq *contour = contours; contour != NULL; contour = contour->h_next)
		{
			//4.2 轮廓中心当做种子点
			CvRect rect = cvBoundingRect(contour);
			CvPoint point = cvPoint(rect.x + 0.5 * rect.width, rect.y + 0.5 * rect.height);
			seeds.push_back(point);
		}

		//5.分水岭
		//种子点图像初始化
		IplImage *markers = cvCreateImage(cvGetSize(img0), IPL_DEPTH_32S, 1);
		cvZero(markers);

		//标注磨牙种子点
		for (int i = 0; i < seeds.size(); i++)
		{
			cvDrawCircle(markers, seeds.at(i), iter * (radius - 1), cvScalarAll(i + 1));
		}

		//牙龈种子点（最大连通域的边缘）
		cvDrawContours(markers, maxRegionContour, cvScalarAll(UPPERGINGIVA), cvScalarAll(0), 1);
		//cvDrawRect(markers, cvPoint(1, 1), cvPoint(markers->width - 2, markers->height - 2), cvScalarAll(UPPERGINGIVA));

		cvWatershed(img0, markers);
		cvSave("markers_inner.xml", markers);

		//6.显示分割结果
		IplImage *img = markers2Colors(img0, markers, seeds.size());
#ifdef DEBUG
		cvNamedWindow("Watershed", CV_WINDOW_NORMAL);
		cvShowImage("Watershed", img);
		cvWaitKey();
#endif // DEBUG

		//7.分析单个牙齿
		illnesses->nbTeeth = seeds.size();
		illnesses->illnesses = new int[seeds.size()];
		for (int iTooth = 1; iTooth <= seeds.size(); iTooth++)
		{
			int result = detectInnerIllness(img0, markers, iTooth);
			vec.push_back(result);
			illnesses->illnesses[iTooth - 1] = result;
		}

#ifdef DEBUG
		/************************************************************************/
		/* 可视化地显示出疾病                                                   */
		/************************************************************************/
		IplImage *showIllnessInPic(IplImage *img0, IplImage *img, std::vector<int> vec);
		IplImage *illnessImg = showIllnessInPic(img0, img, vec);

		cvNamedWindow("Illness", CV_WINDOW_AUTOSIZE);
		cvShowImage("Illness", illnessImg);

		OnMouseParam param;
		param.src = illnessImg;
		param.dst = cvCloneImage(illnessImg);
		param.markers = markers;
		param.vec = vec;

		void on_mouse(int event, int x, int y, int flags, void *param);
		cvSetMouseCallback("Illness", on_mouse, &param);
		cvWaitKey();

		cvReleaseImage(&param.dst);
		cvReleaseImage(&illnessImg);
#endif // DEBUG

		//保存处理后的图片
		saveIllnessInPic(markers, vec, outputFileName);


		//回收
		cvReleaseImage(&binary);
		cvReleaseImage(&gray);
		cvReleaseMemStorage(&storage);

#pragma endregion

	}
	else
	{
		//0.前牙
		inner = false;

		//识别结果图片初始化
		IplImage *haarImg = cvCloneImage(img0);

		//判断上下牙的基准线（图片水平中轴线）
		int yBaseline = haarImg->height / 2;

		CvMat *pointsUpper = cvCreateMat(seq->total * 2, 2, CV_32SC1);
		CvMat *pointsLower = cvCreateMat(seq->total * 2, 2, CV_32SC1);
		CvRect *pUpperRect = NULL;
		CvRect *pLowerRect = NULL;
		int nbSeedUpper = 0, nbSeedLower = 0;
		int *pos = NULL;

		//门牙种子点
		for (i = seq->total - 1; i >= 0; i--)
		{
			CvRect *rect = (CvRect *)cvGetSeqElem(seq, i);

			cvDrawRect(haarImg, cvPoint(rect->x, rect->y), cvPoint(rect->x + rect->width, rect->y + rect->height), cvScalar(0, 0, 255));

			if (rect->y + 0.5 * rect->height < yBaseline)
			{
				if (pUpperRect == NULL || (pUpperRect->x + pUpperRect->width - rect->x) <= (0.25 * MIN(pUpperRect->width, rect->width)))
				{
					CV_MAT_ELEM(*pointsUpper, int, nbSeedUpper, 0) = (int)(rect->x + 0.25f * rect->width);
					CV_MAT_ELEM(*pointsUpper, int, nbSeedUpper, 1) = (int)(rect->y + 0.5f * rect->height);
					nbSeedUpper++;
				}

				CV_MAT_ELEM(*pointsUpper, int, nbSeedUpper, 0) = (int)(rect->x + 0.75f * rect->width);
				CV_MAT_ELEM(*pointsUpper, int, nbSeedUpper, 1) = (int)(rect->y + 0.5f * rect->height);
				nbSeedUpper++;

				pUpperRect = rect;
			}
			else
			{
				if (pLowerRect == NULL || (pLowerRect->x + pLowerRect->width - rect->x) <= (0.25 * MIN(pLowerRect->width, rect->width)))
				{
					CV_MAT_ELEM(*pointsLower, int, nbSeedLower, 0) = (int)(rect->x + 0.25f * rect->width);
					CV_MAT_ELEM(*pointsLower, int, nbSeedLower, 1) = (int)(rect->y + 0.5f * rect->height);
					nbSeedLower++;
				}

				CV_MAT_ELEM(*pointsLower, int, nbSeedLower, 0) = (int)(rect->x + 0.75f * rect->width);
				CV_MAT_ELEM(*pointsLower, int, nbSeedLower, 1) = (int)(rect->y + 0.5f * rect->height);
				nbSeedLower++;

				pLowerRect = rect;
			}
		}

		//牙齿的y轴最小值、最大值
		int ymin, ymax;
		if (seq->total == 0)
		{
			ymin = 0;
			ymax = haarImg->height - 1;
		}
		else
		{
			ymin = haarImg->height - 1;
			ymax = 0;

			for (i = 0; i < seq->total; i++)
			{
				CvRect *rect = (CvRect *)cvGetSeqElem(seq, i);
				ymin = MIN(ymin, rect->y);
				ymax = MAX(ymax, rect->y + rect->height);
			}
		}

		//上牙龈种子点
		CvPoint upperPoint = cvPoint(haarImg->width / 2, ymin - 1);

		//下牙龈种子点
		CvPoint lowerPoint = cvPoint(haarImg->width / 2, ymax);

		//storage中存放Haar级联分类器获得的结果，之前不能释放

#ifdef DEBUG
		cvNamedWindow("haar", CV_WINDOW_NORMAL);
		cvShowImage("haar", haarImg);
		cvWaitKey();
#endif // DEBUG

		cvReleaseImage(&haarImg);
		/************************************************************************/
		/* 开始分割                                                             */
		/************************************************************************/

#pragma region Segmentation

		//种子点图像初始化
		IplImage *markers = cvCreateImage(cvGetSize(img0), IPL_DEPTH_32S, 1);
		cvZero(markers);

		//上牙齿种子点
		for (int i = 0; i < nbSeedUpper; i++)
		{
			int x = CV_MAT_ELEM(*pointsUpper, int, i, 0);
			int y = CV_MAT_ELEM(*pointsUpper, int, i, 1);
			cvDrawCircle(markers, cvPoint(x, y), 10, cvScalarAll(i + 1));
		}

		//下牙齿种子点
		for (int i = 0; i < nbSeedLower; i++)
		{
			int x = CV_MAT_ELEM(*pointsLower, int, i, 0);
			int y = CV_MAT_ELEM(*pointsLower, int, i, 1);
			cvDrawCircle(markers, cvPoint(x, y), 10, cvScalarAll(nbSeedUpper + i + 1));
		}

		//上下牙龈
		//cvDrawCircle(markers, upperPoint, 10, cvScalarAll(UPPERGINGIVA));
		//cvDrawCircle(markers, lowerPoint, 10, cvScalarAll(LOWERGINGIVA));
		//cvDrawCircle(markers, cvPoint(markers->width / 2, ymin / 2), ymin / 2, cvScalarAll(UPPERGINGIVA));
		//cvDrawCircle(markers, cvPoint(markers->width / 2, (ymax + markers->height) / 2), (markers->height - ymax) / 2, cvScalarAll(LOWERGINGIVA));
		cvDrawRect(markers, cvPoint(1, 1), cvPoint(markers->width - 2, markers->height - 2), cvScalarAll(UPPERGINGIVA));

		/*IplImage *markers_bi = cvCloneImage(markers);
		markers_bi->depth = 8;
		cvThreshold(markers_bi, markers_bi, 1, 255, CV_THRESH_BINARY);
		cvNamedWindow("markers");
		cvShowImage("markers", markers_bi);
		cvWaitKey();
		cvReleaseImage(&markers_bi);*/

		//分水岭算法
		cvWatershed(img0, markers);
		cvSave("markers.xml", markers);

#pragma region Depart
		//切分粘合的牙齿
		CvMemStorage *tempStorage = cvCreateMemStorage();
		CvSeq *contours = NULL;
		int nbComp = cvFindContours(cvCloneImage(markers), tempStorage, &contours, 88, CV_RETR_CCOMP, CV_CHAIN_APPROX_SIMPLE);
		assert(nbComp != 0);

		//寻找某个区域水平中线以上含有连续n个点且中间一个纵坐标最大
		const int gap = 55;	//连续几个点
		int nbTeeth = nbSeedLower + nbSeedUpper;

		for (CvSeq *contour = contours; contour != NULL; contour = contour->h_next)
		{
			//!认为轮廓线围成的面积大于100000就为牙龈，然后跳过
			if (cvContourArea(contour) > 100000)
			{
				continue;
			}

			CvRect rect = cvBoundingRect(contour);

			int medianLine = (int)(rect.y + rect.height * 0.5f);

			for (int i = 0; i < contour->total - gap + 1; i++)
			{
				//所有的n个点的都必须在中线上面
				int j;
				for (j = 0; j < gap; j++)
				{
					if (CV_GET_SEQ_ELEM(CvPoint, contour, i + j)->y > medianLine)
					{
						break;
					}
				}
				if (j != gap)
				{
					continue;
				}

				//得到中间的点的坐标
				CvPoint *midPoint = CV_GET_SEQ_ELEM(CvPoint, contour, i + gap / 2);

				//与两边的点的坐标进行对比
				for (j = 0; j < gap; j++)
				{
					CvPoint *curPoint = CV_GET_SEQ_ELEM(CvPoint, contour, i + j);
					if (curPoint->y > midPoint->y)
					{
						break;
					}
				}
				if (j != gap)
				{
					continue;
				}

				//中间的点确实是应该分割的起始点
				int x0 = midPoint->x;
				int y0 = midPoint->y;
				int y1, y2;
				while (CV_IMAGE_ELEM(markers, int, y0, x0) == -1)
				{
					y0++;
				}
				y1 = y0;

				while (CV_IMAGE_ELEM(markers, int, y0, x0) != -1)
				{
					CV_IMAGE_ELEM(markers, int, y0, x0) = -1;
				}
				y2 = y0;

				//将左边的区域的填充设置为++nbTeeth
				int newVal = ++nbTeeth;
				int oldVal = CV_IMAGE_ELEM(markers, int, (y1 + y2) / 2, x0 + 1);

				for (int m = 0; m < markers->height; m++)
				{
					for (int n = 0; n < x0; n++)
					{
						if (CV_IMAGE_ELEM(markers, int, m, n) == oldVal)
						{
							CV_IMAGE_ELEM(markers, int, m, n) = newVal;
						}
					}
				}

				i += gap;
				continue;
			}
		}

		cvReleaseMemStorage(&tempStorage);
#pragma endregion


//#ifdef DEBUG
//		cvClearMemStorage(storage);
//		CvSeq *contours;
//
//		cvFindContours(cvCloneImage(markers), storage, &contours, 88, CV_RETR_CCOMP, CV_CHAIN_APPROX_SIMPLE);
//
//		IplImage *tempImg = cvCreateImage(cvGetSize(img0), IPL_DEPTH_8U, 3);
//		cvZero(tempImg);
//
//		for (; contours != NULL; contours = contours->h_next)
//		{
//			cvDrawContours(tempImg, contours, cvScalarAll(255), cvScalarAll(0), 3);
//		}
//
//		cvNamedWindow("temp");
//		cvShowImage("temp", tempImg);
//		cvWaitKey();
//#endif // DEBUG

		IplImage *markers2Colors(IplImage *img0, IplImage *markers, int nbTeeth);
		
		IplImage *img = markers2Colors(img0, markers, nbTeeth);

#ifdef DEBUG
		cvNamedWindow("after", CV_WINDOW_NORMAL);
		cvShowImage("after", img);
		cvWaitKey();
#endif // DEBUG

#pragma endregion

		/************************************************************************/
		/* 每个牙齿单独抠出进行分析                                             */
		/************************************************************************/

#pragma region SingleTooth

		illnesses->nbTeeth = nbTeeth;
		illnesses->illnesses = new int[nbTeeth];
		for (int iTooth = 1; iTooth <= nbTeeth; iTooth++)
		{
			int result = detectOuterIllness(img0, markers, iTooth);
			vec.push_back(result);
			illnesses->illnesses[iTooth - 1] = result;
		}
			//IplImage *mask = cvCloneImage(img0);
			//uchar *pMask = &CV_IMAGE_ELEM(mask, uchar, 0, 0);
			//int *pMarkers = &CV_IMAGE_ELEM(markers, int, 0, 0);
			//for (i = 0; i < img0->width * img0->height; i++)
			//{
			//	if (pMarkers[i] == iTooth)
			//	{
			//		pMask[3 * i] = pMask[3 * i + 1] = pMask[3 * i + 2] = (uchar)255;
			//	}
			//	else
			//	{
			//		pMask[3 * i] = pMask[3 * i + 1] = pMask[3 * i + 2] = 0;
			//	}
			//}

			//cvNamedWindow("mask");
			//cvShowImage("mask", mask);
			//cvWaitKey();

			////边缘平滑
			//IplConvKernel *elem = cvCreateStructuringElementEx(11, 11, 5, 5, CV_SHAPE_ELLIPSE);
			//cvMorphologyEx(mask, mask, NULL, elem, CV_MOP_OPEN);
			//cvMorphologyEx(mask, mask, NULL, elem, CV_MOP_CLOSE);
			//cvReleaseStructuringElement(&elem);
			//cvShowImage("mask", mask);
			//cvWaitKey();

			//IplImage *imgTooth = cvCloneImage(img0);
			//cvAnd(img0, mask, imgTooth);

			//cvNamedWindow("tooth");
			//cvShowImage("tooth", imgTooth);
			//cvWaitKey();

#pragma endregion

#ifdef DEBUG

		/************************************************************************/
		/* 可视化地显示出疾病                                                   */
		/************************************************************************/
		IplImage *showIllnessInPic(IplImage *img0, IplImage *img, std::vector<int> vec);
		IplImage *illnessImg = showIllnessInPic(img0, img, vec);
		cvNamedWindow("Illness", CV_WINDOW_AUTOSIZE);
		cvShowImage("Illness", illnessImg);

		OnMouseParam param;
		param.src = illnessImg;
		param.dst = cvCloneImage(illnessImg);
		param.markers = markers;
		param.vec = vec;

		void on_mouse(int event, int x, int y, int flags, void *param);
		cvSetMouseCallback("Illness", on_mouse, &param);
		cvWaitKey();

		cvReleaseImage(&param.dst);
		cvReleaseImage(&illnessImg);

		/************************************************************************/
		/* 释放资源                                                             */
		/************************************************************************/

		//种子点坐标
		cvReleaseMat(&pointsUpper);
		cvReleaseMat(&pointsLower);
		cvReleaseImage(&img);
#endif // DEBUG

		//保存处理后的图片
		saveIllnessInPic(markers, vec, outputFileName);

		cvReleaseImage(&markers);
	}


	cvReleaseImage(&img0);
	cvReleaseMemStorage(&storage);
	return *illnesses;
}


//通过形态学Ultimate Erosion寻找一个图像中的种子点
int getSeeds(CvSeq **seeds, IplImage *img, CvMemStorage *storages[])
{
	IplImage *src = cvCloneImage(img);
	int oldNbContour, nbContour;
	CvSeq *oldContours, *contours;
	CvSeq *contour, *oldContour;
	storages[0] = cvCreateMemStorage();
	storages[1] = cvCreateMemStorage();
	storages[2] = cvCreateMemStorage();
	IplConvKernel *kernel = cvCreateStructuringElementEx(5, 5, 2, 2, CV_SHAPE_ELLIPSE);
	int iteration;
	CvRNG rng = cvRNG();

	//Ultimate Erosion寻找种子点
	//思路：当前一次的Contour数量减少的时候，检查前一次的Contour（实际只需要一个点就够了）是否在包含某个后一次的Contour
	//如果不存在则该前一次的Contour的中心即为一个种子点
	cvFindContours(cvCloneImage(src), storages[0], &contours, 88, CV_RETR_CCOMP, CV_CHAIN_APPROX_SIMPLE);

//#ifdef DEBUG
//	cvCvtColor(src, contourImg, CV_GRAY2BGR);
//	for (nbContour = 0, contour = contours; contour != NULL; contour = contour->h_next, nbContour++)
//		cvDrawContours(contourImg, contour, cvScalar(cvRandInt(&rng) % 256, cvRandInt(&rng) % 256, cvRandInt(&rng) % 256),
//		cvScalarAll(0), 3, 3);
//	cvShowImage("contour", contourImg);
//#else
	for (nbContour = 0, contour = contours; contour != NULL; contour = contour->h_next, nbContour++);
//#endif // DEBUG

	*seeds = cvCreateSeq(CV_SEQ_ELTYPE_POINT, sizeof(CvSeq), sizeof(CvPoint), storages[2]);

	iteration = 1;
	while (nbContour != 0)
	{
		oldNbContour = nbContour;
		oldContours = contours;

		cvErode(src, src, kernel, 1);

		cvFindContours(cvCloneImage(src), storages[iteration % 2], &contours, 88, CV_RETR_CCOMP, CV_CHAIN_APPROX_SIMPLE);

//#ifdef DEBUG
//		cvCvtColor(src, contourImg, CV_GRAY2BGR);
//		for (nbContour = 0, contour = contours; contour != NULL; contour = contour->h_next, nbContour++)
//			cvDrawContours(contourImg, contour, cvScalar(cvRandInt(&rng) % 256, cvRandInt(&rng) % 256, cvRandInt(&rng) % 256),
//			cvScalarAll(0), 3, 3);
//		cvShowImage("contour", contourImg);
//#else
		for (nbContour = 0, contour = contours; contour != NULL; contour = contour->h_next, nbContour++);
//#endif // DEBUG

		//如果某区域被腐蚀至消失
		if (nbContour < oldNbContour)
		{
			bool found;
			//遍历
			for (oldContour = oldContours; oldContour != NULL; oldContour = oldContour->h_next)
			{
				found = false;
				CvMat *polygon = cvCreateMat(1, oldContour->total, CV_32FC2);
				float *pData = (float *)(polygon->data.ptr);
				for (int i = 0; i < oldContour->total; i++)
				{
					CvPoint *point = CV_SEQ_ELEM(oldContour, CvPoint, i);
					*pData++ = (float)point->x;
					*pData++ = (float)point->y;
				}

				for (contour = contours; contour != NULL; contour = contour->h_next)
				{
					CvPoint *point = CV_GET_SEQ_ELEM(CvPoint, contour, 0);
					if (cvPointPolygonTest(polygon, cvPoint2D32f(point->x, point->y), 1) > 0)
					{
						found = true;
						break;
					}
				}

				//确实为消失，种子点为oldContour的中心
				if (!found)
				{
					CvRect rect = cvBoundingRect(polygon, 1);
					CvPoint center = cvPoint((int)(rect.x + 0.5f * rect.width), (int)(rect.y + 0.5f * rect.height));
					cvSeqPush(*seeds, &center);
				}
				cvReleaseMat(&polygon);
			}
		}

#ifdef DEBUG
		cvShowImage("image", src);
		printf("Number of Components: %d\n", nbContour);
		cvWaitKey();
#endif // DEBUG

		oldNbContour = nbContour;
		iteration++;
	}

	//将十分靠近的种子点只保留一个
	for (int i = (*seeds)->total - 1; i >= 0; i--)
	{
		CvPoint *point1 = CV_GET_SEQ_ELEM(CvPoint, *seeds, i);
		for (int j = i - 1; j >= 0; j--)
		{
			CvPoint *point2 = CV_GET_SEQ_ELEM(CvPoint, *seeds, j);
			float x1 = (float)point1->x; float y1 = (float)point1->y;
			float x2 = (float)point2->x; float y2 = (float)point2->y;
			if (((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)) < 5 * 5)
			{
				cvSeqRemove(*seeds, i);
			}
		}
	}

	cvReleaseStructuringElement(&kernel);


	return (*seeds)->total;
}


//以图片的形式显示出疾病的类型
IplImage *showIllnessInPic(IplImage *img0, IplImage *img, std::vector<int> vec)
{
	IplImage *illnessImg = cvCreateImage(cvGetSize(img), IPL_DEPTH_8U, 3);
	cvAddWeighted(img0, 0.5, img, 0.5, 0, illnessImg);
	return illnessImg;
}

void on_mouse(int event, int x, int y, int flags, void* param)
{
	OnMouseParam *mouseParam = (OnMouseParam *)param;
	IplImage *dst = mouseParam->dst;
	int toothIndex;
	int result;
	switch (event)
	{
	case CV_EVENT_MOUSEMOVE:
		cvCopyImage(mouseParam->src, dst);

		toothIndex = CV_IMAGE_ELEM(mouseParam->markers, int, y, x);
		if (toothIndex > mouseParam->vec.size())
		{
			return;
		}

		result = mouseParam->vec.at(toothIndex);

		char posStr[20], toothNoStr[20], illnessStr[40];
		sprintf(posStr, "x:%d y:%d", x, y);
		sprintf(toothNoStr, "Tooth No: %d", toothIndex);
		if ((result & ILLNESS_DECAY_MASK) == 0)
		{
			strcpy(illnessStr, "No Decayed Tooth");
		}
		if ((result & ILLNESS_DECAY_LIGHT) != 0)
		{
			strcpy(illnessStr, "Light");
		}
		else if ((result & ILLNESS_DECAY_MEDIUM) != 0)
		{
			strcpy(illnessStr, "Medium");
		}
		else if ((result & ILLNESS_DECAY_SEVERE) != 0)
		{
			strcpy(illnessStr, "Severe");
		}

		cvPutText(dst, posStr, cvPoint(10, 20), &cvFont(1, 1), cvScalarAll(255));
		cvPutText(dst, toothNoStr, cvPoint(10, 40), &cvFont(1, 1), cvScalarAll(255));
		cvPutText(dst, illnessStr, cvPoint(10, 60), &cvFont(1, 1), cvScalarAll(255));

		cvShowImage("Illness", dst);
		break;
	default:
		break;
	}
}

//获取最大连通域的二值图像
IplImage *getMaxRegion(IplImage *bw)
{
	IplImage *maxRegion = cvCloneImage(bw);

	int i, j;
	int color = 1;
	for (i = 0; i < maxRegion->height; i++)
	{
		for (j = 0; j < maxRegion->width; j++)
		{
			uchar curPixel = CV_IMAGE_ELEM(maxRegion, uchar, i, j);
			if (curPixel == 255)
			{
				cvFloodFill(maxRegion, cvPoint(j, i), cvScalarAll(color++));
			}
		}
	}

	//统计连通域个数
	int nbConn = color - 1;
	assert(nbConn <= 255);

	//连通域拥有的像素点个数
	if (nbConn > 0)
	{
		int pixels[256] = { 0 };

		for (i = 0; i < maxRegion->height; i++)
		{
			for (j = 0; j < maxRegion->width; j++)
			{
				pixels[CV_IMAGE_ELEM(maxRegion, uchar, i, j)]++;
			}
		}

		//最大连通域对应的color
		uchar maxColor = 1;
		for (i = 2; i <= nbConn; i++)
		{
			if (pixels[i] > pixels[maxColor])
			{
				maxColor = i;
			}
		}

		//根据这个color将图片二值化（颜色==color则设为255；否则为0）
		for (i = 0; i < maxRegion->height; i++)
		{
			for (j = 0; j < maxRegion->width; j++)
			{
				if (CV_IMAGE_ELEM(maxRegion, uchar, i, j) == maxColor)
					CV_IMAGE_ELEM(maxRegion, uchar, i, j) = 255;
				else
					CV_IMAGE_ELEM(maxRegion, uchar, i, j) = 0;
			}
		}
	}

	return maxRegion;
}

IplImage *markers2Colors(IplImage *img0, IplImage *markers, int nbTeeth)
{
	int i, j;
	IplImage *img = cvCreateImage(cvGetSize(img0), IPL_DEPTH_8U, 3);

	//初始化随机数种子
	CvRNG rng = cvRNG(time(NULL));

	//初始化颜色矩阵
	CvMat *colors = cvCreateMat(1, nbTeeth, CV_8UC3);
	for (i = 0; i < nbTeeth; i++)
	{
		uchar *ptr = colors->data.ptr + i * 3;
		ptr[0] = (uchar)(cvRandInt(&rng) % 200 + 30);
		ptr[1] = (uchar)(cvRandInt(&rng) % 200 + 30);
		ptr[2] = (uchar)(cvRandInt(&rng) % 200 + 30);
	}

	//设置颜色
	for (i = 0; i < markers->height; i++)
	{
		for (j = 0; j < markers->width; j++)
		{
			int cur = CV_IMAGE_ELEM(markers, int, i, j);
			uchar *ptr = &CV_IMAGE_ELEM(img, uchar, i, j * 3);

			if (cur == -1)	//边界
			{
				ptr[0] = ptr[1] = ptr[2] = (uchar)255;
			}
			else
			{
				uchar *pColor = colors->data.ptr + (cur - 1) * 3;

				ptr[0] = pColor[0];
				ptr[1] = pColor[1];
				ptr[2] = pColor[2];
			}
		}
	}

	return img;
}

const Vec3b RED(0, 0, 255);
const Vec3b BLACK(0, 0, 0);
const Vec3b YELLOW(128, 255, 255);
const Vec3b BROWN(87, 122, 185);
const Vec3b GREEN(0, 255, 0);

//根据血量（绿黄红棕）的类比显示
void saveIllnessInPic(IplImage *markers, vector<int> vec, const char *fileName)
{
	IplImage *image = cvCreateImage(cvGetSize(markers), IPL_DEPTH_8U, 3);

	for (int i = 0; i < markers->height; i++)
	{
		for (int j = 0; j < markers->width; j++)
		{
			int curPixel = CV_IMAGE_ELEM(markers, int, i, j);
			if (curPixel > 0 && curPixel < UPPERGINGIVA)
			{
				switch (vec.at(curPixel - 1))
				{
				case ILLNESS_DECAY_SEVERE:
					CV_IMAGE_ELEM(image, Vec3b, i, j) = RED;
					break;
				case ILLNESS_DECAY_MEDIUM:
					CV_IMAGE_ELEM(image, Vec3b, i, j) = BROWN;
					break;
				case ILLNESS_DECAY_LIGHT:
					CV_IMAGE_ELEM(image, Vec3b, i, j) = YELLOW;
					break;
				default:
					CV_IMAGE_ELEM(image, Vec3b, i, j) = GREEN;
					break;
				}
			}
			else
			{
				CV_IMAGE_ELEM(image, Vec3b, i, j) = BLACK;
			}
		}
	}
	const int params[] = { CV_IMWRITE_JPEG_QUALITY, 98 };
	cvSaveImage(fileName, image, params);
}