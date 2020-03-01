using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BuenAireSvc
{
    public class SensorFetchJob : IJob
    {
        private static readonly HttpClient client = new HttpClient();

        async public Task Execute(IJobExecutionContext context)
        {
            var response = await client.GetStringAsync("https://www.purpleair.com/json");

            if (response != null)
            {
                JObject obj = JObject.Parse(response);

                var alaskaSensors =
                    from sensor in obj["results"]
                    where Convert.ToDouble(sensor["Lat"]) > 51 && Convert.ToDouble(sensor["Lon"]) < -130
                    select sensor;

                File.WriteAllText(@".\Data\purpleair_sensors.json", response);
                File.WriteAllText(@".\Data\alaska_sensors.json", JsonConvert.SerializeObject(alaskaSensors));
            }
        }
    }
}
