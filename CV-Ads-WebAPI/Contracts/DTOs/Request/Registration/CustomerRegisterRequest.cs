namespace CV_Ads_WebAPI.Contracts.DTOs.Request.Registration
{
    public class CustomerRegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
