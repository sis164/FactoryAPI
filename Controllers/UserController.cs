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
        public void PostUser(string login,  string password, string phone_number)
        {

            if (!RegexValidator.IsValidLogin(login))
            {
                throw new ArgumentException($"{nameof(login)} is invalid.");
            }
            if(!RegexValidator.IsValidPhone_number(phone_number))
            {
                throw new ArgumentException($"{nameof(phone_number)} is invalid.");
            }
            if (_context.User.FirstOrDefault(x => x.Phone_number == phone_number) is not null)
            {
                throw new ArgumentException($"{nameof(phone_number)} is exists.");
            }
            User user = new(login, HashFunction.GetHashPassword(password),phone_number);
            _context.User.Add(user);
            _context.SaveChanges();
        }


    }
}
