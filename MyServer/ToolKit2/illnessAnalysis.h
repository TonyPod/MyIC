#ifndef __ILLNESS_ANALYSIS_H__
#define __ILLNESS_ANALYSIS_H__

#include <opencv/cv.h>
#include <opencv/highgui.h>
#include <opencv2/objdetect/objdetect.hpp>

using namespace std;

struct OnMouseParam
{
	IplImage *src;
	IplImage *dst;
	IplImage *markers;
	std::vector<int> vec;
};

typedef struct _Illnesses
{
	int nbTeeth;
	int status;
	int *illnesses;
}Illnesses;

//—¿ˆ∏µƒ±‡∫≈
const int UPPERGINGIVA = 20;
const int LOWERGINGIVA = 21;

_declspec(dllexport) Illnesses _stdcall analyze(const char *fileName, const char *outputFileName);
int getSeeds(CvSeq **seeds, IplImage *img, CvMemStorage *storages[]);
void saveIllnessInPic(IplImage *markers, vector<int> vec, const char *fileName);

const int ILLNESS_DECAY_MASK = 7;
//º≤≤°¿‡–Õ
enum
{
	//»£≥›
	ILLNESS_DECAY_LIGHT = 1,
	ILLNESS_DECAY_MEDIUM = 2,
	ILLNESS_DECAY_SEVERE = 4,
};

//¥ÌŒÛ¿‡–Õ
enum
{
	ERR_HAAR_CLASSIFIER_NOT_FOUND = -1,
};

#endif