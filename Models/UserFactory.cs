using FactoryAPI.Utilities;
namespace FactoryAPI.Models
{
    public class UserFactory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<byte[]>? Picture { get; set; }
        public string Phone_number { get; set; }
        public UserFactory(Factory factory)
        {
            Id = factory.Id;
            Name = factory.Name;
            Description = factory.Description;
            Phone_number = factory.Phone_number;
            Picture = PictureConverter.ReadImage(factory.Picture);
        }
    }
}
