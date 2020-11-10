namespace CV_Ads_WebAPI.Contracts.DTOs.Request.AdvertisementCreation
{
    public class TimePeriodLimitRequest
    {
        public int? FromInMinutes { get; set; }
        public int? ToInMinutes { get; set; }
    }
}
