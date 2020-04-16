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
    public partial class DefaultMapPage : ContentPage
    {
        public DefaultMapPage()
        {
            InitializeComponent();

            Map map = new Map();

            Pin pin1 = new Pin
            {
                Position = new Position(64.83d, -147.71d),
                Label = "A Freaking Pin",
                Address = "There will be things said here",
                Icon = BitmapDescriptorFactory.FromBundle("Fifty.png")
            };
            Pin pin2 = new Pin
            {
                Type = PinType.Place,
                Position = new Position(64.83d, -147.69d),
                Label = "A Freaking Pin",
                Address = "Maybe",
                Icon = BitmapDescriptorFactory.FromBundle("OneHundred.png")
            };
            Pin pin3 = new Pin
            {
                Type = PinType.Place,
                Position = new Position(64.83d, -147.73d),
                Label = "A Freaking Pin",
                Address = "Maybe",
                Icon = BitmapDescriptorFactory.FromBundle("OneFifty.png")
            };

            Pin pin4 = new Pin
            {
                Type = PinType.Place,
                Position = new Position(64.82d, -147.71d),
                Label = "A Freaking Pin",
                Icon = BitmapDescriptorFactory.FromBundle("TwoHundred.png")
            };
            Pin pin5 = new Pin
            {
                Type = PinType.Place,
                Position = new Position(64.82d, -147.69d),
                Label = "A Freaking Pin",
                Address = "Maybe",
                Icon = BitmapDescriptorFactory.FromBundle("ThreeHundred.png")
            };
            Pin pin6 = new Pin
            {
                Type = PinType.Place,
                Position = new Position(64.82d, -147.73d),
                Label = "A Freaking Pin",
                Address = "Maybe",
                Icon = BitmapDescriptorFactory.FromBundle("FiveHundred.png")
            };

            Pin pin7 = new Pin
            {
                Type = PinType.Place,
                Position = new Position(64.81d, -147.73d),
                Label = "A Freaking Pin",
                Address = "Maybe",
                Icon = BitmapDescriptorFactory.FromBundle("empty.png")
            };
            List<Pin> pins = new List<Pin> { pin1, pin2, pin3, pin4, pin5, pin6, pin7 };
            foreach (var pin in pins)
            {
                map.Pins.Add(pin);
            }

            TileLayer tileLayer = TileLayer.FromTileUri((x, y, zoom) => new Uri($"https://uafbuenaire.azurewebsites.net/api/machinelearning/{zoom}/{x}/{y}"));

            map.TileLayers.Add(tileLayer);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(64.82d, -147.71d), Distance.FromMiles(1.0)));

            Content = map;
        }
    }
}