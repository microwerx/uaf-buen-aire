using System;

using UIKit;
using Google.Maps;
using CoreGraphics;

namespace buenaire.iOS
{
    public partial class MyViewController : UIViewController
    {
        MapView mapView;

        public MyViewController() : base("MyViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var camera = CameraPosition.FromCamera(latitude: 64.82, longitude: -147.71, zoom: 6);
            mapView = MapView.FromCamera(CGRect.Empty, camera);
            mapView.MyLocationEnabled = true;
            View = mapView;
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

