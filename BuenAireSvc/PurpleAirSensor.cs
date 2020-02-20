using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuenAireSvc
{
    public class PurpleAirSensor
    {
        public int ID { get; set; }
        public string Label { get; set; }
        public string THINGSPEAK_PRIMARY_ID { get; set; }
        public string THINGSPEAK_PRIMARY_ID_READ_KEY { get; set; }
        public string THINGSPEAK_SECONDARY_ID { get; set; }
        public string THINGSPEAK_SECONDARY_ID_READ_KEY { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
