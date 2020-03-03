using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.GoogleMaps;

namespace buenaire
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurpleAirPage : ContentPage
    {
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
                Label = "15"
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
                Label = "55"
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

            Content = map;
        }
    }
}