using System;

namespace CV_Ads_WebAPI.Domain.Models
{
    public class TimePeriodLimit
    {
        public const int MIN_IN_MINUTES = 0;
        public const int MAX_IN_MINUTES = 1439;

        private TimePeriodLimit()
        { }

        public TimePeriodLimit(int fromInMinutes, int toInMinutes)
        {
            Id = Guid.NewGuid();
            FromInMinutes = fromInMinutes;
            ToInMinutes = toInMinutes;
        }

        public Guid Id { get; set; }
        public int FromInMinutes { get; set; }
        public int ToInMinutes { get; set; }

        public Advertisement Advertisement { get; set; }
    }
}
