using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Google.Maps;
using Xamarin.Forms.GoogleMaps.iOS;

namespace buenaire.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private static string MapsApiKey = "AIzaSyCLQOnTc977Zc-LdmLtWE4ToIraVY1pQRk";

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            MapServices.ProvideApiKey(MapsApiKey);
            global::Xamarin.Forms.Forms.Init();
            // Override default ImageFactory by your implementation. 
            var platformConfig = new PlatformConfig
            {
                ImageFactory = new CachingImageFactory()
            };

            Xamarin.FormsGoogleMaps.Init(MapsApiKey, platformConfig);
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
