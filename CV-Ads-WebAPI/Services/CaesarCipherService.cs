using CV_Ads_WebAPI.Services.Interfaces;
using System;
using System.Linq;

namespace CV_Ads_WebAPI.Services
{
    public class CaesarCipherService : ICipherService
    {
        private const int OFFSET = 5;

        public string EncodeString(string originalString)
        {
            char[] encodedCharArray = originalString.Select(c => Convert.ToChar(c + OFFSET)).ToArray();
            return new string(encodedCharArray);
        }

        public string DecodeString(string encodedString)
        {
            char[] originalString = encodedString.Select(c => Convert.ToChar(c - OFFSET)).ToArray();
            return new string(originalString);
        }
    }
}
