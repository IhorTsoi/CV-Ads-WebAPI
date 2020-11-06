using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
