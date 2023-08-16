using FactoryAPI.Models;
using FactoryAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

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
        public Employee GetEmployee(int Id)
        {
            Employee? employee;
            employee = _context.Employee.Find(Id);

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), nameof(employee) + " cannot be null.");
            }
            return employee;
        }

        [HttpPost(Name = "PostEmployee")]
        public void PostEmployee([FromQuery] int[] factory_id, string first_name, string second_name, string patronym, string specialization)
        {
            if (RegexValidator.IsValidName(first_name) && RegexValidator.IsValidName(second_name) && RegexValidator.IsValidName(patronym))
            {
                if (factory_id is null)
                {
                    throw new ArgumentNullException(nameof(factory_id), $"{nameof(factory_id)} is null");
                }

                foreach (int factroryId in factory_id)
                {
                    if (_context.Factory.Find(factroryId) == null)
                        throw new ArgumentOutOfRangeException(nameof(factroryId), "Some factories are not in the Database");
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
            else
            {
                throw new ArgumentException("Employee name or phone number is wrong");
            }
        }

    }
}
