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
    public sealed class Scripts : Fragment
    {
        private static RelativeLayout runBar;
        private static RecyclerView lst;
        private RecyclerView.LayoutManager lstLayoutManager;
        private ScriptsAdapter adapter;
        private static Button run;
        private static int lstMaxHeight;
        private static int lstMinHeight;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.scriptviewer, container, false);
            lst = view.FindViewById<RecyclerView>(Resource.Id.scriptviewer);
            runBar = view.FindViewById<RelativeLayout>(Resource.Id.runBar); 
            run = view.FindViewById<Button>(Resource.Id.startseq);
            

            //hide run bar
            ShowRunBar(0);
            
            adapter = new ScriptsAdapter(Context, Editor.scriptsList);
            lst.SetAdapter(adapter);
            
            //set the layout manager for list
            lstLayoutManager = new LinearLayoutManager(Context);
            lst.SetLayoutManager(lstLayoutManager);

            lstMaxHeight = lst.LayoutParameters.Height;
            lstMinHeight = lst.LayoutParameters.Height - runBar.LayoutParameters.Height;
            //show running window after Run click
            run.Click += delegate
            {
                if (String.IsNullOrEmpty(MainActivity.SelectedScripts))
                {
                    Toast.MakeText(Context, "Please, choose script", ToastLength.Short).Show();
                }
                else
                {
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
                if (lst.LayoutParameters.Height == lstMinHeight)
                {
                    Animation animClick = new ResizeListAnimation(lst,
                        lstMinHeight,
                        lstMaxHeight);
                    animClick.Interpolator = new AccelerateInterpolator();
                    animClick.Duration = 300;
                    lst.Animation = animClick;
                    lst.StartAnimation(animClick);
                }

                runBar.Visibility = ViewStates.Gone;
                runBar.Animate().TranslationY(250);
            }
            else if (i == 1)
            {
                runBar.Visibility = ViewStates.Visible;
                runBar.Animate().TranslationY(0);

                if (lst.LayoutParameters.Height == lstMaxHeight)
                {
                    Animation animClick = new ResizeListAnimation(lst, lstMaxHeight,
                        lstMinHeight);
                    animClick.Interpolator = new AccelerateInterpolator();
                    animClick.Duration = 300;
                    lst.Animation = animClick;
                    lst.StartAnimation(animClick);
                }
            }
        }
    }
}