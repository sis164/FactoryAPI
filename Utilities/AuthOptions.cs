using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FactoryAPI.Utilities
{
    public class AuthOptions
    {
        public const string ISSUER = "FactoryAPI"; // издатель токена
        public const string AUDIENCE = "FactoryClient"; // потребитель токена
        const string KEY = "pkey_factoryAPI_pkey";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
