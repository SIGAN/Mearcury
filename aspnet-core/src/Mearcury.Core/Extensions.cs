using Abp.Runtime.Security;
using System.Text;

namespace Mearcury
{
    public static class Extensions
    {
        /// <summary>
        /// Encrypts <paramref name="plainText"/> with <paramref name="password"/> and using <paramref name="salt"/>
        /// </summary>
        /// <param name="plainText">Original, non-encrypted plain text</param>
        /// <param name="password">Passphrase used to encrypt data</param>
        /// <param name="salt">Salt used to protect from easy decryption in case of security breach</param>
        /// <returns>Encrypted string</returns>
        public static string Encrypt(this string plainText, string password, string salt)
        {
            return SimpleStringCipher.Instance.Encrypt(plainText, password, Encoding.UTF8.GetBytes(salt));
        }

        /// <summary>
        /// Decrypts <paramref name="cipherText"/> using <paramref name="password"/> and <paramref name="salt"/>
        /// </summary>
        /// <param name="cipherText">Encrypted text</param>
        /// <param name="password">Passphrase used to encrypt data</param>
        /// <param name="salt">Salt used to protect from easy decryption in case of security breach</param>
        /// <returns>Decrypted plain string</returns>
        public static string Decrypt(this string cipherText, string password, string salt)
        {
            return SimpleStringCipher.Instance.Decrypt(cipherText, password, Encoding.UTF8.GetBytes(salt));
        }
    }
}
