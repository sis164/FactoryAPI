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

        public static JwtSecurityToken CreateToken(List<Claim> authClaims)
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
            //DateTime expirationDate = DateTime.Now.AddDays(30);
            DateTime expirationDate = DateTime.Now.AddMinutes(0.5);
            StringBuilder sb = new StringBuilder();
            sb.Append(DateDecoder.EncodeExpirationDate(expirationDate));
            //+ random part
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            sb.Append(Convert.ToBase64String(randomNumber));
            //+ last 6 symbols of token
            string tokenName = new JwtSecurityTokenHandler().WriteToken(token).ToString();
            sb.Append(tokenName.Substring(tokenName.Length - 6));

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

        static public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AuthOptions.ISSUER,
                ValidateAudience = true,
                ValidAudience = AuthOptions.AUDIENCE,
                ValidateLifetime = false,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }
    }
}
