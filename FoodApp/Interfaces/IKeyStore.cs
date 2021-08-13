using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodApp.Interfaces
{
    public interface IKeyStore
    {
        public string KeyIndex { get; }
        public string GetKey(string name);
    }
}
