using Android.OS;
using Android.Support.V4.App;
using Android.Views;

namespace IronMan_mobile2
{
    public class Scripts : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.scriptviewer, container, false);
        }
    }
}