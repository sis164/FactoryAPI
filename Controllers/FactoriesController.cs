using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FactoryAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using FactoryAPI.Utilities;
using NuGet.Packaging;

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
            Factory? factory;
            UserFactory userFactory = new();

            factory = _context.Factory.Find(id);

            if (factory is null)
            {
                return BadRequest("Предприятие не существует(");
            }

            userFactory = new(factory);

            return Ok(userFactory);
        }

        [HttpPost(Name = "PostFactory")]
        public IActionResult PostFactory(string name, string description, string phone, [FromBody] string[] pictures)
        {
            if (RegexValidator.IsValidCompanyName(name) && RegexValidator.IsValidPhone_number(phone))
            { 
                Factory factory = new() { Name = name, Description = description, Phone_number = phone, Picture = PictureConverter.SaveImageGetPath(pictures, name) };
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

            

            if (factory.Employee_id is null)
            {
                factory.Employee_id = new List<int>();
            }

            if (factory.Employee_id.Contains(employee_id))
                return Ok();

            factory.Employee_id.Add(employee_id);
            _context.SaveChanges();
            return Ok("Изменения успешно проведены");
        }
    }


}
