using PayPadAdministrator.Classes;
using PayPlusModels.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PayPadAdministrator.Helpers
{
    public class EncryptionHelper
    {       

        public string Encrypt_DecryptKey { get; set; }

        private string initVector = string.Empty;        
        private const int keysize = 256;

        public EncryptionHelper(string keyToEncrypt, string inivector)
        {
            Encrypt_DecryptKey = keyToEncrypt;
            this.initVector = inivector;
        }

        public EncryptionHelper()
        {
            Encrypt_DecryptKey = string.Concat(Utilities.GetConfiguration("KeyEValidate"),DateTime.Now.Year);
            this.initVector = string.Concat(Utilities.GetConfiguration("KeyEVector"), DateTime.Now.Year);
        }

        #region Encrypt Section


        public string EncryptString(string textToEncrypt)
        {
            try
            {
                byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(textToEncrypt);
                PasswordDeriveBytes password = new PasswordDeriveBytes(Encrypt_DecryptKey, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] cipherTextBytes = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                return Convert.ToBase64String(cipherTextBytes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string DecryptString(string encryptedText)
        {
            try
            {
                byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
                byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(Encrypt_DecryptKey, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion



    }
}
