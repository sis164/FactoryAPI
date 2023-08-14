using FactoryAPI.Models;
using FactoryAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Npgsql.Internal.TypeHandlers.NumericHandlers;

namespace FactoryAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ServiceController : Controller
    {
        private readonly ApplicationContext _context;

        public ServiceController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetService")]
        public Service GetService(int id)
        {
            Service? service;
            service = _context.Service.Find(id);

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service), nameof(service) + " cannot be null.");
            }

            return service;
        }

        [HttpPost(Name = "PostService")]
        public void PostService(string name, string description, double cost)
        {
            Service service = new(name,description, cost);
            _context.Service.Add(service);
            _context.SaveChanges();
        }
    }
}
