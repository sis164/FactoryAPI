using FactoryAPI.Models;
using FactoryAPI.Utilities;
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

        [HttpGet(Name = "GetCard")]
        public Card GetCard(int id)
        {
            Card? card;
            card = _context.Card.Find(id);

            if (card is null)
            {
                throw new ArgumentNullException(nameof(card), nameof(card) + " cannot be null.");
            }

            return card;
        }

        [HttpPost(Name = "PostCard")]
        public void PostCard(int Client_id)
        {
            CodeGenerator codeGenerator = new(_context);

            Card card = new(Client_id, codeGenerator.GenerateCode());

            _context.Add(card);
            _context.SaveChanges();
        }
    }
}
