using System;
using System.Collections.Generic;

namespace BuenAireSvc
{
    public class ThingSpeakDataPoint
    {
        public DateTime CreatedAt { get; set; }
        public string PM1Point0 { get; set; }
        public string PM2Point5 { get; set; }
        public string PM10 { get; set; }
    }
}
