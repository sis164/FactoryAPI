using FactoryAPI.Models;
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

        [HttpGet(Name = "GetCardOperation")]
        public IActionResult GetCardOperation(int id)
        {
            var cardOperation = _context.CardOperations.Find(id);

            if (cardOperation is null)
            {
                return BadRequest("Нет операции карт с таким id");
            }

            return Ok(cardOperation);
        }

        [HttpPost(Name = "PostCardOperation")]
        public IActionResult PostCardOperation(int card_id, int service_id, string date, string time, double result_cost)
        {
            CardOperation cardOperation = new(card_id, service_id, date, time, result_cost);
            _context.CardOperations.Add(cardOperation);
            _context.SaveChanges();

            return Ok(cardOperation);
        }
    }
}
