using FactoryAPI.Models;
using FactoryAPI.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FactoryAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ClientController : Controller
    {
        private readonly ApplicationContext _context;

        public ClientController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetClient")]
        public IActionResult GetClient(int id)
        {
            Client? client;
            client = _context.Client.Find(id);

            if (client is null)
            {
                return BadRequest("Нет клиента с таким id");
            }

            return Ok(client);
        }

        [HttpPost(Name = "PostClient")]
        public IActionResult PostClient(string First_name, string Second_name, string Patronym)
        {
            if (!RegexValidator.IsValidName(First_name))
            {
                return BadRequest("Имя пользователя не верно.");
            }
            if (!RegexValidator.IsValidName(Second_name))
            {
                return BadRequest("Фамилия пользователя не верна.");
            }
            if (!RegexValidator.IsValidName(Patronym))
            {
                return BadRequest("Отчество пользователя не верно.");
            }

            Client client = new(First_name, Second_name, Patronym);
            _context.Client.Add(client);
            _context.SaveChanges();

            return Ok(client);
        }
    }
}
