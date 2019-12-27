using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
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
        private static ScriptsAdapter adapter;
        private static Button run;
        private static int lstMaxHeight;
        private static int lstMinHeight;
        public static int scriptCompletedCounter;
        public static Context context;
        private ItemTouchHelper itemTouchHelper = new ItemTouchHelper(new SwipeController(0, ItemTouchHelper.Left));
        
        public static readonly List<ScriptItem> ScriptList = new List<ScriptItem>();
        
        public static List<ScriptItem> SelectedScripts = new List<ScriptItem>();
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.scriptviewer, container, false);
            lst = view.FindViewById<RecyclerView>(Resource.Id.scriptviewer);
            runBar = view.FindViewById<RelativeLayout>(Resource.Id.runBar); 
            run = view.FindViewById<Button>(Resource.Id.startseq);
            context = Context;




            //hide run bar
            runBar.Animate().TranslationY(250);
            //SetRunBarVisibility(VisibilityFlags.Invisible);

            adapter = new ScriptsAdapter(Context, ScriptList) {HasStableIds = true};
            lst.SetAdapter(adapter);
            
            //set the layout manager for list
            lstLayoutManager = new LinearLayoutManager(Context);
            lst.SetLayoutManager(lstLayoutManager);
            
            itemTouchHelper.AttachToRecyclerView(lst);

            lstMaxHeight = lst.LayoutParameters.Height;
            lstMinHeight = lst.LayoutParameters.Height - runBar.LayoutParameters.Height - run.LayoutParameters.Height;
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

        
        public static void AddToScriptList(ScriptItem script)
        {
            if (!ScriptList.Contains(script) && !string.IsNullOrEmpty(script.ScriptName))
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
                Animation animClick = new ResizeListAnimation(lst, fromHeight, toHeight);
                animClick.Interpolator = new AccelerateInterpolator();
                animClick.Duration = 300;
                lst.Animation = animClick;
                lst.StartAnimation(animClick);
        }

        private static int removedItemPosition;
        private static ScriptItem removedItem;

        public static void RemoveScript(int position)
        {
            removedItemPosition = position;
            removedItem = ScriptList[position];
            ScriptList.RemoveAt(position);
            adapter.NotifyDataSetChanged();
        }
        
        public static void InsertScript()
        {
            ScriptList.Insert(removedItemPosition, removedItem);
            adapter.NotifyDataSetChanged();
        }
        
    }
}