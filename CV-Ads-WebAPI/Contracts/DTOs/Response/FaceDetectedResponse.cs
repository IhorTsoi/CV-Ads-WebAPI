using CV_Ads_WebAPI.Domain.Constants;

namespace CV_Ads_WebAPI.Contracts.DTOs.Response
{
    public class FaceDetectedResponse
    {
        public FaceDetectedResponse(Gender gender, int? age)
        {
            Gender = gender;
            Age = age;
        }

        public Gender Gender { get; set; }
        public int? Age { get; set; }
    }
}
