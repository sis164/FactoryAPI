using FactoryAPI.Models;
using FactoryAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

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
        public CardOperation GetCardOperation(int id)
        {
            CardOperation? cardOperation;
            cardOperation = _context.CardOperations.Find(id);

            if (cardOperation is null)
            {
                throw new ArgumentNullException(nameof(cardOperation), nameof(cardOperation) + " cannot be null.");
            }

            return cardOperation;
        }

        [HttpPost(Name = "PostCardOperation")]
        
        public void PostCardOperation(int card_id, int service_id, [FromQuery] DateOnly date, [FromQuery] TimeOnly time, decimal result_cost)
        {
            CardOperation cardOperation = new(card_id,service_id,date,time,result_cost);
            _context.CardOperations.Add(cardOperation);
            _context.SaveChanges();
        }
    }
}
