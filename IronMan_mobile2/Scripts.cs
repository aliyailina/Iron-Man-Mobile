using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace IronMan_mobile2
{
    public class Scripts : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ScriptsAdapter adapter = new ScriptsAdapter(Context, Editor.scriptsList);
            View view = inflater.Inflate(Resource.Layout.scriptviewer, null);
            ListView lst = view.FindViewById<ListView>(Resource.Id.scriptviewer);
            lst.Adapter = adapter;
            return view;
        }
    }
}