using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BuenAireSvc
{
    public class ThingSpeakFetch
    {
        private static readonly HttpClient client = new HttpClient();

        async public static Task<ThingSpeakSensor> GetSensorData(string ID, string ApiKey)
        {
            var response = await client.GetStringAsync("https://api.thingspeak.com/channels/" + ID + "/feeds.json?api_key=" + ApiKey);

            if (response != null)
            {
                JObject obj = JObject.Parse(response);

                var pmData =
                    from entry in obj["feeds"]
                    select new ThingSpeakDataPoint
                    {
                        CreatedAt = Convert.ToDateTime(entry["created_at"]),
                        PM1Point0 = (string)entry["field1"],
                        PM2Point5 = (string)entry["field2"],
                        PM10 = (string)entry["field3"]
                    };

                ThingSpeakSensor returnTest = new ThingSpeakSensor{ ID = ID, Data = pmData.ToList() };
                return returnTest;
            }

            return new ThingSpeakSensor();
        }
    }
}
