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
        public UserFactory GetFactory([FromQuery] int id)
        {
            Factory? factory;
            UserFactory userFactory = new();

            factory = _context.Factory.Find(id);

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory), nameof(factory) + " cannot be null.");
            }

            userFactory = new(factory);

            return userFactory;
        }

        [HttpPost(Name = "PostFactory")]
        public void PostFactory(string name, string description, string phone, string picture, [FromQuery] int[]? services, [FromQuery] List<int>? employee_id)
        {
            if (RegexValidator.IsValidCompanyName(name) && RegexValidator.IsValidPhone_number(phone))
            {

                if (services != null)
                {
                    foreach (int Id in services)
                    {
                        if (_context.Service.Find(Id) is null)
                            throw new ArgumentOutOfRangeException(nameof(Id), "Some services are not in the Database");
                    }
                }

                if (employee_id != null)
                {
                    foreach (int Id in employee_id)
                    {
                        if (_context.Employee.Find(Id) is null)
                            throw new ArgumentOutOfRangeException(nameof(Id), "Some employees are not in the Database");
                    }
                }

                Factory factory = new() { Name = name, Description = description, Phone_number = phone, Picture = PictureConverter.SaveImageGetPath(picture, name), Services = services, Employee_id = employee_id };
                _context.Factory.Add(factory);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Company name or phone number is wrong");
            }
        }

        [HttpPut(Name = "AddEmployee")]
        public void PutEmployee(int employee_id, int factory_id)
        {
            if (_context.Employee.Find(employee_id) is null)
            {
                throw new ArgumentException("this employee doesn't exist");
            }

            var factory = _context.Factory.Find(factory_id);

            if (factory is null)
            {
                throw new ArgumentException("this Factory doesn't exist");
            }

            

            if (factory.Employee_id is null)
            {
                factory.Employee_id = new List<int>();
            }

            if (factory.Employee_id.Contains(employee_id))
                return;

            factory.Employee_id.Add(employee_id);
            _context.SaveChanges();
        }
    }


}
