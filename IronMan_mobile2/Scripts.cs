using System;
using System.Collections.Generic;
using System.Linq;
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
    public sealed class Scripts : Fragment
    {
        private static RelativeLayout runBar;
        private static RecyclerView lst;
        private RecyclerView.LayoutManager lstLayoutManager;
        private ScriptsAdapter adapter;
        private static Button run;
        private static int lstMaxHeight;
        private static int lstMinHeight;
        public static int scriptCompletedCounter;
        
        public static readonly List<string> ScriptList = new List<string>();
        
        public static List<string> SelectedScripts = new List<string>();
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.scriptviewer, container, false);
            lst = view.FindViewById<RecyclerView>(Resource.Id.scriptviewer);
            runBar = view.FindViewById<RelativeLayout>(Resource.Id.runBar); 
            run = view.FindViewById<Button>(Resource.Id.startseq);
            

            //hide run bar
            SetRunBarVisibility(VisibilityFlags.Invisible);
            
            adapter = new ScriptsAdapter(Context, ScriptList);
            lst.SetAdapter(adapter);
            
            //set the layout manager for list
            lstLayoutManager = new LinearLayoutManager(Context);
            lst.SetLayoutManager(lstLayoutManager);

            lstMaxHeight = lst.LayoutParameters.Height;
            lstMinHeight = lst.LayoutParameters.Height - runBar.LayoutParameters.Height;
            //show running window after Run click
            run.Click += delegate
            {
                if (!SelectedScripts.Any())
                {
                    Toast.MakeText(Context, "Please, choose script", ToastLength.Short).Show();
                }
                else
                {
                    scriptCompletedCounter = 0;
                    RunScriptConnection.StartConnectionAsync(MainActivity.Ip);
                    
                    //replace Scripts with Running
                    Fragment running = new Running();
                    FragmentManager.BeginTransaction()
                        .Replace(Resource.Id.container, running)
                        .AddToBackStack(null)
                        .Commit();

                    //hide TabLayout when running window is showed
                    MainActivity.SetTabBarVisibility(VisibilityFlags.Invisible);
                }
            };
            return view;
        }

        
        public static void AddToScriptList(string script)
        {
            if (!ScriptList.Contains(script) && !string.IsNullOrEmpty(script))
            {
                ScriptList.Add(script);
            }
        }
        

        //animated showing Run Bar
        public static void SetRunBarVisibility(VisibilityFlags flag)
        {
            switch (flag)
            {
                case VisibilityFlags.Invisible:
                    runBar.Animate().TranslationY(250);
                    AnimatedListResizing(lstMinHeight, lstMaxHeight);
                    break;
                case VisibilityFlags.Visible:
                    runBar.Animate().TranslationY(0);
                    AnimatedListResizing(lstMaxHeight, lstMinHeight);
                    break;
            }
        }
        
        private static void AnimatedListResizing(int fromHeight, int toHeight)
        {
            if (lst.LayoutParameters.Height != fromHeight) return;
            Animation animClick = new ResizeListAnimation(lst, fromHeight, toHeight);
            animClick.Interpolator = new AccelerateInterpolator();
            animClick.Duration = 300;
            lst.Animation = animClick;
            lst.StartAnimation(animClick);
        }
    }
}