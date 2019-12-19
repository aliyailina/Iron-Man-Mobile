using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Calligraphy;

namespace IronMan_mobile2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", Icon = "@drawable/logo")]
    public sealed class MainActivity : AppCompatActivity
    {
        private TabAdapter adapter;
        private static TabLayout _tabLayout;
        private ViewPager viewPager;
        public static readonly List<string> ScriptList = new List<string>();
        public static string SelectedScripts = null;
        public static string Ip = null;
        
        //add fonts support
        protected override void AttachBaseContext(Context context)
        {
            base.AttachBaseContext(CalligraphyContextWrapper.Wrap(context));
        }
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
            
            //hide Status Bar
            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            
            _tabLayout = FindViewById<TabLayout>(Resource.Id.tabs);
            _tabLayout.SetBackgroundResource(Resource.Drawable.tab_background);

            adapter = new TabAdapter(SupportFragmentManager, this);
            adapter.AddFragment(new Editor(), "EDITOR");
            adapter.AddFragment(new Scripts(), "SCRIPTS");
            viewPager.Adapter = adapter;
            _tabLayout.SetupWithViewPager(viewPager);
            
            HighLightCurrentTab(0); 
            viewPager.PageSelected += (sender, e) => HighLightCurrentTab(e.Position);
            viewPager.PageScrollStateChanged += (sender, e) => { };
        }

        public static void HideTabBar(int id)
        {
            switch (id)
            {
                case 0:
                    _tabLayout.Visibility = ViewStates.Gone;
                    break;
                case 1:
                    _tabLayout.Visibility = ViewStates.Visible;
                    break;
            }
        }

        private void HighLightCurrentTab(int position)
        {
            TabLayout.Tab tab;
            for (var i = 0; i < _tabLayout.TabCount; i++)
            {
                tab = _tabLayout.GetTabAt(i);
                if (tab != null)
                {
                    tab.SetCustomView(null);
                    tab.SetCustomView(adapter.GetTabView(i));
                }
            }
            
            tab = _tabLayout.GetTabAt(position);
            if (tab != null)
            {
                tab.SetCustomView(null);
                tab.SetCustomView(adapter.SelectedTabView(position));
            }
        }
    }
}