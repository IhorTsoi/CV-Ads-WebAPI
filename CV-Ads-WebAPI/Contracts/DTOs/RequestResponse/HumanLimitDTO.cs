using CV_Ads_WebAPI.Domain.Constants;

namespace CV_Ads_WebAPI.Contracts.DTOs.RequestResponse
{
    public class HumanLimitDTO
    {
        public Gender? Gender { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
    }
}
