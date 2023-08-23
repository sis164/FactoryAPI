namespace FactoryAPI.Models.RequestBodies
{
    public class RequestEmployee
    {
        public int[] FactoryId { get; set; }
        public string First_name { get; set; }
        public string Second_name { get; set; }
        public string Patronym { get; set; }
        public string Specialization { get; set; }
        public RequestEmployee(int[] factoryId, string first_name, string second_name, string patronym, string specialization)
        {
            FactoryId = factoryId;
            First_name = first_name;
            Second_name = second_name;
            Patronym = patronym;
            Specialization = specialization;
        }
    }
}
