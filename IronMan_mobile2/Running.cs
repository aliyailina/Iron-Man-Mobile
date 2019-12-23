using System;
using Android.Arch.Lifecycle;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;

namespace IronMan_mobile2
{
    public class Running : Fragment
    {
        private bool backIsClicked;
        public bool scriptIsRunning = true;
        private static TextView pleasewait;
        private Button back;
        private static ProgressBar oneScriptIndicator;
        private static string result;
        private static Context context;
        private static TextView runningText;

        public static string Result
        {
            set
            {
                Scripts.scriptCompletedCounter++;
                var locker = new object();
                lock (locker)
                {
                    if (value != null)
                    {
                        result = value;
                        Toast.MakeText(context, result, ToastLength.Short).Show();
                        if (Scripts.scriptCompletedCounter == ScriptsAdapter.scriptSelectedCounter)
                        {
                            oneScriptIndicator.Visibility = ViewStates.Gone;
                            pleasewait.Visibility = ViewStates.Gone;
                            runningText.Text = "DONE";
                            Toast.MakeText(context, "It's all", ToastLength.Short).Show();
                        }
                    }
                }
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.running, container, false);
            pleasewait = view.FindViewById<TextView>(Resource.Id.pleasewait);
            back = view.FindViewById<Button>(Resource.Id.back);
            oneScriptIndicator = view.FindViewById<ProgressBar>(Resource.Id.oneScriptIndicator);
            runningText = view.FindViewById<TextView>(Resource.Id.runningText);
            context = Context;

            //when "BACK" is clicked
            back.Click += delegate
            {
                FragmentManager.PopBackStackImmediate(); //replace the fragment with previous (Scripts)
                MainActivity.SetTabBarVisibility(VisibilityFlags.Visible); //show tab bar
            };
            
            return view;
        }
    }
    
}