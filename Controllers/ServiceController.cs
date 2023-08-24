using FactoryAPI.Models.RequestBodies;
using FactoryAPI.Models;
using FactoryAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
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


        [Authorize]
        [HttpGet(Name = "GetService")]
        public IActionResult GetService(int id)
        {
            Service? service;
            service = _context.Service.Find(id);

            if (service is null)
            {
                return BadRequest("Сервис с таким id не существует.");
            }
            RequestService requestService = new(service);
            return Ok(requestService);
        }

        [Authorize]
        [HttpPost(Name = "PostService")]
        public IActionResult PostService([FromBody] RequestService requestService)
        {
            Service service = new(requestService.Name, requestService.Description, requestService.Cost, PictureConverter.SaveImageGetPath(requestService.Pictures, requestService.Name + "Service"));
            _context.Service.Add(service);
            _context.SaveChanges();
            return Ok("Сервис успешно зарегистрирован.");
        }
    }
}
