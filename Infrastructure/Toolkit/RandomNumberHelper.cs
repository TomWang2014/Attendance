// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomNumberHelper.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <date>2014/11/5 9:17:41</date>
// <summary>
//   随机数生成辅助类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Toolkit
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 随机数生成辅助类
    /// </summary>
    public class RandomNumberHelper
    {
        /// <summary>
        /// The get random number.
        /// </summary>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        //public static string GetRandomNumber(int length=8)
        //{
        //    Random rand = new Random(Guid.NewGuid().GetHashCode());
        //    string r = rand.Next(99999999).ToString();
        //    var result = string.Empty;
        //    if (r.Length < length)
        //    {        
        //        for (int i = 0; i < length - r.Length; i++)
        //        {
        //            result += "0";//容易重复
        //        }

        //    }
        //    else
        //    {
        //        result = r.Substring(0, length);
        //    }

        //    return result;

        //}

        public static string GetRandomNumber(int length = 8)
        {
            var result = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());
                result.Append(r.Next(0, 10));
            }
            return result.ToString();
        }



        /// <summary>
        /// 根据GUID获取16位的唯一字符串  
        /// </summary>
        /// <returns>string</returns>
        public static string GuidTo16String()
        {
            var i = Guid.NewGuid().ToByteArray().Aggregate<byte, long>(1, (current, b) => current * ((int)b + 1));
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        /// <summary>
        /// 生成随机验证码，返回图片格式
        /// </summary>
        /// <param name="code">
        /// 明文验证码 
        /// </param>
        /// <param name="length">length</param>
        /// <param name="width">
        /// The width. 
        /// </param>
        /// <param name="height">
        /// The height. 
        /// </param>
        /// <returns>
        /// 用户http输出的文件 
        /// </returns>
        public static Bitmap GetValidateCodeMap(out string code, int length, int width = 68, int height = 24)
        {
            code = GetRandomNumber(length);

            var myImage = new Bitmap(70, 30); // 生成一个指定大小的位图
            var graphics = Graphics.FromImage(myImage); // 从一个位图生成一个画布

            graphics.Clear(Color.White); // 清除整个绘画面并以指定的背景色填充,这里是把背景色设为白色
            var random = new Random(); // 实例化一个伪随机数生成器

            // 画图片的前景噪音点,这里有100个
            for (var i = 0; i < 100; i++)
            {
                var x = random.Next(myImage.Width);
                var y = random.Next(myImage.Height);
                myImage.SetPixel(x, y, Color.FromArgb(random.Next())); // 指定坐标为x,y处的像素的颜色
            }

            // 画图片的背景噪音线,这里为2条
            for (var i = 0; i < 0; i++)
            {
                var x1 = random.Next(myImage.Width);
                var x2 = random.Next(myImage.Width);
                var y1 = random.Next(myImage.Height);
                var y2 = random.Next(myImage.Height);
                graphics.DrawLine(new Pen(Color.Black), x1, y1, x2, y2); // 绘制一条坐标x1,y1到坐标x2,y2的指定颜色的线条，这里的线条为黑色
            }

            var font = new Font("Arial", 15, FontStyle.Bold); // 定义特定的文本格式,这里的字体为Arial，大小为15,字体加粗

            // 根据矩形、起始颜色和结束颜色以及方向角度产生一个LinearGradientBrush实例---线性渐变
            var brush =
                new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Rectangle(0, 0, myImage.Width, myImage.Height),

                    // 在坐标0,0处实例化一个和myImage同样大小的矩形
                    Color.Blue,
                    Color.Red,
                    1.2f,
                    true);

            // 绘制文本字符串
            graphics.DrawString(code, font, brush, 2, 2);

            // 绘制有坐标对、宽度和高度指定的矩形---画图片的边框线
            graphics.DrawRectangle(new Pen(Color.Silver), 0, 0, myImage.Width - 1, myImage.Height - 1);

            // 将此图像以指定格式保存到指定的流中
            return myImage;
        }
    }
}
