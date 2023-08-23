﻿using FactoryAPI.Models;
using FactoryAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactoryAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class CardController : Controller
    {
        private readonly ApplicationContext _context;
        public CardController(ApplicationContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetCard(int id)
        {
            var card = _context.Card.Find(id);

            if (card is null)
            {
                return BadRequest("Нет карты с таким id");
            }

            return Ok(card);
        }

        [Authorize]
        [HttpPost]
        public IActionResult PostCard(int Client_id)
        {
            if (_context.Client.Find(Client_id) is null)
            {
                return BadRequest("Клиент с таким id не существует.");
            }

            CodeGenerator codeGenerator = new(_context);

            Card card = new(Client_id, codeGenerator.GenerateCode());

            _context.Add(card);
            _context.SaveChanges();

            return Ok("Карта зарегистрирована успешно");
        }
    }
}
