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
              case "0_50":
                  resId = Resource.Drawable.type2;
                  break;
              case "purpleair":
                  resId = Resource.Drawable.type1;
                  break;
          }

          return AndroidBitmapDescriptorFactory.FromResource(resId);
      }
  }
}
