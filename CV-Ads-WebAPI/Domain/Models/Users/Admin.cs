﻿using CV_Ads_WebAPI.Domain.Constants;
using CV_Ads_WebAPI.Domain.Models.Users.Base;

namespace CV_Ads_WebAPI.Domain.Models
{
    public class Admin : BasePersonifiedUser
    {
        private Admin()
        { }

        public Admin(string login, string password, string firstName, string lastName)
            : base(login, password, Roles.ADMIN, firstName, lastName)
        { }
    }
}
