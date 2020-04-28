using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BuenAireSvc
{
    public class MachineLearningModel
    {
        private List<PurpleAirSensor> PurpleAir;
        private List<ThingSpeakSensor> ThingSpeak = new List<ThingSpeakSensor>();

        public async Task IngestData()
        {
            JArray sensors = JArray.Parse(File.ReadAllText(@".\Data\alaska_sensors.json"));
            PurpleAir = sensors.ToObject<List<PurpleAirSensor>>();

            foreach (PurpleAirSensor sensor in PurpleAir)
            {
                ThingSpeak.Add(await ThingSpeakFetch.GetSensorData(sensor.THINGSPEAK_PRIMARY_ID, sensor.THINGSPEAK_PRIMARY_ID_READ_KEY));
            }
        }

        public void OutputImages()
        {
            //Uncomment this when this function is implemented,
            //so that the freshly made images are immediately
            //merged into map tiles
            //ImageStitcher.mergeTiles("Data/MLMOutput");
        }
    }
}
