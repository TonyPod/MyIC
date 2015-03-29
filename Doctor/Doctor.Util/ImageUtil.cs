using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Util
{
    public class ImageUtil
    {
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="serverImagePath">图片地址</param>
        /// <param name="thumbnailImagePath">缩略图地址</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <param name="p"></param>
        public static void GetThumbnail(string serverImagePath, string thumbnailImagePath, int width, int height)
        {
            System.Drawing.Image serverImage = System.Drawing.Image.FromFile(serverImagePath);
            //画板大小
            int towidth = width;
            int toheight = height;
            //缩略图矩形框的像素点

            int ow = serverImage.Width;
            int oh = serverImage.Height;

            //if (ow > oh)
            //{
            //    toheight = serverImage.Height * width / serverImage.Width;
            //}
            //else
            //{
            //    towidth = serverImage.Width * height / serverImage.Height;
            //}

            //新建一个bmp图片
            System.Drawing.Image bm = new System.Drawing.Bitmap(width, height);
            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bm);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.White);
            //在指定位置并且按指定大小绘制原图片的指定部分
            //g.DrawImage(serverImage, new System.Drawing.Rectangle((width - towidth) / 2, (height - toheight) / 2, towidth, toheight),
            //    0, 0, ow, oh,
            //    System.Drawing.GraphicsUnit.Pixel);
            g.DrawImage(serverImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                0, 0, ow, oh,
                System.Drawing.GraphicsUnit.Pixel);
            try
            {
                //以jpg格式保存缩略图
                bm.Save(thumbnailImagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                serverImage.Dispose();
                bm.Dispose();
                g.Dispose();
            }
        }
    }
}
