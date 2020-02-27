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
    public partial class PurpleAirPage : ContentPage
    {
        public PurpleAirPage()
        {
            InitializeComponent();

            Map map = new Map(MapSpan.FromCenterAndRadius(new Position(64.83, -147.71), Distance.FromMiles(10)));

            Polygon polygon1 = new Polygon()
            {
                StrokeWidth = 25,
                StrokeColor = Color.FromHex("#1BA1E2"),
                FillColor = Color.FromHex("#881BA1E2"),
                Geopath =
                        {
                            new Position(64.80, -147.71),
                            new Position(64.78, -147.69),
                            new Position(64.76, -147.71),
                            new Position(64.78, -147.73)
                        }
            };

            Polygon polygon2 = new Polygon()
            {
                StrokeWidth = 25,
                StrokeColor = Color.DarkRed,
                FillColor = Color.Red,
                Geopath =
                        {
                            new Position(64.90, -147.71),
                            new Position(64.88, -147.69),
                            new Position(64.86, -147.71),
                            new Position(64.88, -147.73)
                        }
            };

            Polygon polygon3 = new Polygon()
            {
                StrokeWidth = 25,
                StrokeColor = Color.DarkOrange,
                FillColor = Color.Orange,
                Geopath =
                        {
                            new Position(64.85, -147.76),
                            new Position(64.83, -147.74),
                            new Position(64.81, -147.76),
                            new Position(64.83, -147.78)
                        }
            };

            Polygon polygon4 = new Polygon()
            {
                StrokeWidth = 25,
                StrokeColor = Color.Yellow,
                FillColor = Color.LightYellow,
                Geopath =
                        {
                            new Position(64.85, -147.66),
                            new Position(64.83, -147.64),
                            new Position(64.81, -147.66),
                            new Position(64.83, -147.68)
                        }
            };

            // add the polygon to the map's MapElements collection
            map.MapElements.Add(polygon1);
            map.MapElements.Add(polygon2);
            map.MapElements.Add(polygon3);
            map.MapElements.Add(polygon4);

            Content = map;
        }
    }
}