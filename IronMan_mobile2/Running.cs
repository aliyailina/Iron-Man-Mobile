﻿using Android.Media;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace IronMan_mobile2
{
    public class Running : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.running, container, false);
            Button back = view.FindViewById<Button>(Resource.Id.back);
            
            //when "BACK" is clicked
            back.Click += delegate
            {
                FragmentManager.PopBackStackImmediate(); //replace the fragment with previous (Scripts)
                MainActivity.HideTabBar(1); //show tab bar
            };
            
            return view;
        }
    }
}