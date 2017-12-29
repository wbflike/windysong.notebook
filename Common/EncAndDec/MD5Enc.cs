using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Common.EncAndDec
{
    /// <summary>
    /// md5加密
    /// </summary>
    public class MD5Enc
    {
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="strText">要加密字符串</param>
        /// <returns></returns>
        public static string MD5Encrypt(string strText)
        {
            //MD5 md5 = new MD5CryptoServiceProvider();
            //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(strText);
            //bytes = md5.ComputeHash(bytes);
            //md5.Clear();

            //string ret = "";
            //for (int i = 0; i < bytes.Length; i++)
            //{
            //    ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            //}

            //return ret.PadLeft(32, '0');

            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(strText))).Replace("-", "").ToLower();

        }
    }
}
