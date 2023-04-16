using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

//This code is got from http://stackoverflow.com/questions/10168240/encrypting-decrypting-a-string-in-c-sharp

namespace Bravure.Security
{
    public class SimpleStringCipher
    {
        public static SimpleStringCipher Instance { get; }

        public byte[] InitVectorBytes;

        public static string DefaultPassPhrase { get; set; }

        public static byte[] DefaultInitVectorBytes { get; set; }

        public static byte[] DefaultSalt { get; set; }

        public const int DefaultKeysize = 256;

        static SimpleStringCipher()
        {
            DefaultPassPhrase = "gsKnGZ041HLL4IM8";
            DefaultInitVectorBytes = Encoding.ASCII.GetBytes("jkE49230Tf093b42");
            DefaultSalt = Encoding.ASCII.GetBytes("hgt!16kl");
            Instance = new SimpleStringCipher();
        }

        public SimpleStringCipher()
        {
            InitVectorBytes = DefaultInitVectorBytes;
        }

        public string Encrypt(
            string plainText,
            string passPhrase = null,
            byte[] salt = null,
            int? keySize = null,
            byte[] initVectorBytes = null)
        {
            if (plainText == null)
            {
                return null;
            }

            if (passPhrase == null)
            {
                passPhrase = DefaultPassPhrase;
            }

            if (salt == null)
            {
                salt = DefaultSalt;
            }

            if (keySize == null)
            {
                keySize = DefaultKeysize;
            }

            if (initVectorBytes == null)
            {
                initVectorBytes = InitVectorBytes;
            }

            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, salt))
            {
                var keyBytes = password.GetBytes(keySize.Value / 8);
                using (var symmetricKey = Aes.Create())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                var cipherTextBytes = memoryStream.ToArray();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public string Decrypt(
            string cipherText,
            string passPhrase = null,
            byte[] salt = null,
            int? keySize = null,
            byte[] initVectorBytes = null)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                return null;
            }

            if (passPhrase == null)
            {
                passPhrase = DefaultPassPhrase;
            }

            if (salt == null)
            {
                salt = DefaultSalt;
            }

            if (keySize == null)
            {
                keySize = DefaultKeysize;
            }

            if (initVectorBytes == null)
            {
                initVectorBytes = InitVectorBytes;
            }

            var cipherTextBytes = Convert.FromBase64String(cipherText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, salt))
            {
                var keyBytes = password.GetBytes(keySize.Value / 8);
                using (var symmetricKey = Aes.Create())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                int totalDecryptedByteCount = 0;
                                while (totalDecryptedByteCount < plainTextBytes.Length)
                                {
                                    var decryptedByteCount = cryptoStream.Read(
                                        plainTextBytes,
                                        totalDecryptedByteCount,
                                        plainTextBytes.Length - totalDecryptedByteCount
                                    );

                                    if (decryptedByteCount == 0)
                                    {
                                        break;
                                    }

                                    totalDecryptedByteCount += decryptedByteCount;
                                }

                                return Encoding.UTF8.GetString(plainTextBytes, 0, totalDecryptedByteCount);
                            }
                        }
                    }
                }
            }
        }
    }
}
