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
        
        public Employee()
        {
            Factorys_id = Array.Empty<int>();
            First_name = string.Empty; 
            Second_name = string.Empty; 
            Patronym = string.Empty;
            Specialization = string.Empty;
        }
    }
}
