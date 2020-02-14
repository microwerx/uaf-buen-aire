using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace buenaire
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void map_Clicked(object sender, EventArgs e)
        {
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
