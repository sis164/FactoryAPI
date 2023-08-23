namespace FactoryAPI.Models.RequestBodies
{
    public class RequestUser
    {
        public string HashCode { get; set; }
        public int Code { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Phone_number { get; set; }
        public RequestUser(string login, string password, string phone_number, string hashCode, int code)
        {
            Login = login;
            Password = password;
            Phone_number = phone_number;
            HashCode = hashCode;
            Code = code;
        }
    }
}
