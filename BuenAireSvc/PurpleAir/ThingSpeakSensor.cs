using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuenAireSvc
{
    public partial class ThingSpeakSensor
    {
        public string ID { get; set; }
        public List<ThingSpeakDataPoint> Data { get; set; }
    }
}
