#ifndef __SINGLE_TOOTH_H__
#define	__SINGLE_TOOTH_H__

#include <opencv\cv.h>

int detectOuterIllness(IplImage *img, IplImage *markers, int toothIndex);
int detectInnerIllness(IplImage *img, IplImage *markers, int toothIndex);

#endif // __SINGLE_TOOTH_H__
