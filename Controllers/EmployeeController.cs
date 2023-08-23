﻿using FactoryAPI.Models;
using FactoryAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet(Name = "GetEmployee")]
        public IActionResult GetEmployee(int Id)
        {
            var employee = _context.Employee.Find(Id);

            if (employee == null)
            {
                return BadRequest("Работник не существует");
            }
            return Ok(employee);
        }

        [Authorize]
        [HttpPost]
        public IActionResult PostEmployee([FromBody] RequestEmployee requestEmployee)
        {
            if (!RegexValidator.IsValidName(requestEmployee.First_name))
            {
                return BadRequest("Имя пользователя не верно.");
            }
            if (!RegexValidator.IsValidName(requestEmployee.Second_name))
            {
                return BadRequest("Фамилия пользователя не верна.");
            }
            if (!RegexValidator.IsValidName(requestEmployee.Patronym))
            {
                return BadRequest("Отчество пользователя не верно.");
            }


            if (requestEmployee.FactoryId is null)
            {
                return BadRequest("Предприятие не найдено");
            }

            foreach (int factoryId in requestEmployee.FactoryId)
            {
                if (_context.Factory.Find(factoryId) == null)
                    return BadRequest("Некоторые предприятия не в зарегистрированы");
            }

            Employee employee = new Employee()
            {
                Factorys_id = requestEmployee.FactoryId,
                First_name = requestEmployee.First_name,
                Second_name = requestEmployee.Second_name,
                Patronym = requestEmployee.Patronym,
                Specialization = requestEmployee.Specialization,
            };

            _context.Employee.Add(employee);
            _context.SaveChanges();

            return Ok("Работник успешно зарегистрирован");
        }
    }

    public class RequestEmployee
    {
        public int[] FactoryId { get; set; }
        public string First_name { get; set; }
        public string Second_name { get; set; }
        public string Patronym { get; set; }
        public string Specialization { get; set; }
        public RequestEmployee(int[] factoryId, string first_name, string second_name, string patronym, string specialization)
        {
            FactoryId = factoryId;
            First_name = first_name;
            Second_name = second_name;
            Patronym = patronym;
            Specialization = specialization;
        }
    }
}

