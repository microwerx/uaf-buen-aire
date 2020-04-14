using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.GoogleMaps;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace buenaire
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurpleAirPage : ContentPage
    {
        private static HttpClient client = new HttpClient();

        public PurpleAirPage()
        {
            Map map = new Map();

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(64.83d, -147.71d), Distance.FromMeters(10000)), false);

            map.CameraIdled += async (sender, args) =>
            {
                string response = await client.GetStringAsync("https://uafbuenaire.azurewebsites.net/api/purpleair/simple/" + map.Region.NearLeft.Latitude +
                    "/" + map.Region.NearLeft.Longitude + "/" + map.Region.FarRight.Latitude + "/" + map.Region.FarRight.Longitude);
                JArray json = JArray.Parse(response);

                if (DateTime.Now.Minute % 5 == 0)
                {
                    foreach (JObject sensor in json)
                    {
                        refreshSensorData(map, sensor);
                    }
                }

                foreach (JObject sensor in json)
                {
                    addSensors(map, sensor);
                }
            };

            Content = map;
        }

        private void refreshSensorData(Map map, JObject sensor)
        {
            string label = sensor.Value<string>("Label");
            int pinIndex = -1;
            for (int i = 0; i < map.Pins.Count; ++i)
            {
                if (map.Pins[i].Label == label)
                {
                    pinIndex = i;
                    break;
                }
            }

            if (pinIndex >= 0)
            {
                map.Pins[pinIndex].Label = label + " (PM 2.5: " + sensor.Value<string>("PM2_5") + "ug/m^3)";
            }
        }

        private void addSensors(Map map, JObject sensor)
        {
            double lat = sensor.Value<double>("Lat");
            double lon = sensor.Value<double>("Lon");
            Pin pin = new Pin() { Position = new Position(lat, lon), Label = sensor.Value<string>("Label") };
            if (!map.Pins.Contains(pin))
            {
                map.Pins.Add(pin);
                refreshSensorData(map, sensor);
            }
        }
    }
}
