namespace FactoryAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Phone_number { get; set; }

        public User(string login, string password, string phone_number)
        {
            Login = login;
            Password = password;
            Phone_number = phone_number;
        }
    }
}
