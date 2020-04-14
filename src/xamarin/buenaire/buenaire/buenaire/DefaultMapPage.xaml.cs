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

            CustomMap customMap = new CustomMap();

            CustomPin pin1 = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(64.83d, -147.71d),
                Label = "A Freaking Pin",
                Name = "Xamarin",
                Address = "There will be things said here",
                IconType = 1,
                Url = "https://www.purpleair.com/map?opt=1/mAQI/a10/cC0#1/25/-30"
            };
            CustomPin pin2 = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(64.83d, -147.69d),
                Label = "A Freaking Pin",
                Address = "Maybe",
                Name = "Xamarin",
                IconType = 2,
                Url = "https://www.purpleair.com/map?opt=1/mAQI/a10/cC0#1/25/-30"
            };
            CustomPin pin3 = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(64.83d, -147.73d),
                Label = "A Freaking Pin",
                Address = "Maybe",
                Name = "Xamarin",
                IconType = 0,
                Url = "https://www.purpleair.com/map?opt=1/mAQI/a10/cC0#1/25/-30"
            };

            CustomPin pin4 = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(64.82d, -147.71d),
                Label = "A Freaking Pin",
                Name = "Xamarin",
                Address = "There will be things said here",
                IconType = 4,
                Url = "https://www.purpleair.com/map?opt=1/mAQI/a10/cC0#1/25/-30"
            };
            CustomPin pin5 = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(64.82d, -147.69d),
                Label = "A Freaking Pin",
                Address = "Maybe",
                Name = "Xamarin",
                IconType = 5,
                Url = "https://www.purpleair.com/map?opt=1/mAQI/a10/cC0#1/25/-30"
            };
            CustomPin pin6 = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(64.82d, -147.73d),
                Label = "A Freaking Pin",
                Address = "Maybe",
                Name = "Xamarin",
                IconType = 3,
                Url = "https://www.purpleair.com/map?opt=1/mAQI/a10/cC0#1/25/-30"
            };

            CustomPin pin7 = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(64.81d, -147.73d),
                Label = "A Freaking Pin",
                Address = "Maybe",
                Name = "Xamarin",
                IconType = 6,
                Url = "https://www.purpleair.com/map?opt=1/mAQI/a10/cC0#1/25/-30"
            };
            customMap.CustomPins = new List<CustomPin> { pin1, pin2, pin3, pin4, pin5, pin6, pin7 };
            customMap.Pins.Add(pin1);
            customMap.Pins.Add(pin2);
            customMap.Pins.Add(pin3);
            customMap.Pins.Add(pin4);
            customMap.Pins.Add(pin5);
            customMap.Pins.Add(pin6);
            customMap.Pins.Add(pin7);
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(64.82d, -147.71d), Distance.FromMiles(1.0)));

            Content = customMap;
        }
    }
}