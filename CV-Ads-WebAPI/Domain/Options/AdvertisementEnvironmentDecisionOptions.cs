namespace CV_Ads_WebAPI.Domain.Options
{
    public class AdvertisementEnvironmentDecisionOptions
    {
        public const string SectionName = "AdvertisementEnvironmentDecisionOptions";

        public float AmountOfTargetAudienceWeight { get; set; }
        public float AmountOfWorkRemainsWeight { get; set; }
    }
}
