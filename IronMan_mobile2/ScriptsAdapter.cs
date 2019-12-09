using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Media.Session;
using Android.Provider;
using Android.Renderscripts;
using Android.Views;
using Android.Widget;

namespace IronMan_mobile2
{
    public class ScriptsAdapter : BaseAdapter<string>
    {
            private List<string> list;
            private Context context;

            public ScriptsAdapter(Context context, List<string> list)
            {
                this.list = list;
                this.context = context;
            }

            public override int Count => list.Count;

            public override string this[int position] => list[position];
            
            public override long GetItemId(int position)
            {
                return position;
            }
            

          public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View view = convertView;
                if (view == null)
                {
                    view = LayoutInflater.From(context).Inflate(Resource.Layout.script, null, false);
                    TextView scriptName = view.FindViewById<TextView>(Resource.Id.textView5);
                    
                    view.Click +=
                        (sender, e) =>
                        {
                            Toast.MakeText(context, "Item choose",
                                ToastLength.Short).Show();
                            LayoutInflater layoutInflater = LayoutInflater.From(context);
                            View v = layoutInflater.Inflate(Resource.Layout.file_info_dialog, null);
                            Button okayBtn = v.FindViewById<Button>(Resource.Id.ok_btn);
                            //view.SetBackgroundResource(Resource.Drawable.ip_background);
                            AlertDialog.Builder builder = new AlertDialog.Builder(context, Resource.Style.AlertDialogTheme);
                            builder.SetView(v);
                            var dialog = builder.Create();
                            dialog.Show();
                            dialog.Window.SetBackgroundDrawableResource(Resource.Drawable.script_info_background);
                            okayBtn.Click += delegate
                            {
                                dialog.Hide();
                            };
                        };
                    ImageButton plusBtn = view.FindViewById<ImageButton>(Resource.Id.plus_btn);
                    plusBtn.Click += delegate
                    {
                        Scripts.ShowRunBar(context);
                        Toast.MakeText(context, "Added", ToastLength.Short).Show();
                    };
                    scriptName.Text = list[position];
                }

                return view;
            }
    }
}