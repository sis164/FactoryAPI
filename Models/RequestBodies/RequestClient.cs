namespace FactoryAPI.Models.RequestBodies
{
    public class RequestClient
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronym { get; set; }

        public RequestClient(string firstName, string secondName, string patronym)
        {
            FirstName = firstName;
            SecondName = secondName;
            Patronym = patronym;
        }
    }
}
