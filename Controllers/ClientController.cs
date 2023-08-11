using FactoryAPI.Models;
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
        public void PostClient([FromQuery] string First_name, string Second_name, string Patronym, string Phone_number)
        {
            Client client = new Client(First_name, Second_name, Patronym, Phone_number);
            _context.Client.Add(client);
            _context.SaveChanges();

        }
    }
}
