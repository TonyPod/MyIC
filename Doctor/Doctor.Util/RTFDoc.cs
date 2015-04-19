using Doctor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Util
{
    /// <summary>
    /// 针对即时通讯RichTextBox的格式的辅助类
    /// </summary>
    public class RTFDoc
    {
        public enum MsgTypeEnum
        {
            Rcv = 0,
            Send = 1
        }

        private const string DOC_FORMAT = "{\\rtf1\\ansi\\ansicpg936\\deff0\\deflang1033\\deflangfe2052{\\fonttbl{\\f0\\fnil\\fcharset134 \\'cb\\'ce\\'cc\\'e5;}}";
        private const string COLORS = "{\\colortbl ;\\red245\\green185\\blue153;\\red255\\green255\\blue255;\\red245\\green252\\blue255;\\red203\\green203\\blue203;\\red240\\green248\\blue255;}";
        private const string VIEW_FORMAT = "\\viewkind4\\uc1\\lang2052\\f0\\fs18";

        private StringBuilder builder = new StringBuilder();

        /// <summary>
        /// 根据即时通讯消息内容写入RTF文件
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        public void InsertMsg(Msg msg, MsgTypeEnum type)
        {
            switch (type)
            {
                case MsgTypeEnum.Rcv:
                    if (msg.Record_id.HasValue)
                    {
                        builder.AppendFormat("\\pard\\cf4 {0} {1}\\par",
                            msg.Time.ToString(), GetAsciiString(msg.From)).AppendLine();
                        //如果信息中有Record_id，则设置为超链接
                        builder.AppendFormat("{{\\field {{\\*\\fldinst HYPERLINK \"{0}\"}}{{\\fldrslt \\cf5\\ul{1} }}}}\\par",
                            "http://Records/" + msg.Record_id.Value.ToString(),
                            GetAsciiString(string.Format("自检编号{0}", msg.Record_id.Value))).AppendLine();
                        builder.AppendLine("\\pard\\cf3\\ulnone\\par").AppendLine();
                    }
                    else
                    {
                        builder.AppendFormat("\\pard\\cf4 {0} {1}\\par",
                            msg.Time.ToString(), GetAsciiString(msg.From)).AppendLine();
                        builder.AppendFormat("\\cf2 {0}\\par", GetAsciiString(msg.Content)).AppendLine();
                        AppendLine();
                    }
                    break;
                case MsgTypeEnum.Send:
                    builder.AppendFormat("\\pard\\cf1 {0} {1}\\par",
                        msg.Time.ToString(), GetAsciiString(msg.From)).AppendLine();
                    builder.AppendFormat("\\cf2 {0}\\par", GetAsciiString(msg.Content)).AppendLine();
                    AppendLine();
                    break;
                default:
                    break;
            }
        }

        private void AppendLine()
        {
            builder.AppendLine("\\pard\\cf3\\par");
        }

        public override string ToString()
        {
            return DOC_FORMAT + COLORS + VIEW_FORMAT + builder.ToString() + "}";
        }

        /// <summary>
        /// 从默认编码（中国默认：GBK）转换到Ascii的编码形式（详见RTF文件规范）
        /// 示例：宋体abc -> \'cb\'ce\'cc\'e5abc
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetAsciiString(string input)
        {
            byte[] buf = Encoding.Default.GetBytes(input);
            StringBuilder builder = new StringBuilder();

            int i = 0;
            while (i < buf.Length)
            {
                //如果该字节大于0x7f（首位为1），则与后面一个字节共同构成一个字符
                if (buf[i] > 0x7f)
                {
                    builder.AppendFormat("\\'{0:x2}", buf[i++]);
                    builder.AppendFormat("\\'{0:x2}", buf[i++]);
                }
                else
                {
                    builder.Append((char)buf[i++]);
                }
            }

            return builder.ToString();
        }
    }
}
