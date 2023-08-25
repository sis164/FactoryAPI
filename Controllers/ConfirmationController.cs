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
    }
}
