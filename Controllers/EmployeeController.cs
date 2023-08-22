using FactoryAPI.Models;
using FactoryAPI.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FactoryAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationContext _context;
        public EmployeeController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetEmployee")]
        public IActionResult GetEmployee(int Id)
        {
            Employee? employee;
            employee = _context.Employee.Find(Id);

            if (employee == null)
            {
                return BadRequest("Работник не существует");
            }
            return Ok(employee);
        }

        [HttpPost(Name = "PostEmployee")]
        public IActionResult PostEmployee([FromQuery] int[] factory_id, string first_name, string second_name, string patronym, string specialization)
        {

            if (!RegexValidator.IsValidName(first_name))
            {
                return BadRequest("Имя пользователя не верно.");
            }
            if (!RegexValidator.IsValidName(second_name))
            {
                return BadRequest("Фамилия пользователя не верна.");
            }
            if (!RegexValidator.IsValidName(patronym))
            {
                return BadRequest("Отчество пользователя не верно.");
            }

            if (RegexValidator.IsValidName(first_name) && RegexValidator.IsValidName(second_name) && RegexValidator.IsValidName(patronym))
            {
                if (factory_id is null)
                {
                    return BadRequest("Предприятие не найдено");
                }

                foreach (int factroryId in factory_id)
                {
                    if (_context.Factory.Find(factroryId) == null)
                        return BadRequest("Некоторые предприятия не в зарегистрированы");
                }


                Employee employee = new()
                {
                    Factorys_id = factory_id,
                    First_name = first_name,
                    Second_name = second_name,
                    Patronym = patronym,
                    Specialization = specialization,
                };
                _context.Employee.Add(employee);
                _context.SaveChanges();
            }
            return Ok("Работник успешно зарегистрирован");
        }

    }
}
