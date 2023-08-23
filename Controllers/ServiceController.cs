﻿using FactoryAPI.Models;
using FactoryAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactoryAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ServiceController : Controller
    {
        private readonly ApplicationContext _context;

        public ServiceController(ApplicationContext context)
        {
            _context = context;
        }


        [Authorize]
        [HttpGet(Name = "GetService")]
        public IActionResult GetService(int id)
        {
            Service? service;
            service = _context.Service.Find(id);

            if (service is null)
            {
                return BadRequest("Сервис с таким id не существует.");
            }
            UserService userService = new(service);
            return Ok(userService);
        }

        [Authorize]
        [HttpPost(Name = "PostService")]
        public IActionResult PostService([FromBody] RequestService requestService)
        {
            Service service = new(requestService.Name, requestService.Description, requestService.Cost, PictureConverter.SaveImageGetPath(requestService.Pictures, requestService.Name + "Service"));
            _context.Service.Add(service);
            _context.SaveChanges();
            return Ok("Сервис успешно зарегистрирован.");
        }
    }
    public class RequestService
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public string[] Pictures { get; set; }
        public RequestService(string name, string description, double cost, string[] pictures)
        {
            Name = name;
            Description = description;
            Cost = cost;
            Pictures = pictures;
        }
    }
}
