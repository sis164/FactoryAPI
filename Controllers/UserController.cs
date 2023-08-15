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
        public void PostUser(string login, string mail, string password, string phone_number)
        {
            if (!RegexValidator.IsValidMail(mail))
            {
                throw new ArgumentException($"{nameof(mail)} is invalid.");
            }
            if (!RegexValidator.IsValidLogin(login))
            {
                throw new ArgumentException($"{nameof(login)} is invalid.");
            }
            if(!RegexValidator.IsValidPhone_number(phone_number))
            {
                throw new ArgumentException($"{nameof(phone_number)} is invalid.");
            }
            User user = new(login, HashFunction.GetHashPassword(password), mail,phone_number);
            _context.User.Add(user);
            _context.SaveChanges();
        }


    }
}
