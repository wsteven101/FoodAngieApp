using FoodApp.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace FoodApp.Services
{


    public class AESEncryptionService :IEncryptionService
    {
        private IKeyStore _keyStore;

        private byte[] HexStringToBytes(string hexString) =>
            Enumerable.Range(0, hexString.Length)
            .Where(h => h % 2 == 0)
            .Select(h => Convert.ToByte(hexString.Substring(h, 2), 16))
            .ToArray();

        public AESEncryptionService(IKeyStore keyStore)
        {
            _keyStore = keyStore;
        }

        private string BytesToString(byte[] bytes)
        {
            var res = "";
            bytes.ToList().ForEach(b => res += b.ToString("X2"));
            return res;
        }

         // The encryption service uses the AES encryption standard.
         // The Rijndael is also supported in this class but not used
         // The AES algorithm actually implements the Rijndael algorithm 
         // The difference between the two .Net implementations is
         // apparently that the AES implementation uses a 256 bit key
         // whilst the Rijndael implementation allows multiple different
         // key sizes
         // 
         // The encryption function returns a string combined of:
         //    [{KeyIndex}]{IV}{EncryptedText}
         // 
         // The Key Index is there for the pupose of key rotation and 
         // the IV to provide different encrypted values even though
         // the Key is the same.
         // It is okay to expose the IV publically as long as the Key 
         // is hidden
        public string EncryptString(string text, string keyName)
        {
            return EncryptStringAESAlg(text, keyName);
            //return EncryptStringRijndaelAlg(text, keyName);
        }

        public string DecryptString(string encryptedText, string keyName)
        {
            return DecryptStringAESAlg(encryptedText, keyName);
            //return DecryptStringRijndaelAlg(encryptedText, keyName);
        }

        public string EncryptStringAESAlg(string text, string keyName)
        {

            var keyIndex = _keyStore.KeyIndex;
            var key = _keyStore.GetKey(keyName);

            byte[] encryptedValue;

            var keyAsBytes = HexStringToBytes(key);

            using (var encryptAlg = Aes.Create())
            {

                // The IV is similar to a Salt and ensures that each encrypted
                // value is different even when using the same encryption key
                // set IV to a random number using RNGCryptoServiceProvider

                var blockSize = encryptAlg.BlockSize / 8; // bit size to bytes
                var ivAsBytes = new byte[blockSize];

                 // generate random bytes
                (new RNGCryptoServiceProvider()).GetBytes(ivAsBytes);

                encryptAlg.Key = keyAsBytes;
                encryptAlg.Padding = PaddingMode.PKCS7;
                encryptAlg.Mode = CipherMode.CBC;
                encryptAlg.IV = ivAsBytes;

                var encryptor = encryptAlg.CreateEncryptor(
                    encryptAlg.Key,
                    encryptAlg.IV);

                using (var memStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(
                        memStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (var writer = new StreamWriter(cryptoStream))
                        {
                            writer.Write(text);
                        }
                        encryptedValue = memStream.ToArray();
                    }

                }

                var encryptedText = BytesToString(encryptedValue);
                var ivAsString = BytesToString(ivAsBytes);

                var fullEncrytpedText = $"[{keyIndex}]{ivAsString}{encryptedText}";

                return fullEncrytpedText;
            }
        }

        public string DecryptStringAESAlg(string encryptedText, string keyName)
        {
            var key = _keyStore.GetKey(keyName);

            var keyAsBytes = HexStringToBytes(key);

            using (var aes = Aes.Create())
            {
                // iv is stored as part of the key
                // 2 chars per byte in hex numbers
                var keyIndex = encryptedText.Substring(1, encryptedText.IndexOf(']') - 1);
                var ivIdx = encryptedText.IndexOf(']') + 1;

                var blockSize = aes.BlockSize / 8; // bit size to bytes
                var blockSizeAsHexChars = blockSize * 2;
                var ivPartOfKey = encryptedText.Substring(ivIdx, blockSizeAsHexChars);
                var ivAsBytes = HexStringToBytes(ivPartOfKey);

                // skip kry index and iv to get to the encrypted text
                var encryptedTextOnlyIdx = ivIdx + blockSizeAsHexChars;
                var encryptedTextOnly = encryptedText.Substring(encryptedTextOnlyIdx);
                var encryptedTextAsBytes = HexStringToBytes(encryptedTextOnly);


                aes.Key = keyAsBytes;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                aes.IV = ivAsBytes;

                var decryptor = aes.CreateDecryptor(
                    aes.Key, aes.IV);

                using (var memStream = new MemoryStream(encryptedTextAsBytes))
                {
                    using (var cryptStream = new CryptoStream(
                        memStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(cryptStream))
                        {
                            var unencryptedText = reader.ReadToEnd();
                            return unencryptedText;
                        }
                    }
                }

            }
        }


        public string EncryptStringRijndaelAlg(string text, string keyName)
        {

            var keyIndex = _keyStore.KeyIndex;
            var key = _keyStore.GetKey(keyName);

            byte[] encryptedValue;

            var keyAsBytes = HexStringToBytes(key);


            using(var rijndael = Rijndael.Create())
            {

                // The IV is similar to a Salt and ensures that each encrypted
                // value is different even when using the same encryption key
                // set IV to a random number using RNGCryptoServiceProvider

                var blockSize = rijndael.BlockSize / 8; // bit size to bytes
                var ivAsBytes = new byte[blockSize];
                (new RNGCryptoServiceProvider()).GetBytes(ivAsBytes);

                rijndael.GenerateKey();
                var akey = rijndael.Key;
                var akeyhex = BytesToString(rijndael.Key);

                rijndael.Key = keyAsBytes;
                rijndael.Padding = PaddingMode.PKCS7;
                rijndael.Mode = CipherMode.CBC;
                rijndael.IV = ivAsBytes;

                var encryptor = rijndael.CreateEncryptor(
                    rijndael.Key,
                    rijndael.IV);

                using (var memStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(
                        memStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (var writer = new StreamWriter(cryptoStream))
                        {
                            writer.Write(text);
                        }
                        encryptedValue = memStream.ToArray();
                    }

                }

                var encryptedText = BytesToString(encryptedValue);
                var ivAsString = BytesToString(ivAsBytes);

                var fullEncrytpedText = $"[{keyIndex}]{ivAsString}{encryptedText}";
                
                return fullEncrytpedText;
            }
        }

        public string DecryptStringRijndaelAlg(string encryptedText, string keyName)
        {
            var key = _keyStore.GetKey(keyName);

            var keyAsBytes = HexStringToBytes(key);

            using (var rijndael = Rijndael.Create())
            {
                // iv is stored as part of the key
                // 2 chars per byte in hex numbers
                var keyIndex = encryptedText.Substring(1, encryptedText.IndexOf(']') - 1);
                var ivIdx = encryptedText.IndexOf(']') + 1;

                var blockSize = rijndael.BlockSize / 8; // bit size to bytes
                var blockSizeAsHexChars = blockSize * 2;
                var ivPartOfKey = encryptedText.Substring(ivIdx, blockSizeAsHexChars);
                var ivAsBytes = HexStringToBytes(ivPartOfKey);

                 // skip kry index and iv to get to the encrypted text
                var encryptedTextOnlyIdx = ivIdx + blockSizeAsHexChars;
                var encryptedTextOnly = encryptedText.Substring(encryptedTextOnlyIdx);
                var encryptedTextAsBytes = HexStringToBytes(encryptedTextOnly);


                rijndael.Key = keyAsBytes;
                rijndael.Padding = PaddingMode.PKCS7;
                rijndael.Mode = CipherMode.CBC;
                rijndael.IV = ivAsBytes;

                var decryptor = rijndael.CreateDecryptor(
                    rijndael.Key, rijndael.IV);

                using(var memStream = new MemoryStream(encryptedTextAsBytes))
                {
                    using(var cryptStream = new CryptoStream(
                        memStream, decryptor, CryptoStreamMode.Read))
                    {
                        using(var reader = new StreamReader(cryptStream))
                        {
                            var unencryptedText = reader.ReadToEnd();
                            return unencryptedText;
                        }
                    }
                }
                
            }
        }
    }
}
