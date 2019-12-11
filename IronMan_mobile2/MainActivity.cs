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
    public class MainActivity : AppCompatActivity
    {
        private TabAdapter adapter;
        private static TabLayout tabLayout;
        private ViewPager viewPager;
        public static List<string> scriptList = new List<string>();
        
        protected override void AttachBaseContext(Context context)
        {
            base.AttachBaseContext(CalligraphyContextWrapper.Wrap(context));
        }
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            tabLayout = FindViewById<TabLayout>(Resource.Id.tabs);
            tabLayout.SetBackgroundResource(Resource.Drawable.tab_background);

            adapter = new TabAdapter(SupportFragmentManager, this);
            adapter.AddFragment(new Editor(), "EDITOR");
            adapter.AddFragment(new Scripts(), "SCRIPTS");
            viewPager.Adapter = adapter;
            tabLayout.SetupWithViewPager(viewPager);
            HightLightCurrentTab(0); 
            viewPager.PageSelected += (sender, e) => HightLightCurrentTab(e.Position);
            viewPager.PageScrollStateChanged += (sender, e) => { };
        }

        public static void HideTabBar(int id)
        {
            if(id == 0)
                tabLayout.Visibility = ViewStates.Gone;
            else if (id == 1)
                tabLayout.Visibility = ViewStates.Visible;
        }

        private void HightLightCurrentTab(int position)
        {
            TabLayout.Tab tab;
            for (int i = 0; i < tabLayout.TabCount; i++)
            {
                tab = tabLayout.GetTabAt(i);
                if (tab != null)
                {
                    tab.SetCustomView(null);
                    tab.SetCustomView(adapter.GetTabView(i));
                }
            }
            
            tab = tabLayout.GetTabAt(position);
            if (tab != null)
            {
                tab.SetCustomView(null);
                tab.SetCustomView(adapter.SelectedTabView(position));
            }
        }
    }
}