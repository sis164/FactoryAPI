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

        
    }
}
