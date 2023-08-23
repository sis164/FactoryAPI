using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace FactoryAPI.Utilities
{
    static public class TokenDecoder
    {
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
        public static JwtSecurityToken ReadToken(string token)
        {
            var validations = new TokenValidationParameters
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
            tokenHandler.ValidateToken(token, validations, out SecurityToken securityToken);

            if (securityToken is null)
            {
                throw new SecurityTokenException("Invalid token");
            }

            return (JwtSecurityToken)securityToken;
        }
        public static int GetIdFromToken(string token)
        {
            if (token.Contains("bearer ") || token.Contains("Bearer "))
            {
                token = token.Remove(0, 7);
            }
            var securityToken = new JwtSecurityToken(token) ?? throw new SecurityTokenException("Invalid token");
            var claims = securityToken.Claims ?? throw new SecurityTokenException("Invalid token");
            string id = claims.First(claim => claim.Type == "User id:").Value;
            return Convert.ToInt32(id);
        }
    }
}
