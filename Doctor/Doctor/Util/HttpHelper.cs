using Doctor.Model;
using Doctor.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Doctor
{
    class HttpHelper
    {
        private static readonly string ServerBaseUri = Properties.Settings.Default.ServerBaseUrl;

        /// <summary>
        /// 异步发送HTTP请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task<string> ConnectionForResultAsync(string url, string content, CancellationToken token)
        {
            return Task.Factory.StartNew(() =>
            {
                url = ServerBaseUri + url;
                string result = "";
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "POST";
                    request.Headers.Add("Accept-Encoding", "gzip, deflate");
                    using (Stream stream = request.GetRequestStream())
                    {
                        byte[] data = Encoding.UTF8.GetBytes(content);
                        stream.Write(data, 0, data.Length);
                    }

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.ContentEncoding == "gzip")
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (GZipStream zipStream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                            {
                                byte[] buf = new byte[1024];
                                int readCount = zipStream.Read(buf, 0, buf.Length);
                                while (readCount > 0)
                                {
                                    memoryStream.Write(buf, 0, readCount);
                                    readCount = zipStream.Read(buf, 0, buf.Length);
                                }
                            }
                            result = Encoding.UTF8.GetString(memoryStream.ToArray());
                        }
                    }
                    else
                    {
                        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                        result = reader.ReadToEnd();
                    }
                    return result;
                }
                catch (Exception)
                {
                    return null;
                }
            }, token);
        }

        /// <summary>
        /// 异步发送HTTP请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Task<string> ConnectionForResultAsync(string url, string content)
        {
            return Task.Factory.StartNew(() =>
            {
                url = ServerBaseUri + url;
                string result = "";
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "POST";
                    request.Headers.Add("Accept-Encoding", "gzip, deflate");
                    using (Stream stream = request.GetRequestStream())
                    {
                        byte[] data = Encoding.UTF8.GetBytes(content);
                        stream.Write(data, 0, data.Length);
                    }

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.ContentEncoding == "gzip")
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (GZipStream zipStream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                            {
                                byte[] buf = new byte[1024];
                                int readCount = zipStream.Read(buf, 0, buf.Length);
                                while (readCount > 0)
                                {
                                    memoryStream.Write(buf, 0, readCount);
                                    readCount = zipStream.Read(buf, 0, buf.Length);
                                }
                            }
                            result = Encoding.UTF8.GetString(memoryStream.ToArray());
                        }
                    }
                    else
                    {
                        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                        result = reader.ReadToEnd();
                    }
                    return result;
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }

        /// <summary>
        /// 发送Http请求到指定url并返回结果
        /// </summary>
        /// <param name="url">指定url地址：一个ashx文件</param>
        /// <param name="content">发送给服务器的内容</param>
        /// <param name="handler">回调函数</param>
        /// <returns>null：连接失败 其他：连接成功，返回服务器相应</returns>
        public static string ConnectionForResult(string url, string content)
        {
            url = ServerBaseUri + url;
            string result = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                using (Stream stream = request.GetRequestStream())
                {
                    byte[] data = Encoding.UTF8.GetBytes(content);
                    stream.Write(data, 0, data.Length);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.ContentEncoding == "gzip")
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (GZipStream zipStream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                        {
                            byte[] buf = new byte[1024];
                            int readCount = zipStream.Read(buf, 0, buf.Length);
                            while (readCount > 0)
                            {
                                memoryStream.Write(buf, 0, readCount);
                                readCount = zipStream.Read(buf, 0, buf.Length);
                            }
                        }
                        result = Encoding.UTF8.GetString(memoryStream.ToArray());
                    }
                }
                else
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    result = reader.ReadToEnd();
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 上传文件：可用于上传图片
        /// </summary>
        /// <param name="url">指定url地址：一个ashx文件</param>
        /// <param name="fileName">要上传文件的本地路径</param>
        /// <returns>上传成功则返回GUID字符串（用于唯一标识一个图片）</returns>
        public static string UploadFile(string url, string fileName)
        {
            url = ServerBaseUri + url;
            if (!File.Exists(fileName))
            {
                throw new Exception("File not Exist");
            }

            try
            {
                using (WebClient client = new WebClient())
                {
                    byte[] bytes = client.UploadFile(url, "POST", fileName);
                    string guidStr = Encoding.UTF8.GetString(bytes);
                    return guidStr;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 上传文件：可用于上传图片
        /// </summary>
        /// <param name="url">指定url地址：一个ashx文件</param>
        /// <param name="fileName">要上传文件的本地路径</param>
        /// <returns>上传成功则返回GUID字符串（用于唯一标识一个图片）</returns>
        public static Task<string> UploadFileAsync(string url, string fileName)
        {
            return Task.Factory.StartNew(() =>
            {
                url = ServerBaseUri + url;
                if (!File.Exists(fileName))
                {
                    throw new Exception("File not Exist");
                }

                try
                {
                    using (WebClient client = new WebClient())
                    {
                        byte[] bytes = client.UploadFile(url, "POST", fileName);
                        string guidStr = Encoding.UTF8.GetString(bytes);
                        return guidStr;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }

        /// <summary>
        /// 下载文件：用于下载个人信息的用户照片和执照
        /// </summary>
        /// <param name="url">指定url地址：一个ashx文件</param>
        /// <param name="fileName">DoctorModel类中的文件路径</param>
        /// <returns>下载成功与否</returns>
        public static bool DownloadFile(string url, string fileName)
        {
            //判断本地是否有图片缓存，如是则不用下载图片
            string dirPath = GeneralHelper.DownloadPicFolder;
            string newFilePath = Path.Combine(dirPath, fileName);
            if (File.Exists(newFilePath))
            {
                return true;
            }

            try
            {
                //下载图片
                url = ServerBaseUri + url;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                byte[] fileNameBytes = Encoding.UTF8.GetBytes(fileName);
                request.Method = "POST";
                request.GetRequestStream().Write(fileNameBytes, 0, fileNameBytes.Length);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();

                //存放文件夹
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                //写入文件
                using (FileStream fileStream = new FileStream(newFilePath, 
                    FileMode.Create, FileAccess.Write))
                {
                    byte[] buf = new byte[1024];
                    int readCount = responseStream.Read(buf, 0, buf.Length);
                    while (readCount > 0)
                    {
                        fileStream.Write(buf, 0, readCount);
                        readCount = responseStream.Read(buf, 0, buf.Length);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                if (File.Exists(newFilePath))
                {
                    File.Delete(newFilePath);
                }
                return false;
            }
        }

        /// <summary>
        /// 通过IP地址获取区域位置（连接新浪IP数据库）
        /// </summary>
        /// <param name="ipAddr"></param>
        /// <returns></returns>
        public static IPRecord GetIPRecord()
        {
            try
            {
                string url = "http://180.149.136.250/iplookup/iplookup.php?format=json";
                //string url = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=json";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet));
                string responseStr = reader.ReadToEnd();
                IPRecord ipRecord = JsonConvert.DeserializeObject<IPRecord>(responseStr);

                return ipRecord;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
