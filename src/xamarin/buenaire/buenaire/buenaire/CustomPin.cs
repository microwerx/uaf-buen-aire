using Xamarin.Forms.Maps;

namespace buenaire
{
    public class CustomPin : Pin
    {
        public int IconType { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        //void SetTheIcon(Marker marker)
        //{
        //    if (customPin.IconType == 0)
        //    {
        //        marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.fifty));
        //    }
        //    else
        //    {
        //        marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin));
        //    }
        //}
    }
}
