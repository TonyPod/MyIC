#include <opencv\cv.h>
#include <opencv\highgui.h>
#include <opencv2\ml\ml.hpp>

#include <io.h>

using namespace std;
using namespace cv;

//�����ļ�����ʾ�ı�ǩ���ɱ�ǩ����
void GetLabelsMat(vector<char *> fileNames, Mat& mat)
{
	memset(mat.data, 0, mat.dataend - mat.datastart);

	for (int i = 0; i < fileNames.size(); i++)
	{
		char *fileName = fileNames.at(i);

		int j;
		for (j = strlen(fileName) - 1; j >= 0; j--)
		{
			if (fileName[j] == '(')
			{
				break;
			}
		}

		int label = fileName[j - 1] - '0';
		mat.at<double>(i, label) = 1;
	}
}

void ExtFeatureMat(vector<char *> fileNames, Mat& inputsMat)
{
	int nbSamples = fileNames.size();

	//��ŻҶȷֲ�������
	int bins[256];
	float mean;

	IplConvKernel *kernel = cvCreateStructuringElementEx(5, 5, 2, 2, CV_SHAPE_ELLIPSE, NULL);

	//1.��ȡ��������

	for (int i = 0; i < nbSamples; i++)
	{
		char *fileName = fileNames.at(i);

		//ת�Ҷ�
		IplImage *oriImg = cvLoadImage(fileName);
		IplImage *gray = cvCreateImage(cvGetSize(oriImg), IPL_DEPTH_8U, 1);
		cvCvtColor(oriImg, gray, CV_BGR2GRAY);

		//0.Ԥ����
		//���ÿ�����ȥ������
		cvMorphologyEx(gray, gray, NULL, kernel, CV_MOP_OPEN, 1);

		//1.�Ҷ�����
		mean = 0;
		int nb10_100[10] = { 0 };
		memset(bins, 0, sizeof(bins));
		for (int m = 0; m < gray->height; m++)
		{
			for (int n = 0; n < gray->width; n++)
			{
				uchar grayVal = CV_IMAGE_ELEM(gray, uchar, m, n);
				bins[grayVal]++;
				mean += grayVal;
				if (grayVal < 100)
				{
					nb10_100[grayVal / 10]++;
				}
			}
		}
		mean /= (gray->height * gray->width);

		//2.�Ҷȷ�ֵ
		int max = 0;
		int j = 0;
		while (bins[j++] == 0);
		max = j;

		//������
		printf("�ļ�����%s\n", fileName);
		printf("�Ҷ����ģ�%f\n", mean);
		printf("�Ҷȷ�ֵ��%d\n", max);

		//3.Hu�����
		CvMoments moments;
		CvHuMoments hu_moments;
		cvMoments(gray, &moments, 0);
		cvGetHuMoments(&moments, &hu_moments);
		cout << hu_moments.hu1 << endl;
		cout << hu_moments.hu2 << endl;
		cout << hu_moments.hu3 << endl;
		//����õ����׾أ�Խ�����Խ��

		printf("\n");

		//��������
		vector<double> vec;
		vec.push_back(mean);
		vec.push_back(max);
		for (int i = 0; i < 10; i++)
		{
			vec.push_back(nb10_100[i]);
		}
		vec.push_back(hu_moments.hu1);
		vec.push_back(hu_moments.hu2);
		vec.push_back(hu_moments.hu3);

		if (inputsMat.data == NULL)
		{
			inputsMat = Mat(nbSamples, vec.size(), CV_64FC1);
		}
		for (int t = 0; t < vec.size(); t++)
		{
			inputsMat.at<double>(i, t) = vec.at(t);
		}

		//�ͷ���Դ
		cvReleaseImage(&oriImg);
		cvReleaseImage(&gray);
	}

	cvReleaseStructuringElement(&kernel);
}

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

		//���ļ�
		if (_A_SUBDIR != filefind.attrib)
		{
			//�ų�����JPEG��ͼƬ
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
	const char *path = "C:\\Users\\Tony\\Desktop\\ѵ��ͼƬ\\4��\\";
	vector<char *> fileNames = findJpgsInFolder(path);

	CvANN_MLP ann;
	ann.load("params.xml");
	ann.save("params2.xml");

	Mat inputsMat;
	ExtFeatureMat(fileNames, inputsMat);

	Mat outputsMat = Mat(fileNames.size(), 4, CV_64FC1);

	ann.predict(inputsMat, outputsMat);

	//��ʾ������
	for (int i = 0; i < fileNames.size(); i++)
	{
		int maxIdx = 0;
		for (int j = 0; j < 4; j++)
		{
			if (outputsMat.at<double>(i, j) > outputsMat.at<double>(i, maxIdx))
			{
				maxIdx = j;
			}
		}

		//�ҵ�����0-3����
		cout << fileNames.at(i) << endl;
		cout << (maxIdx > 0 ? "ȣ��" : "����") << endl;
		cout << endl;
	}

	system("pause");
	return 0;
}