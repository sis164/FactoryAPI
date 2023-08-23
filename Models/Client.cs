namespace FactoryAPI.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string First_name { get; set; }
        public string Second_name { get; set; }
        public string Patronym { get; set; }
        public Client(string first_name, string second_name, string patronym)
        {
            First_name = first_name;
            Second_name = second_name;
            Patronym = patronym;

        }
    }
}
