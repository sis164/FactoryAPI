using FactoryAPI.Models.RequestBodies;
using FactoryAPI.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace FactoryAPI.Models
{
    public class NewsReport
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Pictures { get; set; }
        public int Service_Id { get; set; }
        public int Factory_Id { get; set; }
        public int Employee_Id { get; set; }
        public int Likes { get; set; }

        public NewsReport()
        {
            Description = string.Empty;
            Pictures = string.Empty;
        }
        public NewsReport(RequestNewsReport requestReport)
        {
            Factory_Id = requestReport.Factory_Id;
            Service_Id = requestReport.Service_Id;
            Employee_Id = requestReport.Employee_Id;
                Description = requestReport.Description;
            Likes = requestReport.Likes;
        }

    }
}
