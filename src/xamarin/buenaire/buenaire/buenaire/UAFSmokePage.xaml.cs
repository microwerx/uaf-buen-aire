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
    public partial class UAFSmokePage : ContentPage
    {
        public UAFSmokePage()
        {
            InitializeComponent();

            Map map = new Map();

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(64.83d, -147.71d), Distance.FromMeters(10000)), false);

            Pin pin1 = new Pin()
            {
                Position = new Position(64.83, -147.73),
                Label = "Fairbanks!",
                Icon = BitmapDescriptorFactory.DefaultMarker(Color.FromRgba(0, 177, 106, 1))
            };
            map.Pins.Add(pin1);

            Pin pin2 = new Pin()
            {
                Position = new Position(64.83, -147.72),
                Label = "Fairbanks!",
                Icon = BitmapDescriptorFactory.DefaultMarker(Color.FromRgba(240, 255, 0, 1))
            };
            map.Pins.Add(pin2);

            Pin pin3 = new Pin()
            {
                Position = new Position(64.83, -147.71),
                Label = "Fairbanks!",
                Icon = BitmapDescriptorFactory.DefaultMarker(Color.FromRgba(248, 148, 6, 1))
            };
            map.Pins.Add(pin3);

            Pin pin4 = new Pin()
            {
                Position = new Position(64.83, -147.70),
                Label = "Fairbanks!",
                Icon = BitmapDescriptorFactory.DefaultMarker(Color.FromRgba(207, 0, 15, 1))
            };
            map.Pins.Add(pin4);

            Pin pin5 = new Pin()
            {
                Position = new Position(64.83, -147.69),
                Label = "Fairbanks!",
                Icon = BitmapDescriptorFactory.DefaultMarker(Color.Purple)
            };
            map.Pins.Add(pin5);

            Pin pin6 = new Pin()
            {
                Position = new Position(64.83, -147.68),
                Label = "Fairbanks!",
                Icon = BitmapDescriptorFactory.DefaultMarker(Color.White)
            };
            map.Pins.Add(pin6);

            Content = map;
        }
    }
}