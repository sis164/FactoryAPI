using FactoryAPI.Utilities;
namespace FactoryAPI.Models
{
    public class UserFactory
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string[]? Picture { get; set; }
        public string Phone_number { get; set; }
        public UserFactory(Factory factory)
        {
            Name = factory.Name;
            Description = factory.Description;
            Phone_number = factory.Phone_number;
            //Picture = PictureConverter.ReadImage(factory.Picture);
        }
    }
}
