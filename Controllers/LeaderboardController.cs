using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace FactoryAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class LeaderboardController : Controller
    {
        private readonly ApplicationContext _context;
        public LeaderboardController(ApplicationContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetLeaderBoard()
        {
            var list = _context.Factory.Where(factory => factory.Total_likes > 0).OrderByDescending(factory => factory.Total_likes).ToList();
            return Ok(list.ToJson());
        }
    }
}
