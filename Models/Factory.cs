namespace FactoryAPI.Models
{
    public class Factory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string Phone_number { get; set; }

        public Factory()
        {
            Name = string.Empty;
            Description = string.Empty;
            Picture = string.Empty;
            Phone_number = string.Empty;
        }
        public Factory(string name, string description, string picture, string phone_number)
        {
            Name = name;
            Description = description;
            Picture = picture;
            Phone_number = phone_number;
        }
        public Factory(int id, string name, string description, string picture, string phone_number)
        {
            Id = id;
            Name = name;
            Description = description;
            Picture = picture;
            Phone_number = phone_number;
        }
    }
}
