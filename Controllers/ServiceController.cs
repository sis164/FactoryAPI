using FactoryAPI.Models;
using FactoryAPI.Utilities;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetService(int id)
        {
            Service? service;
            service = _context.Service.Find(id);

            if (service is null)
            {
                return BadRequest("Сервис не существует(");
            }
            UserService userService = new(service);
            return Ok(userService);
        }

        [HttpPost(Name = "PostService")]
        public void PostService([FromBody] RequestService requestService)
        {
            var name = requestService.Name;
            var description = requestService.Description;
            var cost = requestService.Cost;
            var pictures = requestService.Pictures;
            Service service = new(name,description, cost, PictureConverter.SaveImageGetPath(pictures, name + "Service"));
            _context.Service.Add(service);
            _context.SaveChanges();
        }
    }
    public class RequestService
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public string[] Pictures { get; set; }
    }
}
