using System;

namespace CV_Ads_WebAPI.Domain.Models
{
    public class HumanLimit
    {
        private HumanLimit()
        { }

        public HumanLimit(Gender gender, int? minAge, int? maxAge)
        {
            Id = Guid.NewGuid();
            Gender = gender;
            MinAge = minAge;
            MaxAge = maxAge;
        }

        public Guid Id { get; set; }
        public Gender Gender { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }

        public Advertisement Advertisement { get; set; }
    }
}
