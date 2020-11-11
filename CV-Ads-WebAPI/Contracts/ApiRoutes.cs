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
            public const string GetPaymentAmount = Base + "/payment";
            public const string Pay = Base + "/payment";
        }

        public static class Partner
        {
            private const string Base = "partners";

            public const string Register = Base + "/register";
            public const string Login = Base + "/login";

            public const string GetRevenueAmount = Base + "/revenue";
            public const string WithdrawRevenue = Base + "/revenue";
        }

        public static class SmartDevice
        {
            private const string Base = "smartdevices";

            public const string GetAll = Base;
            public const string Register = Base + "/register";
            public const string Login = Base + "/login";
            public const string Reset = Base + "/{smartDeviceId}/reset";
            public const string Activate = Base + "/activate";
            public const string UpdateConfiguration = Base + "/{smartDeviceId}";
            public const string Block = Base + "/{smartDeviceId}/block";
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
            public const string GetAdvertisementByEnvironment = Base + "byenvironment";
            public const string GetAllPersonalAdvertisements = Base;
        }

        public static class ServiceData
        {
            private const string Base = "servicedata";

            public const string Export = Base;
            public const string Import = Base;
        }
    }
}
