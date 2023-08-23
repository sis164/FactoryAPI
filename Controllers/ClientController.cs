using FactoryAPI.Models.RequestBodies;
using FactoryAPI.Models;
using FactoryAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet(Name = "GetClient")]
        public IActionResult GetClient(int id)
        {
            var client = _context.Client.Find(id);

            if (client is null)
            {
                return BadRequest("Нет клиента с таким id");
            }

            return Ok(client);
        }

        [Authorize]
        [HttpPost(Name = "PostClient")]
        public IActionResult PostClient([FromBody] RequestClient requestClient)
        {
            if (!RegexValidator.IsValidName(requestClient.FirstName))
            {
                return BadRequest("Имя пользователя не верно.");
            }
            if (!RegexValidator.IsValidName(requestClient.SecondName))
            {
                return BadRequest("Фамилия пользователя не верна.");
            }
            if (!RegexValidator.IsValidName(requestClient.Patronym))
            {
                return BadRequest("Отчество пользователя не верно.");
            }

            Client client = new(requestClient.FirstName, requestClient.SecondName, requestClient.Patronym);
            _context.Client.Add(client);
            _context.SaveChanges();

            return Ok(client);
        }
    }
}
