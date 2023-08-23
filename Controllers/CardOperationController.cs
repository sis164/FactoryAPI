using FactoryAPI.Models.RequestBodies;
using FactoryAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactoryAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class CardOperationController : Controller
    {
        private readonly ApplicationContext _context;
        public CardOperationController(ApplicationContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetCardOperation(int id)
        {
            var cardOperation = _context.CardOperations.Find(id);

            if (cardOperation is null)
            {
                return BadRequest("Нет операции карт с таким id.");
            }

            return Ok(cardOperation);
        }

        [Authorize]
        [HttpPost]
        public IActionResult PostCardOperation([FromBody] RequestCardOperation requestCardOperation)
        {
            CardOperation cardOperation = new(
                requestCardOperation.CardId,
                requestCardOperation.ServiceId,
                requestCardOperation.Date,
                requestCardOperation.Time,
                requestCardOperation.ResultCost);

            _context.CardOperations.Add(cardOperation);
            _context.SaveChanges();

            return Ok("Операция карт успешно зарегистрирована.");
        }
    }
}
