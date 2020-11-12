using System;

namespace CV_Ads_WebAPI.Domain.Models
{
    public class UserIdentity
    {
        private UserIdentity()
        { }

        public UserIdentity(Guid userId, string login, string password, string role)
        {
            Id = userId;
            Login = login;
            Password = password;
            Role = role;
        }

        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
