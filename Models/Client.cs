namespace FactoryAPI.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string First_name { get; set; }
        public string Second_name { get; set; }
        public string Patronym { get; set; }
        public string Phone_number { get; set; }

        public Client()
        {
            First_name = string.Empty;
            Second_name = string.Empty;
            Patronym = string.Empty;
            Phone_number = string.Empty;
        }
        public Client(string first_name, string second_name, string patronym, string phone_number)
        {
            First_name = first_name;
            Second_name = second_name;
            Patronym = patronym;
            Phone_number = phone_number;
        }
    }
}
