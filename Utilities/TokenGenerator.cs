using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace FactoryAPI.Utilities
{
    static public class TokenGenerator
    {
        public static JwtSecurityToken GenerateAccessToken(ClaimsIdentity identity)
        {
            var jwt = new JwtSecurityToken( // Access token
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: DateTime.UtcNow,
                    claims: identity.Claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );
            return jwt;
        }

        public static JwtSecurityToken GenerateAccessToken(List<Claim> authClaims)
        {
            var token = new JwtSecurityToken( // Access token
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: DateTime.UtcNow,
                    claims: authClaims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        public static string GenerateRefreshToken(JwtSecurityToken token)
        {
            //+ time
            DateTime expirationDate = DateTime.Now.AddDays(30);
            StringBuilder sb = new StringBuilder();
            sb.Append(DateDecoder.EncodeExpirationDate(expirationDate));
            //+ random part
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            sb.Append(Convert.ToBase64String(randomNumber));
            //+ last 6 symbols of token
            string tokenName = new JwtSecurityTokenHandler().WriteToken(token).ToString();
            sb.Append(tokenName.AsSpan(tokenName.Length - 6));

            return sb.ToString();
        }

        static public bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param)
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        }

        
    }
}
