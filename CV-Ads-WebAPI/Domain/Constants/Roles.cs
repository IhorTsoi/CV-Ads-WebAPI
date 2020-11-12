namespace CV_Ads_WebAPI.Domain.Constants
{
    public static class Roles
    {
        public const string ADMIN = "ADMIN";
        public const string CUSTOMER = "CUSTOMER";
        public const string PARTNER = "PARTNER";
        public const string SMART_DEVICE = "SMART_DEVICE";

        public static string[] GetRoles()
        {
            return new string[] {
                ADMIN,
                CUSTOMER,
                PARTNER,
                SMART_DEVICE
            };
        }
    }
}
