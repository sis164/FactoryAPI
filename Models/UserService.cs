using FactoryAPI.Utilities;

namespace FactoryAPI.Models
{
    public class UserService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public List<byte[]>? Pictures { get; set; }
        public UserService(Service service)
        {
            Name = service.Name;
            Description = service.Description;
            Cost = service.Cost;
            Pictures = PictureConverter.ReadImage(service.Pictures);
        }
    }
}
