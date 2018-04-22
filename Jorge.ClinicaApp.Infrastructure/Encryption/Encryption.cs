using System;
using System.Security.Cryptography;
using System.Text;

namespace Jorge.ClinicaApp.Infrastructure.Encryption
{
    public class Encryption
    {

        private const string Key = "EKaSFsdVeniz7FdLLoHXJKasddeasdZj0E96ooQzwqbsNwerweasLaSyq35prPG2";

        private const string Salt = "&Test&";

        public static string DecryptTripleDes(string base64Text)
        {
            var decTripleDes = String.Empty;
            if (!string.IsNullOrEmpty(base64Text))
            {
                byte[] buffer = new byte[0];
                var des = new TripleDESCryptoServiceProvider();
                var hashMd5 = new MD5CryptoServiceProvider();
                des.Key = hashMd5.ComputeHash(Encoding.ASCII.GetBytes(Key));
                des.Mode = CipherMode.ECB;
                var desDecrypt = des.CreateDecryptor();
                buffer = Convert.FromBase64String(base64Text);
                decTripleDes = ASCIIEncoding.ASCII.GetString(desDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));
                if (((Salt.Length > 0)
                            && decTripleDes.EndsWith(Salt, StringComparison.OrdinalIgnoreCase)))
                {
                    decTripleDes = decTripleDes.Substring(0, (decTripleDes.Length - Salt.Length));
                }

            }

            return decTripleDes;
        }

        public static string EncryptTripleDes(string plaintext)
        {
            var tripleDes = String.Empty;
            if (!string.IsNullOrEmpty(plaintext))
            {
                if ((Salt.Length > 0))
                {
                    plaintext = (plaintext + Salt);
                }

                byte[] buffer = new byte[0];
                var des = new TripleDESCryptoServiceProvider();
                var hashMd5 = new MD5CryptoServiceProvider();
                des.Key = hashMd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Key));
                des.Mode = CipherMode.ECB;
                var desEncrypt = des.CreateEncryptor();
                buffer = ASCIIEncoding.ASCII.GetBytes(plaintext);
                tripleDes = Convert.ToBase64String(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            }

            return tripleDes;
        }
    }
}
