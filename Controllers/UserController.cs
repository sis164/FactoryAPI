using FactoryAPI.Models;
using FactoryAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace FactoryAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UserController : Controller
    {


        private readonly ApplicationContext _context;

        public UserController(ApplicationContext context)
        {
            _context = context;
        }


        [HttpPost(Name = "PostUser")]
        public IActionResult PostUser(string login,  string password, string phone_number)
        {
            if (!RegexValidator.IsValidLogin(login))
            {
                return BadRequest("Логин пользователя не корректен.");
            }
            if(!RegexValidator.IsValidPhone_number(phone_number))
            {
                return BadRequest("Номер телефона не корректен.");
            }
            if (_context.User.FirstOrDefault(x => x.Phone_number == phone_number) is not null)
            {
                return BadRequest("Пользователь с таким номером телефона уже существует.");
            }
            User user = new(login, HashFunction.GetHashPassword(password),phone_number);
            _context.User.Add(user);
            _context.SaveChanges();
            return Ok("Пользователь зарегистрирован.");
        }


    }
}
