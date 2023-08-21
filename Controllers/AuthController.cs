using FactoryAPI.Utilities;
using FactoryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;

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
            var identity = GetIdentity(login, password);

            if (identity == null)
            {
                return BadRequest("Неверный логин или пароль.");
            }

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: DateTime.UtcNow,
                    claims: identity.Claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
        }

        [Authorize]
        [HttpGet(Name = "GetLogin")]
        public IActionResult GetLogin()
        {
            return Ok();
        }

        private ClaimsIdentity? GetIdentity(string login, string password)
        {
            User? person = _context.User.FirstOrDefault(x => x.Login == login && x.Password == password);

            if (person is null)
            {
                return null;
            }

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
