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
        public Client GetClient(  int id)
        {
            Client? client;
            try
            {
                client = _context.Client.Find(id);

                if (client is null)
                {
                    throw new ArgumentNullException(nameof(client), nameof(client) + " cannot be null.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Client();
            }
            return client;
        }

        [HttpPost(Name = "PostClient")]
        public void PostClient([FromBody] Client client)
        {

        }
    }
}
