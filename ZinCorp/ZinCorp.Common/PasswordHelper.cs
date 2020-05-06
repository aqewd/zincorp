using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ZinCorp.Common
{
    public class Password
    {
        private string _password;
        private int _salt;

        public Password(string strPassword, int nSalt)
        {
            _password = strPassword;
            _salt = nSalt;
        }

        public static int CreateRandomSalt()
        {
            var _saltBytes = new Byte[4];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(_saltBytes);

            return ((((int)_saltBytes[0]) << 24) + (((int)_saltBytes[1]) << 16) +
                    (((int)_saltBytes[2]) << 8) + ((int)_saltBytes[3]));
        }

        public string ComputeSaltedHash()
        {
            // Create Byte array of password string
            var encoder = new ASCIIEncoding();
            var _secretBytes = encoder.GetBytes(_password);

            // Create a new salt
            var _saltBytes = new byte[4];
            _saltBytes[0] = (byte)(_salt >> 24);
            _saltBytes[1] = (byte)(_salt >> 16);
            _saltBytes[2] = (byte)(_salt >> 8);
            _saltBytes[3] = (byte)(_salt);

            // append the two arrays
            var toHash = new byte[_secretBytes.Length + _saltBytes.Length];
            Array.Copy(_secretBytes, 0, toHash, 0, _secretBytes.Length);
            Array.Copy(_saltBytes, 0, toHash, _secretBytes.Length, _saltBytes.Length);

            var sha1 = SHA1.Create();
            var computedHash = sha1.ComputeHash(toHash);

            return encoder.GetString(computedHash);
        }
    }
}
