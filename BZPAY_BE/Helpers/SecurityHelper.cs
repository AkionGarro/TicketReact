using BZPAY_BE.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace BZPAY_BE.Helpers
{
    public static class SecurityHelper
    {
        /// Encript a string.
        public static string Encript(string _cadenaAEncript)
        {
            string result = string.Empty;
            byte[] encryted = Encoding.UTF8.GetBytes(_cadenaAEncript);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// unencript a string.
        public static string Decript(string _cadenaADecript)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaADecript);
            result = Encoding.UTF8.GetString(decryted);
            return result;
        }

        public static string EncodePassword(string pass, int passwordFormat, string salt)
        {
            if (passwordFormat == 0)
                return pass;
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            byte[] src = Convert.FromBase64String(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            byte[] inArray = null;
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            if (passwordFormat == 1)
            {
                HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
                if ((algorithm == null))
                {
                    throw new Exception("Invalid HashAlgoritm");
                }
                inArray = algorithm.ComputeHash(dst);
            }
            return Convert.ToBase64String(inArray);
        }






    }
}
