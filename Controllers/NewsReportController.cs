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
        [HttpGet]
        public IActionResult GetNewsReport([FromQuery] int id)
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
        [HttpPost]
        [Route("add-like")]
        public IActionResult AddLike([FromBody] int newsReportId)
        {
            var newsReport = _context.NewsReport.Find(newsReportId);
            if (newsReport is null)
            {
                return BadRequest("Нет поста с таким id");
            }
            var factory = _context.Factory.Find(newsReport.Factory_Id);
            if (factory is null)
            {
                return BadRequest("Неправильно привязан пост к объекту");
            }
            newsReport.Likes++;
            factory.Total_likes++;
            _context.SaveChanges();
            return Ok("Лайк успешно поставлен.");
        }

        [Authorize]
        [HttpPost]
        [Route("delete-like")]
        public IActionResult DeleteLike([FromBody] int newsReportId)
        {
            var newsReport = _context.NewsReport.Find(newsReportId);
            if (newsReport is null)
            {
                return BadRequest("Нет поста с таким id");
            }
            var factory = _context.Factory.Find(newsReport.Factory_Id);
            if (factory is null)
            {
                return BadRequest("Неправильно привязан пост к объекту");
            }
            if (newsReport.Likes > 0)
                newsReport.Likes--;
            if (factory.Total_likes > 0)
                factory.Total_likes--;
            _context.SaveChanges();
            return Ok("Лайк успешно убран.");
        }

    //    [Authorize]
        [HttpPost]
        public IActionResult PostNewsReport([FromBody] RequestNewsReport requestReport)
        {
            if (requestReport is null)
            {
                return BadRequest("Данные введены не корекктно");
            }

            var factory = _context.Factory.Find(requestReport.Factory_Id);
            if (factory is null)
            {
                return BadRequest("Нет объекта с таким id");
            }

            //var service = _context.Service.Find(requestReport.Service_Id);
            //if (service is null)
            //{
            //    return BadRequest("Нет услуги с таким id");
            //}

            //var employee = _context.Employee.Find(requestReport.Employee_Id);
            //if (employee is null)
            //{
            //    return BadRequest("Нет работника с таким id");
            //}

            NewsReport newsReport = new(requestReport)
            {
                Pictures = (requestReport.Pictures is null) ? null : PictureConverter.SaveImageGetPath(requestReport.Pictures, factory.Name + "NewsReport")
            };
            _context.NewsReport.Add(newsReport);
            _context.SaveChanges();

            return Ok("Пост зарегистрирован.");
        }
    }
}
