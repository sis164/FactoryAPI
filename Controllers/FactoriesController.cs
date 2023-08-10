using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FactoryAPI.Models;
using System.ComponentModel.DataAnnotations;

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
            var factory = _context.Factory.Find(id);

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory), nameof(factory) + " cannot be null.");
            }
            UserFactory userFactory = new(factory);
            return userFactory;
        }

        [HttpPost(Name = "PostFactory")]
        public void PostFactory([FromQuery] string name, string description, string phone, string picture)
        {
            Factory factory = new() { Name = name, Description = description, Phone_number = phone, Picture = picture };
            _context.Factory.Add(factory);
            _context.SaveChanges();
        }

    }
}
