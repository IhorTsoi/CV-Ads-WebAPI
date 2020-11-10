using CV_Ads_WebAPI.Domain.Constants;
using CV_Ads_WebAPI.Domain.Models.Users.Base;
using System;
using System.Collections.Generic;

namespace CV_Ads_WebAPI.Domain.Models
{
    public class Customer : BasePersonifiedUser
    {
        private Customer()
        { }

        public Customer(string login, string password, string firstName, string lastName)
            : base(login, password, Roles.CUSTOMER, firstName, lastName)
        {
            LastPaidDate = DateTime.UtcNow;
            Advertisements = new List<Advertisement>();
        }

        public DateTime LastPaidDate { get; set; }
        public List<Advertisement> Advertisements { get; set; }
    }
}
