using Effortless.Net.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Utils.EncryptionDecryption
{
    public static class Encrypt
    {
        public static string EncryptText(string cipher)
        {
            return Hash.Create(HashType.SHA512, cipher, null, false);
        }
    }
}
