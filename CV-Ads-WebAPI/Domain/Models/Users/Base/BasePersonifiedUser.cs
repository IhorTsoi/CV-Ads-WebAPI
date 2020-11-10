namespace CV_Ads_WebAPI.Domain.Models.Users.Base
{
    public class BasePersonifiedUser : BaseUser
    {
        protected BasePersonifiedUser()
        { }

        public BasePersonifiedUser(string login, string password, string role, string firstName, string lastName)
            : base(login, password, role)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
