using namespace buenaire
{
  using Xamarin.Forms.GoogleMaps;
  using Xamarin.Forms.GoogleMaps.Android.Factories;
  using AndroidBitmapDescriptor = Android.Gms.Maps.Model.BitmapDescriptor;
  using AndroidBitmapDescriptorFactory = Android.Gms.Maps.Model.BitmapDescriptorFactory;

  public sealed class AccessNativeBitmapConfig : IBitmapDescriptorFactory
  {
      public AndroidBitmapDescriptor ToNative(BitmapDescriptor descriptor)
      {
          int resId = 0;
          switch (descriptor.Id)
          {
              case "Fifty":
                  resId = Resource.Drawable.Fifty;
                  break;
              case "purpleair":
                  resId = Resource.Drawable.purpleair;
                  break;
          }

          return AndroidBitmapDescriptorFactory.FromResource(resId);
      }
  }
}
