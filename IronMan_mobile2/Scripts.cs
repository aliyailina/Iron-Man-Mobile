using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
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
        private static RecyclerView lst;
        private RecyclerView.LayoutManager lstLayoutManager;
        private ScriptsAdapter adapter;
        private Button run;
        private static int runBarHeight;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.scriptviewer, container, false);
            lst = view.FindViewById<RecyclerView>(Resource.Id.scriptviewer);
            runBar = view.FindViewById<RelativeLayout>(Resource.Id.runBar); 
            run = view.FindViewById<Button>(Resource.Id.startseq);
            runBarHeight = runBar.LayoutParameters.Height;
            

            //hide run bar
            ShowRunBar(0);
            
            adapter = new ScriptsAdapter(Context, Editor.scriptsList);
            lst.SetAdapter(adapter);
            
            //set the layout manager for list
            lstLayoutManager = new LinearLayoutManager(Context);
            lst.SetLayoutManager(lstLayoutManager);

            runBar.LayoutParameters.Height = 0;
            runBar.Visibility = ViewStates.Gone;

            //show running window after Run click
            run.Click += delegate
            {

                if (String.IsNullOrEmpty(MainActivity.choosenScripts))
                {
                    Toast.MakeText(Context, "Please, choose script", ToastLength.Short).Show();
                }
                else
                {
                    RunScriptConnection.StartConnectionAsync(MainActivity.IP);
                    //create the FragmentTransaction
                    Fragment running = new Running();
                    FragmentTransaction ft = FragmentManager.BeginTransaction();
                    ft.Replace(Resource.Id.container, running);
                    ft.AddToBackStack(null);
                    ft.Commit();

                    //hide TabLayout when running window is showed
                    MainActivity.HideTabBar(0);
                }
            };
            return view;
        }
        
        //animated showing Run Bar
        public static void ShowRunBar(int i)
        {
            if (i == 0)
            {
                runBar.Animate().TranslationY(250);
            }
            else if (i == 1)
            {
                //TODO: Create right resizing
                runBar.Visibility = ViewStates.Visible;
                runBar.Animate().TranslationY(0);
                
                Animation animClick = new ResizeListAnimation(lst, lst.LayoutParameters.Height, lst.LayoutParameters.Height - runBar.LayoutParameters.Height);
                runBar.LayoutParameters.Height = runBarHeight;
                animClick.Interpolator = new AccelerateInterpolator();
                animClick.Duration = 300;
                lst.Animation = animClick;
                lst.StartAnimation(animClick);
            }
        }
    }
}