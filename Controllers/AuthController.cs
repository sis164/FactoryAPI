using FactoryAPI.Utilities;
using FactoryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text;

namespace FactoryAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class AuthController : Controller
    {
        private readonly ApplicationContext _context;

        public AuthController(ApplicationContext context)
        {

            _context = context;
        }

        [HttpPost(Name = "GetToken")]
        public IActionResult GetToken(string login, string password)
        {
            password = HashFunction.GetHashPassword(password);

            var identity = GetIdentity(out int id, login, password);

            if (identity == null)
            {
                return BadRequest("Неверный логин или пароль.");
            }

            var jwt = new JwtSecurityToken( // Access token
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: DateTime.UtcNow,
                    claims: identity.Claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            var jwtRefresh = GenerateRefreshToken(jwt);

            var user = _context.User.Find(id);
            if (user == null)
            {
                return BadRequest("Неверный логин или пароль.");
            }

            user.jwtRefresh = jwtRefresh;

            _context.SaveChanges();

            return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
        }

        [Authorize]
        [HttpGet(Name = "GetLogin")]
        public IActionResult GetLogin()
        {
            return Ok();
        }

        private ClaimsIdentity? GetIdentity(out int id, string login, string password)
        {
            User? person = _context.User.FirstOrDefault(x => x.Login == login && x.Password == password);

            if (person is null)
            {
                id = 0;
                return null;
            }

            id = person.Id;
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim("User id:", person.Id.ToString())
                };

            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        private static string EncodeExpirationDate(DateTime expirationDate)
        {
            // Extract the components from the expiration date
            int year = expirationDate.Year % 100; // Considering the last two digits of the year
            int month = expirationDate.Month;
            int day = expirationDate.Day;
            int hour = expirationDate.Hour;
            int minute = expirationDate.Minute;

            // Encode the date using the same logic
            uint date = (uint)((year << 20) | (month << 16) | (day << 11) | (hour << 6) | minute);

            StringBuilder sBuilder = new StringBuilder();

            for (int n = 0; n < 6; n++)
            {
                byte b6Bit = (byte)((date >> (n * 6)) & 0x3F);
                sBuilder.Append(AsciiToByte8bit(b6Bit));
            }

            return sBuilder.ToString();
        }

        // Helper method to convert a 6-bit value to an ASCII character
        private static char AsciiToByte8bit(byte value)
        {
            // ASCII characters 0x20 to 0x5F represent printable characters
            return (char)(value + 0x20);
        }

        private static string GenerateRefreshToken(JwtSecurityToken token)
        {
            //+ time
            DateTime expirationDate = DateTime.Now.AddDays(30);
            StringBuilder sb = new StringBuilder();
            sb.Append(EncodeExpirationDate(expirationDate));
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
    }
}
