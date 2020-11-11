namespace CV_Ads_WebAPI.Contracts.DTOs.Request
{
    public class ActivateSmartDeviceRequest
    {
        public string SerialNumber { get; set; }
        public string DefaultPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
