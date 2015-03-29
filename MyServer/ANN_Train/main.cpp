#include <opencv2\ml\ml.hpp>
#include <opencv\cv.h>
#include <io.h>
#include <opencv\highgui.h>

using namespace cv;
using namespace std;

void releaseFilesVector(vector<char *>& vec)
{
	int size = vec.size();
	for (int i = 0; i < size; i++)
	{
		free(vec.at(i));
	}
	vec.clear();
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
	int nb20, nb50, nb80;

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
		printf("文件名：%s\n", fileName);
		printf("灰度重心：%f\n", mean);
		printf("灰度峰值：%d\n", max);
		printf("低于20：%d\n", nb20);
		printf("低于50：%d\n", nb50);
		printf("低于80：%d\n", nb80);

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
		vec.push_back(nb20);
		vec.push_back(nb50);
		vec.push_back(nb80);
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

int main(void)
{
	const char *folderPath = "C:\\Users\\Tony\\Desktop\\训练图片\\4种\\";
	const int nbKind = 4;
	vector<char *> fileNames = findJpgsInFolder(folderPath);
	
	//1.提取特征向量
	Mat inputsMat;
	ExtFeatureMat(fileNames, inputsMat);
	
	//2.提取分类结果
	Mat outputsMat = Mat(fileNames.size(), nbKind, CV_64FC1);
	GetLabelsMat(fileNames, outputsMat);

	//2.ANN学习
	CvANN_MLP ann;
	CvANN_MLP_TrainParams params;
	params.train_method = CvANN_MLP_TrainParams::BACKPROP;
	params.bp_dw_scale = 0.1;
	params.bp_moment_scale = 0.1;

	//Mat layerSizes(1, 3, CV_32SC1, {2, 5, 1});
	Mat layerSizes = (Mat_<int>(1, 4) << inputsMat.cols, 10, 10, outputsMat.cols);
	ann.create(layerSizes, CvANN_MLP::SIGMOID_SYM);
	ann.train(inputsMat, outputsMat, Mat(), Mat(), params);
	ann.save("params.xml");

	Mat inputsMat2 = inputsMat.clone();
	Mat outputsMat2 = Mat(outputsMat.rows, outputsMat.cols, CV_64FC1);

	CvANN_MLP ann2;
	ann2.load("params.xml");
	ann2.predict(inputsMat2, outputsMat2);

	int nbCorrect = 0;
	int nbCorrect2 = 0;
	int realClass = 0;

	for (int i = 0; i < fileNames.size(); i++)
	{
		cout << fileNames.at(i) << endl;
		int maxIdx = 0;
		for (int j = 1; j < outputsMat2.cols; j++)
		{
			if (outputsMat2.at<double>(i, j) > outputsMat2.at<double>(i, maxIdx))
			{
				maxIdx = j;
			}
		}

		//检查文件名开头数字是否与分类结果相同
		int k = strlen(fileNames.at(i)) - 1;
		while (fileNames.at(i)[k] != '(')
			--k;

		realClass = fileNames.at(i)[k - 1] - '0';
		if (realClass == maxIdx)
			++nbCorrect;
		if (!((!realClass) ^ (!maxIdx)))
			++nbCorrect2;

		cout << maxIdx << endl;
		cout << endl;
	}

	//统计错误率
	cout << "4分类正确率：" << nbCorrect * 100 / fileNames.size() << "%" << endl;
	cout << "2分类正确率：" << nbCorrect2 * 100 / fileNames.size() << "%" << endl;
	
	system("pause");
	return 0;
}