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
        public void PostFactory([FromQuery] string name, string description, string phone, string picture)
        {
            if (RegexValidator.IsValidCompanyName(name) && RegexValidator.IsValidPhone_number(phone))
            {
                Factory factory = new() { Name = name, Description = description, Phone_number = phone, Picture = PictureConverter.SaveImageGetPath(picture, name) };
                _context.Factory.Add(factory);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Company name or phone number is wrong");
            }
        }

    }
}
