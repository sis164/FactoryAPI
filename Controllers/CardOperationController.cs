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

    public class RequestCardOperation
    {
        public int CardId { get; set; }
        public int ServiceId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public double ResultCost { get; set; }
        public RequestCardOperation(int cardId, int serviceId, string date, string time, double resultCost)
        {
            CardId = cardId;
            ServiceId = serviceId;
            Date = date;
            Time = time;
            ResultCost = resultCost;
        }
    }
}
