namespace FactoryAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string Phone_number { get; set; }

        public User()
        {
            Login = string.Empty;
            Password = string.Empty;
            Mail = string.Empty;
            Phone_number = string.Empty;
        }

        public User(string login, string password, string mail, string phone_number)
        {
            Login = login;
            Password = password;
            Mail = mail;
            Phone_number = phone_number;
        }
    }
}
