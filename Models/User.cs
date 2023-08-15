﻿namespace FactoryAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }

        public User()
        {
            Login = string.Empty;
            Password = string.Empty;
            Mail = string.Empty;
        }

        public User(string login, string password, string mail)
        {
            Login = login;
            Password = password;
            Mail = mail;
        }
    }
}
