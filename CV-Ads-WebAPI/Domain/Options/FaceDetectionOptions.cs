namespace CV_Ads_WebAPI.Domain.Options
{
    public class FaceDetectionOptions
    {
        public const string SectionName = "FaceDetectionOptions";

        public string SubscriptionKey { get; set; }
        public string Endpoint { get; set; }
    }
}
