using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Contracts.DTOs.Request
{
    public class FaceDetectionRequest
    {
        public IFormFile FormFile { get; set; }
    }
}
