using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;

namespace buenaire
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DefaultMapPage : ContentPage
    {
        public DefaultMapPage()
        {
            InitializeComponent();

            Map map = new Map(MapSpan.FromCenterAndRadius(new Position(64.83, -147.71), Distance.FromMiles(10)));

            Pin pin = new Pin()
            {
                Position = new Position(64.83, -147.71),
                Label = "Fairbanks!"
            };

            map.Pins.Add(pin);

            Content = map;
        }
    }
}