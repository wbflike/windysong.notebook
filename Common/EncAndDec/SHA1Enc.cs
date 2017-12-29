using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Common.EncAndDec
{
    /// <summary>
    /// sha1加密
    /// </summary>
    public class SHA1Enc
    {
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="strText">加密字符串</param>
        /// <returns></returns>
        public static string SHA1Encrypt(string strText)
        {

            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = UTF8Encoding.Default.GetBytes(strText);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            //str_sha1_out = str_sha1_out.Replace("-", "");
            return str_sha1_out.Replace("-", "").ToLower();
        }
    }
}
