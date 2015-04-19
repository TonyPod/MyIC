#include <opencv\cv.h>
#include <opencv\highgui.h>
#include <opencv2\ml\ml.hpp>

#include <io.h>

using namespace std;
using namespace cv;

//根据文件名显示的标签生成标签矩阵
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

	//存放灰度分布的数组
	int bins[256];
	float mean;

	IplConvKernel *kernel = cvCreateStructuringElementEx(5, 5, 2, 2, CV_SHAPE_ELLIPSE, NULL);

	//1.提取特征向量

	for (int i = 0; i < nbSamples; i++)
	{
		char *fileName = fileNames.at(i);

		//转灰度
		IplImage *oriImg = cvLoadImage(fileName);
		IplImage *gray = cvCreateImage(cvGetSize(oriImg), IPL_DEPTH_8U, 1);
		cvCvtColor(oriImg, gray, CV_BGR2GRAY);

		//0.预处理
		//利用开操作去除亮斑
		cvMorphologyEx(gray, gray, NULL, kernel, CV_MOP_OPEN, 1);

		//1.灰度重心
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

		//2.灰度峰值
		int max = 0;
		int j = 0;
		while (bins[j++] == 0);
		max = j;

		//输出结果
		printf("文件名：%s\n", fileName);
		printf("灰度重心：%f\n", mean);
		printf("灰度峰值：%d\n", max);

		//3.Hu不变矩
		CvMoments moments;
		CvHuMoments hu_moments;
		cvMoments(gray, &moments, 0);
		cvGetHuMoments(&moments, &hu_moments);
		cout << hu_moments.hu1 << endl;
		cout << hu_moments.hu2 << endl;
		cout << hu_moments.hu3 << endl;
		//最多用到三阶矩（越大误差越大）

		printf("\n");

		//特征向量
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

		//释放资源
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
	const char *path = "C:\\Users\\Tony\\Desktop\\训练图片\\4种\\";
	vector<char *> fileNames = findJpgsInFolder(path);

	CvANN_MLP ann;
	ann.load("params.xml");
	ann.save("params2.xml");

	Mat inputsMat;
	ExtFeatureMat(fileNames, inputsMat);

	Mat outputsMat = Mat(fileNames.size(), 4, CV_64FC1);

	ann.predict(inputsMat, outputsMat);

	//显示分类结果
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

		//找到属于0-3最大的
		cout << fileNames.at(i) << endl;
		cout << (maxIdx > 0 ? "龋齿" : "正常") << endl;
		cout << endl;
	}

	system("pause");
	return 0;
}