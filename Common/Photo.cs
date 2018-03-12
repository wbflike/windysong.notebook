using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Photo
    {
        /// <summary>
        /// 缩略图生成
        /// </summary>
        /// <param name="inputPath">输入路径包含文件名</param>
        /// <param name="outputPath">输出路径包含文件名</param>
        /// <param name="width">宽度</param>
        /// <param name="hight">高度</param>
        /// <returns></returns>
        public static bool GetThumbnail(string inputPath,string outputPath,int width,int hight)
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
    }
}
