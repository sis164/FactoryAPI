using FactoryAPI.Models;
using FactoryAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

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
        public Client GetClient(int id)
        {
            Client? client;
            client = _context.Client.Find(id);

            if (client is null)
            {
                throw new ArgumentNullException(nameof(client), nameof(client) + " cannot be null.");
            }

            return client;
        }

        [HttpPost(Name = "PostClient")]
        public void PostClient([FromQuery] string First_name, string Second_name, string Patronym)
        {
            if (RegexValidator.IsValidName(First_name) && RegexValidator.IsValidName(Second_name) && RegexValidator.IsValidName(Patronym))
            {
                Client client = new(First_name, Second_name, Patronym);
                _context.Client.Add(client);
                _context.SaveChanges();
            }
        }
    }
}
