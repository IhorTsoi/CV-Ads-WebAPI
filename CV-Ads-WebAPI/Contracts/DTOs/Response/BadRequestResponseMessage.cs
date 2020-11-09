using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
