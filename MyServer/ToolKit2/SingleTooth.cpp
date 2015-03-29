#include "SingleTooth.h"
#include "illnessAnalysis.h"
#include <opencv\highgui.h>

//int detectIllness(IplImage *img, IplImage *markers, int toothIndex)
//{
//	int result = 0;
//
//	//龋齿检测
//	IplImage *gray = cvCreateImage(cvGetSize(img), IPL_DEPTH_8U, 1);
//	cvCvtColor(img, gray, CV_BGR2GRAY);
//
//	//统计黑色像素点的数目
//	int nbDark = 0;
//	int nbPixels = 0;
//	const int threshold = 80;
//	for (int i = 0; i < img->height; i++)
//	{
//		for (int j = 0; j < img->width; j++)
//		{
//			if (CV_IMAGE_ELEM(markers, int, i, j) == toothIndex)
//			{
//				nbPixels++;
//				if (CV_IMAGE_ELEM(gray, uchar, i, j) < threshold)
//				{
//					nbDark++;
//				}
//			}
//		}
//	}
//
//	float darkPercent = (float)nbDark / nbPixels;
//
//	//根据统计得到的黑色像素数量百分比判定龋齿深浅
//	if (darkPercent < 0.01f)
//	{
//		result &= ILLNESS_DECAY_MASK;
//	}
//	else if (darkPercent < 0.02f)
//	{
//		result |= ILLNESS_DECAY_LIGHT;
//	}
//	else if (darkPercent < 0.05f)
//	{
//		result |= ILLNESS_DECAY_MEDIUM;
//	}
//	else if (darkPercent < 0.10f)
//	{
//		result |= ILLNESS_DECAY_SEVERE;
//	}
//
//	return result;
//}


int detectIllness(IplImage *img, IplImage *markers, int toothIndex)
{
	
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


