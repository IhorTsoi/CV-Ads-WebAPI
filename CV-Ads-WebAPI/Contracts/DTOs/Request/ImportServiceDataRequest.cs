using Microsoft.AspNetCore.Http;

namespace CV_Ads_WebAPI.Contracts.DTOs.Request
{
    public class ImportServiceDataRequest
    {
        public IFormFile FormFile { get; set; }
    }
}
