namespace FactoryAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public int[] Factorys_id { get; set; }
        public string First_name { get; set; }
        public string Second_name { get; set; }
        public string Patronym { get; set;}
        public string Specialization { get; set; }
    }
}
