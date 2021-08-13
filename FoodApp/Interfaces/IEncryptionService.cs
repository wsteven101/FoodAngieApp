using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodApp.Interfaces
{
    public interface IEncryptionService
    {
        public string EncryptString(string text, string keyName);
        public string DecryptString(string encryptedText, string keyName);
    }
}
