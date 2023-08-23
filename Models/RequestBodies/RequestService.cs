namespace FactoryAPI.Models.RequestBodies
{
    public class RequestService
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public string[] Pictures { get; set; }
        public RequestService(string name, string description, double cost, string[] pictures)
        {
            Name = name;
            Description = description;
            Cost = cost;
            Pictures = pictures;
        }
    }
}
