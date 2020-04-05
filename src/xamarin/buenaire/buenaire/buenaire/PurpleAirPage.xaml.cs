﻿using System;
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
        private double swLat, swLon;
        private double neLat, neLon;

        public PurpleAirPage()
        {
            Map map = new Map();

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(64.83d, -147.71d), Distance.FromMeters(10000)), false);

            var circle1 = new Circle();
            circle1.Center = new Position(64.87d, -147.62d);
            circle1.Radius = Distance.FromMeters(3000f);
            circle1.StrokeColor = Color.Blue;
            circle1.StrokeWidth = 6f;
            circle1.FillColor = Color.FromRgba(137, 196, 244, 96);
            Pin pin1 = new Pin()
            {
                Position = new Position(64.87d, -147.62d),
                Label = "15",
                Icon = BitmapDescriptorFactory.FromView(new ContentView { HeightRequest = 50, WidthRequest = 50, Content = new Image { Source = ImageSource.FromFile("@drawable/0_50.png") } })
                //Icon = BitmapDescriptorFactory.FromBundle("Fifty")
                };
            map.Pins.Add(pin1);
            map.Circles.Add(circle1);

            var circle2 = new Circle();
            circle2.Center = new Position(64.87d, -147.81d);
            circle2.Radius = Distance.FromMeters(3000f);
            circle2.StrokeColor = Color.Purple;
            circle2.StrokeWidth = 6f;
            circle2.FillColor = Color.FromRgba(0, 0, 255, 32);
            Pin pin2 = new Pin()
            {
                Position = new Position(64.87d, -147.81d),
                Label = "55",
                //Icon = BitmapDescriptorFactory.FromBundle("purpleair")
            };
            map.Pins.Add(pin2);
            map.Circles.Add(circle2);

            var circle3 = new Circle();
            circle3.Center = new Position(64.81d, -147.62d);
            circle3.Radius = Distance.FromMeters(3000f);
            circle3.StrokeColor = Color.Red;
            circle3.StrokeWidth = 6f;
            circle3.FillColor = Color.FromRgba(240, 52, 52, 32);
            Pin pin3 = new Pin()
            {
                Position = new Position(64.81d, -147.62d),
                Label = "155"
            };
            map.Pins.Add(pin3);
            map.Circles.Add(circle3);

            var circle4 = new Circle();
            circle4.Center = new Position(64.81d, -147.81d);
            circle4.Radius = Distance.FromMeters(3000f);
            circle4.StrokeColor = Color.Yellow;
            circle4.StrokeWidth = 6f;
            circle4.FillColor = Color.FromRgba(255, 255, 126, 96);
            Pin pin4 = new Pin()
            {
                Position = new Position(64.81d, -147.81d),
                Label = "1"
            };
            map.Pins.Add(pin4);
            map.Circles.Add(circle4);

            map.CameraIdled += async (sender, args) =>
            {
                /*if ( DateTime.Now.Minute % 2 == 0)
                {
                    return;
                }*/

                //double dLat = Math.Abs(map.Region.NearLeft.Latitude - map.Region.FarLeft.Latitude);
                //double dLon = Math.Abs(map.Region.NearLeft.Longitude - map.Region.NearRight.Longitude);
                //if (Math.Abs(map.Region.NearLeft.Latitude - swLat) < (dLat/2)
                //    && Math.Abs(map.Region.NearLeft.Longitude - swLon) < (dLon/2))
                //{
                //    return;
                //}
                
                //swLat = map.Region.NearLeft.Latitude;
                //swLon = map.Region.NearLeft.Longitude;
                //neLat = map.Region.FarRight.Latitude;
                //neLon = map.Region.FarRight.Longitude;

                string response = await client.GetStringAsync("https://uafbuenaire.azurewebsites.net/api/purpleair/" + map.Region.NearLeft.Latitude +
                    "/" + map.Region.NearLeft.Longitude + "/" + map.Region.FarRight.Latitude + "/" + map.Region.FarRight.Longitude);

                JArray json = JArray.Parse(response);

                map.Pins.Clear();
                foreach (JObject sensor in json)
                {
                    if (!sensor.ContainsKey("ParentID"))
                    {
                        double lat = sensor.Value<double>("Lat");
                        double lon = sensor.Value<double>("Lon");

                        Pin pin = new Pin() { Position = new Position(lat, lon), Label = sensor.Value<string>("Label") + "PM 2.5: " + sensor.Value<string>("PM2_5Value") + " ug/m^3" };
                        map.Pins.Add(pin);
                    }
                }
            };
            
            Content = map;
        }
    }
}
