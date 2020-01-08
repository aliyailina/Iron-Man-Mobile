using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;

namespace IronMan_mobile2
{
    public sealed class Scripts : Fragment
    {
        private static RelativeLayout _runBar;
        private static RecyclerView _lst;
        private RecyclerView.LayoutManager lstLayoutManager;
        private static ScriptsAdapter _adapter;
        private static Button _run;
        private static int _lstMaxHeight;
        private static int _lstMinHeight;
        public static int ScriptCompletedCounter;
        public static Context context;
        private ItemTouchHelper itemTouchHelper;

        private static readonly List<ScriptItem> ScriptList = new List<ScriptItem>();
        
        public static readonly List<ScriptItem> SelectedScripts = new List<ScriptItem>();
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            itemTouchHelper = new ItemTouchHelper(new SwipeController(0, ItemTouchHelper.Left));
            View view = inflater.Inflate(Resource.Layout.scriptviewer, container, false);
            _lst = view.FindViewById<RecyclerView>(Resource.Id.scriptviewer);
            _runBar = view.FindViewById<RelativeLayout>(Resource.Id.runBar); 
            _run = view.FindViewById<Button>(Resource.Id.startseq);
            context = Context;




            //hide run bar
            _runBar.Animate().TranslationY(250);
            //SetRunBarVisibility(VisibilityFlags.Invisible);

            _adapter = new ScriptsAdapter(Context, ScriptList) {HasStableIds = true};
            _lst.SetAdapter(_adapter);
            
            //set the layout manager for list
            lstLayoutManager = new LinearLayoutManager(Context);
            _lst.SetLayoutManager(lstLayoutManager);
            
            itemTouchHelper.AttachToRecyclerView(_lst);

            _lstMaxHeight = _lst.LayoutParameters.Height;
            _lstMinHeight = _lst.LayoutParameters.Height - _runBar.LayoutParameters.Height - _run.LayoutParameters.Height;
            //show running window after Run click
            _run.Click += delegate
            {
                if (!SelectedScripts.Any())
                {
                    Toast.MakeText(Context, "Please, choose script", ToastLength.Short).Show();
                }
                else
                {
                    ScriptCompletedCounter = 0;
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
                    _runBar.Animate().TranslationY(250);
                    AnimatedListResizing(_lstMinHeight, _lstMaxHeight);
                    break;
                case VisibilityFlags.Visible:
                    _runBar.Animate().TranslationY(0);
                    AnimatedListResizing(_lstMaxHeight, _lstMinHeight);
                    break;
            }
        }
        
        private static void AnimatedListResizing(int fromHeight, int toHeight)
        {
                Animation animClick = new ResizeListAnimation(_lst, fromHeight, toHeight);
                animClick.Interpolator = new AccelerateInterpolator();
                animClick.Duration = 300;
                _lst.Animation = animClick;
                _lst.StartAnimation(animClick);
        }

        public static void RemoveScript(int position)
        {
            ScriptList.RemoveAt(position);
            _adapter.NotifyDataSetChanged();
            _adapter.NotifyItemChanged(position);
            _adapter.NotifyItemRangeChanged(position, ScriptList.Count);
            _adapter.NotifyItemRemoved(position);
            DeleteScriptConnection.StartConnectionAsync(MainActivity.Ip, ScriptList[position]);
        }

    }
}