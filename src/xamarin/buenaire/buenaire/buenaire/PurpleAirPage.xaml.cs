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
using GoogleApi;

namespace buenaire
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurpleAirPage : ContentPage
    {
        private static HttpClient client = new HttpClient();

        public PurpleAirPage()
        {
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

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

            SearchBar placesBar = new SearchBar
            { 
                Placeholder = "Search a location...",
                TextColor = Color.Black,
                PlaceholderColor = Color.Black,
                CancelButtonColor = Color.Black,
                BackgroundColor = Color.WhiteSmoke,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(SearchBar)),
                FontAttributes = FontAttributes.Italic,
            };

            grid.Children.Add(map, 0, 1, 0, 2);
            grid.Children.Add(placesBar, 0, 0);

            Content = grid;
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
