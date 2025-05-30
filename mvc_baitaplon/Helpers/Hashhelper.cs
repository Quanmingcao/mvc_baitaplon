using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace mvc_baitaplon.Helpers
{

    public static class HashHelper
    {
        public static string ToSHA256(string text)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder sb = new StringBuilder();

                foreach (var b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }

}