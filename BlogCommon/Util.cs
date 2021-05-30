using System;
using System.Security.Cryptography;
using System.Text;

namespace BlogCommon
{
    public class Util
    {
        static MD5 md5 = MD5.Create();
        public static  string MD5Encrpy(string destStrVal)
        {
            byte[] bufferMd5 = md5.ComputeHash(Encoding.UTF8.GetBytes(destStrVal));
            StringBuilder sbMd5 = new StringBuilder();
            foreach (byte itemBuf in bufferMd5)
            {
                sbMd5.Append(itemBuf.ToString("X"));
            }
            return sbMd5.ToString();
        }
    }
}
