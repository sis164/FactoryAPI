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
        public int Likes {get; set; }
    }
}
