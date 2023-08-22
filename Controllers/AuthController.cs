using FactoryAPI.Utilities;
using FactoryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

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

        [HttpPost]
        [Route("login")]
        public IActionResult GetToken(string login, string password)
        {
            password = HashFunction.GetHashPassword(password);

            var identity = GetIdentity(out int id, login, password);

            if (identity == null)
            {
                return BadRequest("Неверный логин или пароль.");
            }

            var jwt = TokenGenerator.GenerateAccessToken(identity);

            var jwtRefresh = TokenGenerator.GenerateRefreshToken(jwt);

            var user = _context.User.Find(id);
            if (user == null)
            {
                return BadRequest("Неверный логин или пароль.");
            }

            user.jwtRefresh = jwtRefresh;

            _context.SaveChanges();

            return Ok("Access: " + new JwtSecurityTokenHandler().WriteToken(jwt) + " Refresh: " + jwtRefresh);
        }

        [Authorize]
        [HttpGet]
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok();
        }

        [HttpPost]
        [Route("refresh-token")]
        public IActionResult RefreshToken(TokenModel? token)
        {
            if (token == null)
            {
                return BadRequest("Залогиньтесь");
            }

            var accessToken = token.AccessToken;
            var refreshToken = token.RefreshToken;

            if (refreshToken is null)
            {
                return BadRequest("Invalid refresh token");
            }

            var principal = TokenGenerator.GetPrincipalFromExpiredToken(accessToken);

            if (principal == null)
            {
                return BadRequest("Invalid access token");
            }

            string username = principal.Identity.Name;

            var user = _context.User.FirstOrDefault(x => x.Login == username);
            if (user == null || user.jwtRefresh != refreshToken || DateDecoder.DecodeExpirationDate(refreshToken[..6]) <= DateTime.Now)
            {
                return BadRequest("Invalid refresh token");
            }

            var newAccessToken = TokenGenerator.CreateToken(principal.Claims.ToList());
            var newRefreshToken = TokenGenerator.GenerateRefreshToken(newAccessToken);

            user.jwtRefresh = newRefreshToken;
            _context.SaveChanges();

            return Ok("Access: " + new JwtSecurityTokenHandler().WriteToken(newAccessToken).ToString() + " Refresh: " + newRefreshToken);
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

    }
}
