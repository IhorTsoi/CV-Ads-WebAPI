using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Domain.Models
{
    public class TimePeriodLimit
    {
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
