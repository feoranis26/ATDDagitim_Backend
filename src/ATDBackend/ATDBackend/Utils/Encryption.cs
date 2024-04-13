using System.Security.Cryptography;
using System.Text;

namespace ATDBackend.Utils
{
    internal static class Encryption
    {
        public static class TripleDES
        {
            public static byte[] Encrypt(byte[] data, string iv, string secret)
            {
                byte[] encryptedBytes;
                using (var des = new TripleDESCryptoServiceProvider())
                {
                    des.IV = Encoding.UTF8.GetBytes(iv);
                    des.Key = Encoding.UTF8.GetBytes(secret);
                    des.Mode = CipherMode.CBC; // Cipher Block Chaining
                    des.Padding = PaddingMode.PKCS7; // Padding mode

                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(data, 0, data.Length);
                            cs.FlushFinalBlock();
                        }
                        encryptedBytes = ms.ToArray();
                    }
                }
                return encryptedBytes;
            }

            public static byte[] Decrypt(byte[] data, string iv, string secret)
            {
                byte[] decryptedBytes;
                using (var des = new TripleDESCryptoServiceProvider())
                {
                    des.IV = Encoding.UTF8.GetBytes(iv);
                    des.Key = Encoding.UTF8.GetBytes(secret);
                    des.Mode = CipherMode.CBC;
                    des.Padding = PaddingMode.PKCS7;

                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(data, 0, data.Length);
                            cs.FlushFinalBlock();
                        }
                        decryptedBytes = ms.ToArray();
                    }
                }
                return decryptedBytes;
            }
        }
    }
}
