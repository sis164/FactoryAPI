using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FactoryAPI.Utilities
{
    static public class AuthOptions
    {
        public const string ISSUER = "FactoryAPI"; // издатель токена
        public const string AUDIENCE = "FactoryClient"; // потребитель токена
        const string KEY = "pkey_factoryAPI_pkey";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена (в минутах)
        //public const int LIFETIME = 20;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
