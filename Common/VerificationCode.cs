using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Pens;
using SixLabors.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace Common
{
    public class VerificationCode
    {
        /// <summary>
        /// 画点+画字=验证码byte[]
        /// </summary>
        /// <param name="content">验证码</param>
        /// <param name="outImgPath">输出图片路径</param>
        /// <param name="x">图片宽度</param>
        /// <param name="y">图片高度</param>
        public static byte[] GetValidCodeByte(string content = "0512", string fontFilePath= "WRYH.ttf", int xx = 150, int yy = 25)
        {
            var bb = default(byte[]);
            try
            {
                var dianWith = 1; //点宽度
                var xx_space = 10;  //点与点之间x坐标间隔
                var yy_space = 5;    //y坐标间隔
                var wenZiLen = content.Length;  //文字长度
                var maxX = xx / wenZiLen; //每个文字最大x宽度
                var prevWenZiX = 0; //前面一个文字的x坐标
                var size = 16;//字体大小

                //字体
                var install_Family = new FontCollection().Install(
                  fontFilePath
                  //@"C:\Windows\Fonts\STKAITI.TTF"   //windows系统下字体文件
                  );
                var font = new Font(install_Family, size);  //字体

                //点坐标
                var listPath = new List<IPath>();
                for (int i = 0; i < xx / xx_space; i++)
                {
                    for (int j = 0; j < yy / yy_space; j++)
                    {
                        var position = new Vector2(i * xx_space, j * yy_space);
                        var linerLine = new LinearLineSegment(position, position);
                        var shapesPath = new SixLabors.Shapes.Path(linerLine);
                        listPath.Add(shapesPath);
                    }
                }

                //画图
                using (Image<SixLabors.ImageSharp.Rgba32> image = new Image<Rgba32>(xx, yy))   //画布大小
                {
                    image.Mutate(x =>
                    {
                        var imgProc = x;

                        //逐个画字
                        for (int i = 0; i < wenZiLen; i++)
                        {
                            //当前的要输出的字
                            var nowWenZi = content.Substring(i, 1);

                            //文字坐标
                            var wenXY = new Vector2();
                            var maxXX = prevWenZiX + (maxX - size);
                            wenXY.X = new Random().Next(prevWenZiX, maxXX);
                            wenXY.Y = new Random().Next(0, yy - size);

                            prevWenZiX = Convert.ToInt32(Math.Floor(wenXY.X)) + size;

                            //画字
                            imgProc.DrawText(
                                   nowWenZi,   //文字内容
                                   font,
                                   i % 2 > 0 ? Rgba32.HotPink : Rgba32.Red,
                                   wenXY,
                                   TextGraphicsOptions.Default);
                        }

                        //画点 
                        imgProc.BackgroundColor(Rgba32.WhiteSmoke).   //画布背景
                                     Draw(
                                     Pens.Dot(Rgba32.HotPink, dianWith),   //大小
                                     new SixLabors.Shapes.PathCollection(listPath)  //坐标集合
                                 );
                    });
                    using (MemoryStream stream = new MemoryStream())
                    {
                        image.SaveAsPng(stream);
                        bb = stream.GetBuffer();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return bb;
        }

    }
}
