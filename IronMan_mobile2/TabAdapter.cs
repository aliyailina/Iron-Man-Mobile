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
        List<Fragment> fragments = new List<Fragment>();
        private List<string> titels = new List<string>();
        private Context context;

        public TabAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

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
            titels.Add(title);
        }

        public View GetTabView(int position)
        {
            View view = LayoutInflater.From(context).Inflate(Resource.Layout.custom_tab, null);
            TextView textView = view.FindViewById<TextView>(Resource.Id.tabTextView);
            textView.Text = titels[position];
            return view;
        }

        public View SelectedTabView(int position)
        {
            View view = LayoutInflater.From(context).Inflate(Resource.Layout.custom_tab, null);
            TextView textView = view.FindViewById<TextView>(Resource.Id.tabTextView);
            textView.Text = titels[position];
            textView.SetTextColor(Color.Black);
            return view;
        }

    }
}