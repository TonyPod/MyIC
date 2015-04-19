#include "SingleTooth.h"
#include "illnessAnalysis.h"
#include <opencv\highgui.h>
#include <opencv2\ml\ml.hpp>
#include <io.h>

using namespace cv;
using namespace std;

int detectInnerIllness(IplImage *img, IplImage *markers, int toothIndex)
{
	int result = 0;

	//龋齿检测
	IplImage *gray = cvCreateImage(cvGetSize(img), IPL_DEPTH_8U, 1);
	cvCvtColor(img, gray, CV_BGR2GRAY);

	//统计黑色像素点的数目
	int nbDark = 0;
	int nbPixels = 0;
	const int threshold = 80;
	for (int i = 0; i < img->height; i++)
	{
		for (int j = 0; j < img->width; j++)
		{
			if (CV_IMAGE_ELEM(markers, int, i, j) == toothIndex)
			{
				nbPixels++;
				if (CV_IMAGE_ELEM(gray, uchar, i, j) < threshold)
				{
					nbDark++;
				}
			}
		}
	}

	float darkPercent = (float)nbDark / nbPixels;

	//根据统计得到的黑色像素数量百分比判定龋齿深浅
	if (darkPercent < 0.01f)
	{
		result &= ILLNESS_DECAY_MASK;
	}
	else if (darkPercent < 0.02f)
	{
		result |= ILLNESS_DECAY_LIGHT;
	}
	else if (darkPercent < 0.05f)
	{
		result |= ILLNESS_DECAY_MEDIUM;
	}
	else if (darkPercent < 0.10f)
	{
		result |= ILLNESS_DECAY_SEVERE;
	}

	return result;
}


//从64x64的灰度图像中提取特征向量
void ExtFeatureMat(IplImage *gray, Mat& inputsMat)
{
	assert(gray->width == 64 && gray->height == 64);

	//存放灰度分布的数组
	int bins[256];
	float mean;
	int nb20, nb50, nb80;

	IplConvKernel *kernel = cvCreateStructuringElementEx(5, 5, 2, 2, CV_SHAPE_ELLIPSE, NULL);

	//0.预处理
	//利用开操作去除亮斑
	cvMorphologyEx(gray, gray, NULL, kernel, CV_MOP_OPEN, 1);

	//1.灰度重心
	mean = 0;
	nb20 = nb50 = nb80 = 0;
	memset(bins, 0, sizeof(bins));
	for (int m = 0; m < gray->height; m++)
	{
		for (int n = 0; n < gray->width; n++)
		{
			uchar grayVal = CV_IMAGE_ELEM(gray, uchar, m, n);
			bins[grayVal]++;
			mean += grayVal;
			if (grayVal < 20)
			{
				nb20++;
			}
			else if (grayVal < 50)
			{
				nb50++;
			}
			else if (grayVal < 80)
			{
				nb80++;
			}
		}
	}
	mean /= (gray->height * gray->width);

	//2.灰度峰值
	int max = 0;
	int j = 0;
	while (bins[j++] == 0);
	max = j;

	//输出结果
#ifdef DEBUG
	printf("灰度重心：%f\n", mean);
	printf("灰度峰值：%d\n", max);
	printf("低于20：%d\n", nb20);
	printf("低于50：%d\n", nb50);
	printf("低于80：%d\n", nb80);
#endif // DEBUG

	//3.Hu不变矩
	CvMoments moments;
	CvHuMoments hu_moments;
	cvMoments(gray, &moments, 0);
	cvGetHuMoments(&moments, &hu_moments);

#ifdef DEBUG


	cout << hu_moments.hu1 << endl;
	cout << hu_moments.hu2 << endl;
	cout << hu_moments.hu3 << endl;
	//最多用到三阶矩（越大误差越大）

	printf("\n");
#endif // DEBUG

	//特征向量
	vector<double> vec;
	vec.push_back(mean);
	vec.push_back(max);
	vec.push_back(nb20);
	vec.push_back(nb50);
	vec.push_back(nb80);
	vec.push_back(hu_moments.hu1);
	vec.push_back(hu_moments.hu2);
	vec.push_back(hu_moments.hu3);

	for (int t = 0; t < vec.size(); t++)
	{
		inputsMat.at<double>(0, t) = vec.at(t);
	}

	cvReleaseStructuringElement(&kernel);
}

int detectOuterIllness(IplImage *img, IplImage *markers, int toothIndex)
{
	int left = markers->width - 1;
	int top = markers->height - 1;
	int right = 0;
	int bottom = 0;

	int curPixel;
	int i, j;
	for (i = 0; i < markers->height; i++)
	{
		for (j = 0; j < markers->width; j++)
		{
			curPixel = CV_IMAGE_ELEM(markers, int, i, j);
			if (curPixel == toothIndex)
			{
				//修正外界长方形
				if (j < left) left = j;
				if (j > right) right = j;
				if (i < top) top = i;
				if (i > bottom) bottom = i;
			}
		}
	}

	//将长方形转换为外接正方形
	int centerX = (left + right) / 2;
	int centerY = (top + bottom) / 2;
	int radius = MIN((right - left) / 2, (bottom - top) / 2) - 1;

	//提取指定牙齿的部分
	IplImage *toothImg = cvCreateImage(cvSize(radius * 2 + 1, radius * 2 + 1), IPL_DEPTH_8U, 3);
	for (i = 0; i < toothImg->height; i++)
	{
		for (j = 0; j < toothImg->width; j++)
		{
			CV_IMAGE_ELEM(toothImg, Vec3b, i, j) = CV_IMAGE_ELEM(img, Vec3b, centerY - radius + i, centerX - radius + j);
		}
	}

	cvShowImage("toothImg", toothImg);

	//压缩至64x64
	IplImage *newToothImg = cvCreateImage(cvSize(64, 64), IPL_DEPTH_8U, 3);
	cvResize(toothImg, newToothImg, 1);
	IplImage *newToothImgGray = cvCreateImage(cvSize(64, 64), IPL_DEPTH_8U, 1);
	cvCvtColor(newToothImg, newToothImgGray, CV_BGR2GRAY);
	cvShowImage("newToothImgGray", newToothImgGray);

	//特征向量提取
	int nbKind = 4; //疾病类型（龋齿3种+正常=4）
	Mat inputsMat = Mat(1, 8, CV_64FC1);
	Mat outputsMat = Mat(1, nbKind, CV_64FC1);
	ExtFeatureMat(newToothImgGray, inputsMat);

	//ANN预测
	CvANN_MLP ann;
	ann.load("params.xml");
	ann.predict(inputsMat, outputsMat);

	int maxIdx = 0;
	for (int i = 0; i < nbKind; i++)
	{
		if (outputsMat.at<double>(0, i) > outputsMat.at<double>(0, maxIdx))
		{
			maxIdx = i;
		}
	}

	//判决结果
	int result = 0;
	if (maxIdx > 0)
	{
		result = 1 << (maxIdx - 1);
	}

	//回收
	cvReleaseImage(&toothImg);
	cvReleaseImage(&newToothImg);
	cvReleaseImage(&newToothImgGray);

	return result;
}

//#pragma region Analysis
//
//	IplImage *b, *g, *r;
//	b = cvCreateImage(cvGetSize(img0), IPL_DEPTH_8U, 1);
//	g = cvCreateImage(cvGetSize(img0), IPL_DEPTH_8U, 1);
//	r = cvCreateImage(cvGetSize(img0), IPL_DEPTH_8U, 1);
//	cvSplit(imgTooth, b, g, r, NULL);
//
//	int dim = 1;
//	int sizes = 256;
//	float range[] = { 0, 255 };
//	float *ranges[] = { range };
//	CvHistogram *histR = cvCreateHist(dim, &sizes, CV_HIST_ARRAY, ranges, 1);
//	CvHistogram *histG = cvCreateHist(dim, &sizes, CV_HIST_ARRAY, ranges, 1);
//	CvHistogram *histB = cvCreateHist(dim, &sizes, CV_HIST_ARRAY, ranges, 1);
//
//	cvCalcHist(&r, histR);
//	cvCalcHist(&g, histG);
//	cvCalcHist(&b, histB);
//
//	IplImage *imgHist = cvCreateImage(cvSize(256, 300), 8, 3);
//	for (int i = 1; i < sizes; i++)
//	{
//		cvLine(imgHist,
//			cvPoint(i - 1, imgHist->height - (int)(cvQueryHistValue_1D(histR, i - 1) / 10)),
//			cvPoint(i, imgHist->height - (int)(cvQueryHistValue_1D(histR, i) / 10)),
//			CV_RGB(255, 0, 0));
//		cvLine(imgHist,
//			cvPoint(i - 1, imgHist->height - (int)(cvQueryHistValue_1D(histG, i - 1) / 10)),
//			cvPoint(i, imgHist->height - (int)(cvQueryHistValue_1D(histG, i) / 10)),
//			CV_RGB(0, 255, 0));
//		cvLine(imgHist,
//			cvPoint(i - 1, imgHist->height - (int)(cvQueryHistValue_1D(histB, i - 1) / 10)),
//			cvPoint(i, imgHist->height - (int)(cvQueryHistValue_1D(histB, i) / 10)),
//			CV_RGB(0, 0, 255));
//	}
//
//	cvNamedWindow("hist");
//	cvShowImage("hist", imgHist);
//	cvWaitKey();
//
//	cvReleaseHist(&histB);
//	cvReleaseHist(&histG);
//	cvReleaseHist(&histR);
//
//#pragma endregion