using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;

namespace IronMan_mobile2
{
    public class Scripts : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ScriptsAdapter adapter = new ScriptsAdapter(Context, Editor.scriptsList);
            View view = inflater.Inflate(Resource.Layout.scriptviewer, container, false); 
            ListView lst = view.FindViewById<ListView>(Resource.Id.scriptviewer);
            Button run = view.FindViewById<Button>(Resource.Id.startseq);
            lst.Adapter = adapter;

            run.Click += delegate
            {
                Fragment running = new Running();
                FragmentTransaction ft = FragmentManager.BeginTransaction();
                ft.Replace(Resource.Id.container, running);
                ft.AddToBackStack(null);
                ft.Commit();
                MainActivity.HideTabBar();
            };
            return view;
        }
    }
}