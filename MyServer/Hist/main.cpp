#include <opencv\cv.h>
#include <opencv\highgui.h>
#include <io.h>
#include <opencv2\ml\ml.hpp>

using namespace std;
using namespace cv;

vector<char *> findJpgsInFolder(const char *path)
{
	vector<char *> fileNames;
	struct _finddata_t filefind;
	char curr[100];
	strcpy(curr, path);
	strcat(curr, "\\*.*");
	int done = 0, handle;
	if ((handle = _findfirst(curr, &filefind)) == -1)
		return fileNames;
	while (!(done = _findnext(handle, &filefind)))
	{
		if (!strcmp(filefind.name, "..")){
			continue;
		}

		//是文件
		if (_A_SUBDIR != filefind.attrib)
		{
			//排除不是JPEG的图片
			char *result = NULL;
			char *prev = NULL;
			char delims[] = ".";
			char fileNameTemp[100];
			strcpy(fileNameTemp, filefind.name);
			result = strtok(fileNameTemp, delims);
			while (result != NULL)
			{
				prev = result;
				result = strtok(NULL, delims);
			}
			if (prev == NULL)
			{
				continue;
			}

			if (strcmp(prev, "jpg") != 0)
			{
				continue;
			}

			char *temp = (char *)malloc(100);
			strcat(strcpy(temp, path), filefind.name);
			fileNames.push_back(temp);
			cout << path << "\\" << filefind.name << endl;
		}
	}
	_findclose(handle);

	return fileNames;
}

int main(void)
{
	const char *pathname = "C:\\Users\\Tony\\Desktop\\训练图片\\4种\\";
	vector<char *> fileNames = findJpgsInFolder(pathname);

	IplImage *image = cvLoadImage(fileNames.at(0));
	int hist_size = 256;
	float range[] = { 0, 255 };
	float* ranges[] = { range };

	IplImage* gray_plane = cvCreateImage(cvGetSize(image), 8, 1);
	cvCvtColor(image, gray_plane, CV_BGR2GRAY);
	CvHistogram* gray_hist = cvCreateHist(1, &hist_size, CV_HIST_ARRAY, ranges, 1);
	cvCalcHist(&gray_plane, gray_hist, 0, 0);

	cvSave("hist.xml", gray_hist);

	//相关：CV_COMP_CORREL      
	//卡方：CV_COMP_CHISQR  
	//直方图相交：CV_COMP_INTERSECT  
	//Bhattacharyya距离：CV_COMP_BHATTACHARYYA  
	//double  com = cvCompareHist(gray_hist, gray_hist2, CV_COMP_BHATTACHARYYA);
	//cout << com << endl;

	return 0;
}