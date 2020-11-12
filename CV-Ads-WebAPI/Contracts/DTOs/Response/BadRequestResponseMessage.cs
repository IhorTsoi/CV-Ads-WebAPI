namespace CV_Ads_WebAPI.Contracts.DTOs.Response
{
    public class BadRequestResponseMessage
    {
        public BadRequestResponseMessage(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
