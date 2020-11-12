using CV_Ads_WebAPI.Domain.Constants;

namespace CV_Ads_WebAPI.Contracts.DTOs.Request
{
    public class ChangeStatusOfAdvertisementRequest
    {
        public AdvertisementStatus? NewAdvertisementStatus { get; set; }
    }
}
