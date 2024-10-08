﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PrecierosEC.Core.Utiliies
{
#pragma warning disable CA1041 // Proporcionar un mensaje ObsoleteAttribute
    [Obsolete]
#pragma warning restore CA1041 // Proporcionar un mensaje ObsoleteAttribute
    public class DbConnectionHelper
    {
        public class ConnectionInfo
        {
            public string ConnectionString { get; internal set; }
            public string Password { get; internal set; }
            public string UserId { get; internal set; }
        }

        public static int ObtenSaltTam(byte[] bytClave)
        {
            var key = new Rfc2898DeriveBytes(bytClave, bytClave, 1000);
            byte[] ba = key.GetBytes(2);
            StringBuilder sb = new();
            for (int i = 0; i < ba.Length; i++)
            {
                sb.Append(Convert.ToInt32(ba[i]));
            }
            int saltSize = 0;
            string s = sb.ToString();
            foreach (char c in s)
            {
                int intc = Convert.ToInt32(c.ToString());
                saltSize += intc;
            }

            return saltSize;
        }

        public static string FnDesencripta(string strEncriptado, Int32? modulo = 0)
        {
            string strEncripUsuario = "";
            string cadenausuario = "";
            switch (modulo)
            {
                case 61: // CREDITO WEB
                    strEncripUsuario = "IQ1SJ8afVt/0mkTIIbfx5sEqe/nw0dRE8IrOs3UxEpTVrILxv/aNnilZ71QLaA+JbqV7lhZO1g7tg9QbHCpCfQ==";
                    break;
            }

            string cadenaconn = FnDesencriptaAlgoritmo(strEncriptado);

            if (!string.IsNullOrEmpty(strEncripUsuario))
                cadenausuario = FnDesencriptaAlgoritmo(strEncripUsuario);

            return cadenaconn + " " + cadenausuario;
        }

        private static string FnDesencriptaAlgoritmo(string strEncriptado)
        {
            string strClave;
            strClave = "1Au2Rn3Ti4Ec5Fo6Am7Ce8Tr9A";
            byte[] bytEncriptado = Convert.FromBase64String(strEncriptado);
            byte[] originalclave = Encoding.UTF8.GetBytes(strClave);
            originalclave = SHA256.Create().ComputeHash(originalclave);

            byte[] bytDesencriptado = AESDesencripta(bytEncriptado, originalclave);

            int saltSize = ObtenSaltTam(originalclave);

            byte[] originalBytes = new byte[bytDesencriptado.Length - saltSize];
            for (int i = saltSize; i < bytDesencriptado.Length; i++)
            {
                originalBytes[i - saltSize] = bytDesencriptado[i];
            }

            return Encoding.UTF8.GetString(originalBytes);
        }
        
        private static byte[] AESDesencripta(byte[] bytEncriptado, byte[] bytLlave)
        {
            byte[] bytEncriptados = null;
            byte[] saltBytes = bytLlave;

            using (MemoryStream ms = new())
            {
                using RijndaelManaged rjmAlgoritmo = new();
                rjmAlgoritmo.KeySize = 256;
                rjmAlgoritmo.BlockSize = 128;

                var key = new Rfc2898DeriveBytes(bytLlave, saltBytes, 1000);
                rjmAlgoritmo.Key = key.GetBytes(rjmAlgoritmo.KeySize / 8);
                rjmAlgoritmo.IV = key.GetBytes(rjmAlgoritmo.BlockSize / 8);

                rjmAlgoritmo.Mode = CipherMode.CBC;

                using (CryptoStream cs = new(ms, rjmAlgoritmo.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytEncriptado, 0, bytEncriptado.Length);
                    cs.Close();
                }
                bytEncriptados = ms.ToArray();
            }

            return bytEncriptados;
        }
    }
}
