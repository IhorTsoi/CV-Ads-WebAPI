namespace CV_Ads_WebAPI.Contracts.DTOs.Response
{
    public class AdvertisementForSmartDeviceResponse
    {
        public AdvertisementForSmartDeviceResponse(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public string Name { get; set; }
        public string Url { get; set; }
    }
}
