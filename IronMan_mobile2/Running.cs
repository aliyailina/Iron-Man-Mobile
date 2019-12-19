using System;
using System.Net.Mime;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Transitions;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;

namespace IronMan_mobile2
{
    public class Running : Fragment
    {
        private bool backIsClicked;
        public bool scriptIsRunning = true;
        private TextView pleasewait;
        private Button back;
        private static ProgressBar oneScriptIndicator;
        public static string result = null;
        private static Context context;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.running, container, false);
            pleasewait = view.FindViewById<TextView>(Resource.Id.pleasewait);
            back = view.FindViewById<Button>(Resource.Id.back);
            oneScriptIndicator = view.FindViewById<ProgressBar>(Resource.Id.oneScriptIndicator);
            context = Context;
            
            //TODO: right fragment working
            
            RunScriptConnection.StartConnection(MainActivity.Ip);
            
            //when "BACK" is clicked
            back.Click += delegate
            {
                FragmentManager.PopBackStackImmediate(); //replace the fragment with previous (Scripts)
                MainActivity.HideTabBar(1); //show tab bar
            };
            
            return view;
        }

        public static void ScriptFinishedResult()
        {
            oneScriptIndicator.Visibility = ViewStates.Gone;
            Toast.MakeText(context,
                result == "Successful" ? "Script completed successfully!" : "Script completed with error!",
                ToastLength.Short).Show();
        }
    }
}