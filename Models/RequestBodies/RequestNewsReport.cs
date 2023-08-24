using FactoryAPI.Utilities;

namespace FactoryAPI.Models.RequestBodies
{
    public class RequestNewsReport
    {
        public string? Description { get; set; }
        public string[]? Pictures { get; set; }
        public int Service_Id { get; set; }
        public int Factory_Id { get; set; }
        public int Employee_Id { get; set; }
        public int Likes { get; set; }

        public RequestNewsReport()
        {

        }

        public RequestNewsReport(string? description, string[]? pictures, int service_Id, int factory_Id, int employee_Id, int likes)
        {
            Description = description;
            Pictures = pictures;
            Service_Id = service_Id;
            Factory_Id = factory_Id;
            Employee_Id = employee_Id;
            Likes = likes;
        }
        public RequestNewsReport(NewsReport report) 
        {
            this.Description = report.Description;
            this.Pictures = PictureConverter.ReadImage(report.Pictures);
            this.Likes = report.Likes;
            this.Service_Id = report.Service_Id;
            this.Employee_Id = report.Employee_Id;
            this.Factory_Id = report.Factory_Id;

        }
    }
}
