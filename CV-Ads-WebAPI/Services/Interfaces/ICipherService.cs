namespace CV_Ads_WebAPI.Services.Interfaces
{
    public interface ICipherService
    {
        public string EncodeString(string originalString);
        public string DecodeString(string encodedString);
    }
}
