using System.Security.Cryptography;
using System.Text;

namespace FactoryAPI.Utilities
{
    public static class HashFunction
    {
        private static string SaltPassword(string password)
        {
            return $"aaSDcc{password}ASDcadc";
        }
        public static string GetHashPassword(string password)
        {
            SHA256 hash = SHA256.Create();
            return Convert.ToHexString(hash.ComputeHash(Encoding.ASCII.GetBytes(SaltPassword(password))));
        }
    }
}
