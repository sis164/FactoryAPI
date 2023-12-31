﻿using FactoryAPI.Models.RequestBodies;
using FactoryAPI.Models;
using FactoryAPI.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FactoryAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UserController : Controller
    {
        private readonly ApplicationContext _context;

        public UserController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult PostUser([FromBody] RequestUser requestUser)
        {
            if (!RegexValidator.IsValidLogin(requestUser.Login))
            {
                return BadRequest("Логин пользователя не корректен.");
            }
            if (!RegexValidator.IsValidPhone_number(requestUser.Phone_number))
            {
                return BadRequest("Номер телефона не корректен.");
            }
            if (_context.User.FirstOrDefault(x => x.Phone_number == requestUser.Phone_number) is not null)
            {
                return BadRequest("Пользователь с таким номером телефона уже существует.");
            }
            if (requestUser.HashCode != HashFunction.GetHashPassword(requestUser.Code.ToString()))
            {
                return BadRequest("Не подтверждён номер телефона.");
            }

            User user = new(requestUser.Login, HashFunction.GetHashPassword(requestUser.Password), requestUser.Phone_number);
            _context.User.Add(user);
            _context.SaveChanges();

            return Ok("Пользователь зарегистрирован.");
        }
    }
}
