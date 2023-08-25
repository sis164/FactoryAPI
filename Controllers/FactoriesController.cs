using Microsoft.AspNetCore.Mvc;
using FactoryAPI.Models.RequestBodies;
using FactoryAPI.Models;
using FactoryAPI.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;

namespace FactoryAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class FactoriesController : Controller
    {
        private readonly ApplicationContext _context;

        public FactoriesController(ApplicationContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetFactory([FromQuery] int id)
        {
            var factory = _context.Factory.Find(id);

            if (factory is null)
            {
                return BadRequest("Предприятие с таким id не существует.");
            }

            RequestFactory requestFactory = new(factory);

            return Ok(requestFactory);
        }

        [Authorize]
        [HttpPost]
        public IActionResult PostFactory([FromHeader] string Authorization, [FromBody] RequestFactory requestFactory)
        {
            if (RegexValidator.IsValidCompanyName(requestFactory.Name) && RegexValidator.IsValidPhone_number(requestFactory.Phone_number))
            {
                Factory factory = new()
                {
                    Name = requestFactory.Name,
                    Description = requestFactory.Description,
                    Phone_number = requestFactory.Phone_number,
                    Picture = PictureConverter.SaveImageGetPath(requestFactory.Pictures, requestFactory.Name + "Factory"),
                    Owner_Id = TokenDecoder.GetIdFromToken(Authorization),
                };
                _context.Factory.Add(factory);
                _context.SaveChanges();
            }
            else
            {
                return BadRequest("Название предприятия или номер телефона не корректны(");
            }
            return Ok("Предприятие зарегистрировано");
        }

        [Authorize]
        [HttpPut]
        public IActionResult PutEmployee(int employee_id, int factory_id)
        {
            if (_context.Employee.Find(employee_id) is null)
            {
                return BadRequest("Данный работник не зарегистрирован");
            }

            var factory = _context.Factory.Find(factory_id);

            if (factory is null)
            {
                return BadRequest("Придприятие не зарегистрировано (");
            }

            factory.Employee_id ??= new List<int>();

            if (factory.Employee_id.Contains(employee_id))
                return Ok();

            factory.Employee_id.Add(employee_id);
            _context.SaveChanges();
            return Ok("Изменения успешно проведены");
        }

        [Authorize]
        [HttpGet]
        [Route("all-factories")]
        public IActionResult GetAllFactories([FromHeader] string Authorization)
        {
            int ownerId = TokenDecoder.GetIdFromToken(Authorization);
            var factories = _context.Factory.Where(factory=>factory.Owner_Id == ownerId).ToList();
            List<RequestFactory> result = new();
            foreach(Factory factory in factories)
            {
                result.Add(new RequestFactory(factory));
            }
            return Ok(result);
        }
    }
}
