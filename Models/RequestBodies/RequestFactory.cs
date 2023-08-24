using FactoryAPI.Utilities;

namespace FactoryAPI.Models.RequestBodies
{
    public class RequestFactory
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone_number { get; set; }
        public string[] Pictures { get; set; }
        public RequestFactory()
        {
            Name = string.Empty;
            Description = string.Empty;
            Phone_number = string.Empty;
            Pictures = Array.Empty<string>();
        }
        public RequestFactory(Factory factory)
        {
            Name = factory.Name;
            Description = factory.Description;
            Phone_number = factory.Phone_number;
            Pictures = PictureConverter.ReadImageNotNull(factory.Picture);
        }
    }
}
