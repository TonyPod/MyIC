using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MyServer
{
    class ClientThread
    {
        private byte[] buf = new byte[1024];//服务器端设置缓冲区         
        private FileStream fileStream;
        private NetworkStream netStream;
        private TcpClient tcpClient;
        private long readLen;
        private string fileName;

        private string RcvFolder = Settings1.Default["RcvFolder"].ToString();
        private string AnalyzedFolder = Settings1.Default["AnalyzedFolder"].ToString();

        private ClientThread() { }

        /// <summary>
        /// 文件接收线程辅助类构造函数
        /// </summary>
        /// <param name="tcpClient"></param>
        public ClientThread(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
        }

        ///// <summary>
        ///// 回调函数
        ///// </summary>
        ///// <param name="result"></param>
        //private void ReadAsyncCallBack(IAsyncResult result)
        //{
        //    int readCount = netStream.EndRead(result);
        //    fileStream.Write(buf, 0, readCount);
        //    readLen += readCount;

        //    if (readLen == fileLen)
        //    {
        //        fileStream.Close();
        //        Console.WriteLine("File Received: " + fileName);

        //        //算法判断疾病类型
        //        string newFileName = fileName.Substring(0, fileName.LastIndexOf('.')) + "Temp.jpg";
        //        MyServer.Util.Illnesses illType = Util.analyze(fileName, newFileName);
        //        int nbTeeth = illType.nbTeeth;
        //        int[] ints = new int[nbTeeth];
        //        Marshal.Copy(illType.illnesses, ints, 0, nbTeeth);
        //        JObject jObj = new JObject(
        //            new JProperty("userId", 123),
        //            new JProperty("nbTeeth", nbTeeth),
        //            new JProperty("illType", new JArray(ints)));

        //        string jStr = jObj.ToString();
        //        byte[] bytes = Encoding.Default.GetBytes(jStr);

        //        //将json返回给客户端
        //        if (netStream.CanWrite)
        //        {
        //            netStream.Write(bytes, 0, bytes.Length);
        //        }
        //        return;
        //    }
        //    netStream.BeginRead(buf, 0, buf.Length, ReadAsyncCallBack, null);
        //}

        /// <summary>
        /// 开始接收
        /// </summary>
        public void StartReceive()
        {
            try
            {
                netStream = tcpClient.GetStream();
                while (!netStream.DataAvailable) ;

                //读入JSON：用户名，图片名称，图片长度
                byte[] bytes = new byte[1024];
                int readCount = netStream.Read(bytes, 0, bytes.Length);
                string json = Encoding.UTF8.GetString(bytes, 0, readCount);

                //JSON的{}后面可能跟着图片的一些内容，因此要找}并保存后面的东西
                int idx = json.IndexOf('}');
                JObject jObj = null;
                if (idx == json.Length - 1)
                {
                    jObj = JObject.Parse(json);
                }
                else
                {
                    jObj = JObject.Parse(json.Substring(0, idx + 1));
                }

                fileName = jObj["Filename"].ToString();
                string username = jObj["Username"].ToString();
                long fileLen = long.Parse(jObj["Filelen"].ToString());

                if (!Directory.Exists(RcvFolder))
                {
                    Directory.CreateDirectory(RcvFolder);
                }

                fileStream = new FileStream(Path.Combine(RcvFolder, fileName), FileMode.Create);
                if (idx < readCount - 1)
                {
                    fileStream.Write(bytes, idx + 1, readCount - idx - 1);
                    readLen = fileStream.Position;
                }
                else
                {
                    readLen = 0;
                }

                Console.WriteLine("{0} File Name: {1}", DateTime.Now.ToString(), fileName);
                Console.WriteLine("{0} Total Bytes: {1} Bytes", DateTime.Now.ToString(), fileLen);
                Console.WriteLine("{0} Start Receiving...", DateTime.Now.ToString());
                int left = Console.CursorLeft;
                int top = Console.CursorTop;

                //netStream.BeginRead(buf, 0, buf.Length, ReadAsyncCallBack, null);
                while (readLen < fileLen)
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine("Progress: {0}/{1}  {2:g3}%", readLen, fileLen, readLen * 100 / fileLen);
                    readCount = netStream.Read(buf, 0, buf.Length);
                    fileStream.Write(buf, 0, readCount);
                    readLen += readCount;     
                }

                Console.SetCursorPosition(left, top);
                Console.WriteLine("{0} File Received: {1}", DateTime.Now.ToString(), fileName);
                Console.WriteLine();
                fileStream.Close();

                //开始分析
                string newFileName = "T_" + fileName.Substring(0, fileName.LastIndexOf('.')) + ".jpg";
                if (!Directory.Exists(AnalyzedFolder))
                {
                    Directory.CreateDirectory(AnalyzedFolder);
                }

                MyServer.Util.Illnesses illType = Util.analyze(Path.Combine(RcvFolder, fileName), Path.Combine(AnalyzedFolder, fileName));
                int nbTeeth = illType.nbTeeth;
                int[] ints = new int[nbTeeth];
                Marshal.Copy(illType.illnesses, ints, 0, nbTeeth);

                ////组装JSON数组并返回
                JObject jObjSend = new JObject(
                    new JProperty("userId", 123),
                    new JProperty("nbTeeth", nbTeeth),
                    new JProperty("illType", new JArray(ints)));

                string jStr = jObjSend.ToString();
                byte[] jBytes = Encoding.UTF8.GetBytes(jStr);

                //将json返回给客户端
                if (netStream.CanWrite)
                {
                    netStream.Write(jBytes, 0, jBytes.Length);
                }

                //将图片发送给即时通讯服务器
                Msg msg = new Msg("PhotoAnalyzer", username, fileName);
                Util.SendMsgAsync(msg);
                
                //保存分析结果到数据库
                CVResultModel result = new CVResultModel();
                result.Path = fileName;
                result.Teeth_illnesses = string.Join("_", ints.Select(i => i.ToString()).ToArray());
                result.Teeth_num = nbTeeth;
                CVResultDAL.Insert(result);
            }
            catch (Exception e)
            {
                //写入日志
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("发生时间：{0}", DateTime.Now.ToString()).AppendLine();
                builder.AppendFormat("错误原因：{0}", e.ToString()).AppendLine();
                builder.AppendLine();
                Util.WriteToLog(builder.ToString());

                Console.WriteLine("{0} {1} 传输失败", DateTime.Now.ToString(), fileName);
                Console.WriteLine();
            }
        }
    }
}
