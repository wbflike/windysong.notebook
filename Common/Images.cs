using SixLabors.ImageSharp;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Images
    {
        /// <summary>
        /// 缩略图生成
        /// </summary>
        /// <param name="inputPath">输入路径包含文件名</param>
        /// <param name="outputPath">输出路径包含文件名</param>
        /// <param name="width">宽度</param>
        /// <param name="hight">高度</param>
        /// <returns></returns>
        public static bool ThumbnailImage(string inputPath,string outputPath,int width,int hight)
        {
            try
            {
                using (Image<Rgba32> image = Image.Load(inputPath))
                {
                    image.Mutate(x => x
                         .Resize(width, hight)//宽度 高度
                         );
                    image.Save(outputPath);
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left">开始裁图的x坐标,相当于用left和top在图片上定位出一个点，走这个点开始裁</param>
        /// <param name="top">开始裁图的y坐标,相当于用left和top在图片上定位出一个点，走这个点开始裁</param>
        /// <param name="width">裁图的宽度</param>
        /// <param name="height">裁图的高度</param>
        /// <param name="inputPath">输入路径包含文件名</param>
        /// <param name="outputPath">输出路径包含文件名</param>
        public static void CutImage(int left, int top, int width, int height, string inputPath, string outputPath)
        {
            using (Image<Rgba32> imageOld = Image.Load<Rgba32>(inputPath))
            {
                imageOld.Clone(img => img.Crop(new Rectangle { X = left, Y = top, Width = width, Height = height })).Save(outputPath);
            }
        }
    }
}
