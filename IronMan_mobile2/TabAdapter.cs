using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Color = Android.Graphics.Color;

namespace IronMan_mobile2
{
    public class TabAdapter : FragmentPagerAdapter
    {
        private readonly List<Fragment> fragments = new List<Fragment>();
        private readonly List<string> titles = new List<string>();
        private readonly Context context;

        public TabAdapter(FragmentManager fm, Context context) : base(fm)
        {
            this.context = context;
        }

        public override int Count => fragments.Count;

        public override Fragment GetItem(int position)
        {
            return fragments[position];
        }

        public void AddFragment(Fragment fragment, string title)
        {
            fragments.Add(fragment);
            titles.Add(title);
        }

        public View GetTabView(int position)
        {
            View view = LayoutInflater.From(context).Inflate(Resource.Layout.custom_tab, null);
            TextView textView = view.FindViewById<TextView>(Resource.Id.tabTextView);
            textView.Text = titles[position];
            return view;
        }

        public View SelectedTabView(int position)
        {
            View view = LayoutInflater.From(context).Inflate(Resource.Layout.custom_tab, null);
            TextView textView = view.FindViewById<TextView>(Resource.Id.tabTextView);
            textView.Text = titles[position];
            return view;
        }
    }
}