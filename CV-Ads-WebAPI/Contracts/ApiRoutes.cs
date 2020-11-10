namespace CV_Ads_WebAPI.Contracts
{
    public static class ApiRoutes
    {
        public static class Admin
        {
            private const string Base = "admins";

            public const string Register = Base + "/register";
            public const string Login = Base + "/login";
        }

        public static class Customer
        {
            private const string Base = "customers";

            public const string Register = Base + "/register";
            public const string Login = Base + "/login";
        }

        public static class Partner
        {
            private const string Base = "partners";

            public const string Register = Base + "/register";
            public const string Login = Base + "/login";
        }

        public static class SmartDevice
        {
            private const string Base = "smartdevices";

            public const string Register = Base + "/register";
            public const string Login = Base + "/login";
            public const string Reset = Base + "/reset/{smartDeviceId}";
        }

        public static class FaceDetection
        {
            private const string Base = "facedetection";

            public const string DetectFacesOnPhoto = Base;
        }

        public static class Advertisement
        {
            private const string Base = "advertisements";

            public const string CreateAdvertisement = Base;
            public const string ChangeStatusOfAdvertisement = Base + "/{advertisementId}";
        }
    }
}
