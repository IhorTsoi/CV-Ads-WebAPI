using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Domain.Constants;
using System;

namespace CV_Ads_WebAPI.Domain.Models
{
    public class HumanLimit
    {
        public const int MIN_AGE = 0;
        public const int MAX_AGE = 100;

        private HumanLimit()
        { }

        public HumanLimit(Gender gender, int minAge, int maxAge)
        {
            Id = Guid.NewGuid();
            Gender = gender;
            MinAge = minAge;
            MaxAge = maxAge;
        }

        public Guid Id { get; set; }
        public Gender Gender { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public Guid AdvertisementId { get; set; }

        public Advertisement Advertisement { get; set; }

        public bool IsMatch(FaceRequest face)
        {
            bool genderMatches = Gender == Gender.NotSpecified || face.Gender == Gender;
            bool ageMatches = face.Age >= MinAge && face.Age <= MaxAge;
            return genderMatches && ageMatches;
        }
    }
}
