using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;

namespace IronMan_mobile2
{
    public class Scripts : Fragment
    {
        private static RelativeLayout runBar;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ScriptsAdapter adapter = new ScriptsAdapter(Context, Editor.scriptsList);
            View view = inflater.Inflate(Resource.Layout.scriptviewer, container, false);
            runBar = view.FindViewById<RelativeLayout>(Resource.Id.runBar);
            runBar.TranslationY = 200;
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
                MainActivity.HideTabBar(0);
            };
            return view;
        }

        public static void ShowRunBar(Context context)
        {
            runBar.Animate().TranslationY(0);

        }
    }
}