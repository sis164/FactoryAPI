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
        public void PostUser(string login, string mail, string password)
        {
            if (!RegexValidator.IsValidMail(mail))
            {
                throw new ArgumentException($"{nameof(mail)} is invalid.");
            }
            if (!RegexValidator.IsValidLogin(login))
            {
                throw new ArgumentException($"{nameof(login)} is invalid.");
            }
            User user = new(login, HashFunction.GetHashPassword(password), mail);
            _context.User.Add(user);
            _context.SaveChanges();
        }


    }
}
