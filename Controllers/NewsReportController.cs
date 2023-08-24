using Microsoft.AspNetCore.Mvc;
using FactoryAPI.Models.RequestBodies;
using FactoryAPI.Models;
using FactoryAPI.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;

namespace FactoryAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class NewsReportController : Controller
    {
        private readonly ApplicationContext _context;

        public NewsReportController(ApplicationContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet(Name = "GetNewsReport")]
        public IActionResult GetFactory([FromQuery] int id)
        {
            var newsreport = _context.NewsReports.Find(id);

            if (newsreport is null)
            {
                return BadRequest("Поста с таким id не существует.");
            }

            RequestNewsReport requestreport = new(newsreport);

            return Ok(requestreport);
        }

        [Authorize]
        [HttpPost(Name = "PostNewsReport")]
        public IActionResult PostFactory([FromBody] RequestNewsReport requestReport)
        {
            if (requestReport is null)
            {
                return BadRequest("Данные введены не корекктно");
            }
            
            NewsReport newsreport = new()
            {
                Service_Id = requestReport.Service_Id,
                Employee_Id = requestReport.Employee_Id,
                Description = requestReport.Description,
                Likes = requestReport.Likes,
            };
            if (requestReport.Pictures is null)
            {
                newsreport.Pictures = null;
            }
            else 
            {
                newsreport.Pictures = PictureConverter.SaveImageGetPath(requestReport.Pictures, _context.Employee.Find(requestReport.Employee_Id) + "NewsReport");
            }
            _context.NewsReports.Add(newsreport);
            _context.SaveChanges();

            return Ok("Предприятие зарегистрировано");
        }
    }
}
