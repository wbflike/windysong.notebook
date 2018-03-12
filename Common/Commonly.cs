using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Commonly
    {
        public static string GetRandomString(int iLength)
        {
            string buffer = "0123456789";// 随机字符中也可以为汉字（任何）  
            StringBuilder sb = new StringBuilder();
            Random r = new Random();
            int range = buffer.Length;
            for (int i = 0; i < iLength; i++)
            {
                sb.Append(buffer.Substring(r.Next(range), 1));
            }
            return sb.ToString();
        }
    }
}
