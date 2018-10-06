using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayFit.Models
{
    public class CombinedServiceBookingTimings
    {
        public Service Combined_service { get; set; }
        public ServiceTimings Combined_serviceTimings { get; set; } 
        public ServiceBooking Combined_serviceBooking { get; set; }

    }
}