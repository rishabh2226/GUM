﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUM.DAL.Security
{
    class PasswordEncription
    {
        public static string Encrypt(string password)
        {
            string encryptedPassword = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            encryptedPassword = Convert.ToBase64String(encode);
            return encryptedPassword;
        }
    }
}
