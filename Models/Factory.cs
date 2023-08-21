﻿namespace FactoryAPI.Models
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
            Services = Array.Empty<int>();
            Employee_id = new List<int>();
        }
    }
}
