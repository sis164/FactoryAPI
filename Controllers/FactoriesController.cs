using Microsoft.AspNetCore.Mvc;
using FactoryAPI.Models.RequestBodies;
using FactoryAPI.Models;
using FactoryAPI.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;

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

        [HttpGet(Name = "GetFactory")]
        public IActionResult GetFactory([FromQuery] int id)
        {
            var factory = _context.Factory.Find(id);

            if (factory is null)
            {
                return BadRequest("Предприятие с таким id не существует.");
            }

            UserFactory userFactory = new(factory);

            return Ok(userFactory);
        }

        [HttpPost(Name = "PostFactory")]
        public IActionResult PostFactory([FromBody] RequestFactory requestFactory)
        {
            if (RegexValidator.IsValidCompanyName(requestFactory.Name) && RegexValidator.IsValidPhone_number(requestFactory.Phone_number))
            {
                Factory factory = new()
                {
                    Name = requestFactory.Name,
                    Description = requestFactory.Description, 
                    Phone_number = requestFactory.Phone_number, 
                    Picture = PictureConverter.SaveImageGetPath(requestFactory.Pictures, requestFactory.Name + "Factory")
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

        [HttpPut(Name = "AddEmployee")]
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
    }
}
