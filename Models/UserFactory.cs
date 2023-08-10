namespace FactoryAPI.Models
{
    public class UserFactory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[]? Picture { get; set; }
        public string Phone_number { get; set; }

        public UserFactory()
        {
            Name = string.Empty;
            Description = string.Empty;
            Phone_number = string.Empty;
            Picture = Array.Empty<byte>();
        }
        public UserFactory(Factory factory)
        {
            Id = factory.Id;
            Name = factory.Name;
            Description = factory.Description;
            Phone_number = factory.Phone_number;
            try
            {
                Picture = File.ReadAllBytes(factory.Picture);
            }
            catch (FileNotFoundException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
