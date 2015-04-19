using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Doctor
{
    public static class ClassEx
    {
        /// <summary>
        /// 增加一行
        /// </summary>
        public static void AppendLine(this RichTextBox rtb)
        {
            rtb.AppendText(Environment.NewLine);
            rtb.SelectionStart = rtb.TextLength;
        }

        /// <summary>
        /// 增加一行
        /// </summary>
        public static void AppendLine(this RichTextBox rtb, string text)
        {
            rtb.AppendText(text + Environment.NewLine);
            rtb.SelectionStart = rtb.TextLength;
        }

        /// <summary>
        /// 向RichTextBox中追加指定样式的文本
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="foreColor"></param>
        public static void AppendLine(this RichTextBox rtb, string text, Font font, Color foreColor, HorizontalAlignment alignment = HorizontalAlignment.Left)
        {
            text = text + Environment.NewLine;

            int start = rtb.TextLength;
            int len = text.Length;

            rtb.AppendText(text);
            rtb.SelectionStart = start;
            rtb.SelectionLength = len;
            rtb.SelectionAlignment = alignment;
            rtb.SelectionColor = foreColor;
            rtb.SelectionFont = font;

            rtb.SelectionStart = rtb.TextLength;
        }

        /// <summary>
        /// 检查TextBox为空的扩展方法
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="trim">是否去除空格再检查</param>
        /// <param name="msg">为空时的信息</param>
        /// <returns></returns>
        public static bool CheckTextBoxEmpty(this TextBox textBox, bool trim, string msg)
        {
            string content = textBox.Text;
            if (trim)
            {
                content = content.Trim();
            }

            if (string.IsNullOrEmpty(content))
            {
                MessageBox.Show(msg);
                textBox.Focus();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除StringBuilder尾部的一个回车
        /// </summary>
        /// <param name="builder"></param>
        public static void RemoveLine(this StringBuilder builder)
        {
            string newLine = Environment.NewLine;
            string str = builder.ToString();
            if (str.Length < newLine.Length)
            {
                return;
            }

            if(newLine.Equals(str.Substring(str.Length - newLine.Length)))
            {
                builder.Remove(builder.Length - newLine.Length, newLine.Length);
            }
        }

        /// <summary>
        /// 将流用UTF8解析为字符串
        /// </summary>
        /// <param name="stream"></param>
        public static string ToUTF8String(this Stream stream)
        {
            using(StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// 将字符串以UTF8写入流
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="content"></param>
        public static void WriteUTF8String(this Stream stream, string content)
        {
            byte[] buf = Encoding.UTF8.GetBytes(content);
            stream.Write(buf, 0, buf.Length);
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FirstLetterUpper(this string str)
        {

            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }

        /// <summary>
        /// 改变图片分辨率
        /// </summary>
        /// <param name="img"></param>
        /// <param name="newW"></param>
        /// <param name="newH"></param>
        /// <returns></returns>
        public static Bitmap KiResizeImage(this Image img)
        {
            try
            {
                int screenW = Screen.PrimaryScreen.Bounds.Width;
                int screenH = Screen.PrimaryScreen.Bounds.Height;

                int imgW = img.Width;
                int imgH = img.Height;

                float ratio = screenW / (float)screenH;
                float imgRatio = imgW / (float)imgH;

                int newW = imgW;
                int newH = imgH;

                //判断图片是水平过长还是垂直过长
                if (imgW > screenW && imgH <= screenH)
                {
                    newW = screenW;
                    newH = newW * imgH / imgW;
                }
                else if (imgW <= screenW && imgH > screenH)
                {
                    newH = screenH;
                    newW = newH * imgW / imgH;
                }
                else if (imgW > screenW && imgH > screenH)
                {
                    if (imgRatio > ratio)
                    {
                        newW = screenW;
                        newH = newW * imgH / imgW;
                    }
                    else
                    {
                        newH = screenH;
                        newW = newH * imgW / imgH;
                    }
                }

                Bitmap bmp = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(img);

                // 插值算法的质量
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, imgW, imgH), GraphicsUnit.Pixel);
                g.Dispose();

                return bmp;
            }
            catch
            {
                return null;
            }
        }
    }
}
