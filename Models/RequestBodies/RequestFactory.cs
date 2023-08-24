using FactoryAPI.Utilities;

namespace FactoryAPI.Models.RequestBodies
{
    public class RequestFactory
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone_number { get; set; }
        public string[] Pictures { get; set; }
        public RequestFactory(string name, string description, string phone_number, string[] pictures)
        {
            Name = name;
            Description = description;
            Phone_number = phone_number;
            Pictures = pictures;
        }
        public RequestFactory (Factory factory)
        {
            Name = factory.Name;
            Description = factory.Description;
            Phone_number = factory.Phone_number;
            Pictures = PictureConverter.ReadImageNotNull(factory.Picture);
        }
    }
}
