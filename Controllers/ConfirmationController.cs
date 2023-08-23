using FactoryAPI.Models;
using FactoryAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using System.IdentityModel.Tokens.Jwt;

namespace FactoryAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ConfirmationController : Controller
    {

        [HttpGet]
        public IActionResult GetConfirmationCode()
        {
            int code = CodeGenerator.GenerateNumericCode();
            Dictionary<string, string> result = new()
            {
                { "code", code.ToString() },
                { "hashed code", HashFunction.GetHashPassword(code.ToString()) }
            };
            return Ok(result.ToJson());
        }

        //[Authorize]
        //[HttpPost]
        //public IActionResult PostConfirmationCode([FromHeader] string Authorization, [FromBody] int code)
        //{
        //    int id = TokenDecoder.GetIdFromToken(Authorization);
        //    var user = _context.User.Find(id);
        //    if (user is null)
        //    {
        //        return BadRequest("Wrong Access Token");
        //    }

        //    if (user.ConfirmationCode == code)
        //    {
        //        return Ok("Подтверждено успешно.");
        //    }
        //    else
        //    {
        //        return BadRequest("Код не совпадает.");
        //    }
        //}
    }
}
