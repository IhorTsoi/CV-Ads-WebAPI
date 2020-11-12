using System;

namespace CV_Ads_WebAPI.Domain.Models.Users.Base
{
    public abstract class BaseUser
    {
        protected BaseUser()
        { }

        protected BaseUser(string login, string password, string role)
        {
            Id = Guid.NewGuid();
            UserIdentity = new UserIdentity(Id, login, password, role);
        }

        public Guid Id { get; set; }
        public UserIdentity UserIdentity { get; set; }
    }
}
