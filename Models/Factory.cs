namespace FactoryAPI.Models
{
    public class Factory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string Phone_number { get; set; }
        public int[]? Services { get; set; }
        public List<int>? Employee_id { get; set; }

        public Factory()
        {
            Name = string.Empty;
            Description = string.Empty;
            Picture = string.Empty;
            Phone_number = string.Empty;
            Services = new int[0];
            Employee_id = new List<int>();
        }
        public Factory(string name, string description, string picture, string phone_number, int[] services, List<int>? employee_id)
        {
            Name = name;
            Description = description;
            Picture = picture;
            Phone_number = phone_number;
            Services = services;
            Employee_id = employee_id;
        }
        public Factory(int id, string name, string description, string picture, string phone_number, int[] services, List<int> employee_id)
        {
            Id = id;
            Name = name;
            Description = description;
            Picture = picture;
            Phone_number = phone_number;
            Services = services;
            Employee_id = employee_id;
        }
    }
}
