using dotnetcore.api.model.template.Constants;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace dotnetcore.api.model.template.Common
{
    public class Security
    {
        #region "Properties"

        public string PassPhrase { get; set; }
        public string UserId { get; set; }

        #endregion
    }

    public static class Securities
    {
        public static string Encrypt(this string str, string passPhrase)
        {
            if (passPhrase.Length > 16)
                throw new Exception(ErrorMessages.PassPhraseLength);

            if (passPhrase.Length < 16)
            {
                for (int i = 0; passPhrase.Length < 16; i++)
                {
                    var arr = passPhrase.ToCharArray();
                    passPhrase = passPhrase + arr[i];
                }
            }

            var key = Encoding.UTF8.GetBytes(passPhrase);

            using (var aes = Aes.Create())
            {
                using (var encryptor = aes.CreateEncryptor(key, aes.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(str);
                        }

                        byte[] iv = aes.IV;
                        byte[] decryptedContent = msEncrypt.ToArray();
                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }

        }

        public static string Decrypt(this string str, string passPhrase)
        {
            if (passPhrase.Length > 16)
                throw new Exception(ErrorMessages.PassPhraseLength);

            if (passPhrase.Length < 16)
            {
                for (int i = 0; passPhrase.Length < 16; i++)
                {
                    char[] arr = passPhrase.ToCharArray();
                    passPhrase += arr[i];
                }
            }

            byte[] fullCipher = Convert.FromBase64String(str);
            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);

            byte[] key = Encoding.UTF8.GetBytes(passPhrase);

            using (var aes = Aes.Create())
            {
                using (var decryptor = aes.CreateDecryptor(key, iv))
                {
                    string result;

                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }

        }

        public static string Mask(this string str, char? character = '*')
        {
            if (str == null)
                throw new ArgumentNullException("string is null");
            string mask = string.Empty;

            for (var i = 0; i < str.Length; i++)
            {
                mask = mask + character;
            }
            return mask;
        }
    }
}
