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
        public bool ScriptIsRunning = true;
        private static TextView _pleasewait;
        private Button back;
        private static ProgressBar _oneScriptIndicator;
        private static string _result;
        private static Context _context;
        private static TextView _runningText;
        private static TextView _howManyScriptsText;

        public static string Result
        {
            get => _result;
            set
            {
                _result = null;
                Scripts.ScriptCompletedCounter++;
                var locker = new object();
                lock (locker)
                {
                    if (value != null)
                    {
                        _result = value;
                        Toast.MakeText(_context, _result, ToastLength.Short).Show();
                        _howManyScriptsText.Text = $"{Scripts.ScriptCompletedCounter}/{ScriptsAdapter.ScriptSelectedCounter}";
                        if (Scripts.ScriptCompletedCounter == ScriptsAdapter.ScriptSelectedCounter)
                        {
                            _oneScriptIndicator.Visibility = ViewStates.Gone;
                            _pleasewait.Visibility = ViewStates.Gone;
                            _runningText.Text = "DONE";
                            Toast.MakeText(_context, "It's all", ToastLength.Short).Show();
                        }
                    }
                }
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.running, container, false);
            _pleasewait = view.FindViewById<TextView>(Resource.Id.pleasewait);
            back = view.FindViewById<Button>(Resource.Id.back);
            _oneScriptIndicator = view.FindViewById<ProgressBar>(Resource.Id.oneScriptIndicator);
            _runningText = view.FindViewById<TextView>(Resource.Id.runningText);
            _howManyScriptsText = view.FindViewById<TextView>(Resource.Id.howManyScriptsText);
            _context = Context;

            _howManyScriptsText.Text = $"0/{ScriptsAdapter.ScriptSelectedCounter}";
            

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