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

        async public static Task getSensorData(string ID, string ApiKey)
        {
            var response = await client.GetStringAsync("https://api.thingspeak.com/channels/" + ID + "/feeds.json?api_key=" + ApiKey);

            if (response != null)
            {
                JObject obj = JObject.Parse(response);

                var pm2point5 =
                    from entry in obj["feeds"]
                    select entry;

                File.WriteAllText(@".\Data\Feeds\" + ID + ".json", JsonConvert.SerializeObject(pm2point5));
            }
        }
    }
}
