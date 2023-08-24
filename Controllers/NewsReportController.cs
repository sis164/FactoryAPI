using Microsoft.AspNetCore.Mvc;
using FactoryAPI.Models.RequestBodies;
using FactoryAPI.Models;
using FactoryAPI.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
            var newsreport = _context.NewsReport.Find(id);

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
                Factory_Id = requestReport.Factory_Id,
                Service_Id = requestReport.Service_Id,
                Employee_Id = requestReport.Employee_Id,
                Description = requestReport.Description,
                Likes = requestReport.Likes,
                Pictures = (requestReport.Pictures is null) ? null : PictureConverter.SaveImageGetPath(requestReport.Pictures, _context.Factory.Find(requestReport.Factory_Id)!.Name + "NewsReport")
            };
            _context.NewsReport.Add(newsreport);
            _context.SaveChanges();

            return Ok("Пост зарегистрирован.");
        }
    }
}
