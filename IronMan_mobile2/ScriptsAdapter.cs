using System.Collections.Generic;
using System.Globalization;
using System.Net.Mime;
using Android.App;
using Android.Content;
using Android.Media.Session;
using Android.Provider;
using Android.Renderscripts;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace IronMan_mobile2
{
    public class ScriptsViewHolder : RecyclerView.ViewHolder
    {
        public TextView scriptName { get; private set; }
        public ImageButton btnPlus { get; private set; }
 
        public ScriptsViewHolder(View itemView) : base(itemView)
        {
            scriptName = itemView.FindViewById<TextView>(Resource.Id.textView5);
            btnPlus = itemView.FindViewById<ImageButton>(Resource.Id.plus_btn);
        }
    }
    public class ScriptsAdapter : RecyclerView.Adapter
    {
            private List<string> list;
            private Context context;

            public ScriptsAdapter(Context context, List<string> list)
            {
                this.list = list;
                this.context = context;
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.script, parent, false);
                
                ScriptsViewHolder vh = new ScriptsViewHolder(itemView);
                return vh;
            }

            public override int ItemCount => list.Count;

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                ScriptsViewHolder vh = holder as ScriptsViewHolder;
                
                vh.scriptName.Text = list[position];
                ((ScriptsViewHolder) holder).btnPlus.Click += delegate
                {
                    Scripts.ShowRunBar();
                    ((ScriptsViewHolder) holder).btnPlus.SetImageResource(Resource.Id.checkbox);
                };
                ((ScriptsViewHolder) holder).ItemView.Click += (sender, e) =>
                {
                    LayoutInflater layoutInflater = LayoutInflater.From(context);
                    View v = layoutInflater.Inflate(Resource.Layout.file_info_dialog, null);
                    Button okayBtn = v.FindViewById<Button>(Resource.Id.ok_btn);
                    //view.SetBackgroundResource(Resource.Drawable.ip_background);
                    AlertDialog.Builder builder = new AlertDialog.Builder(context, Resource.Style.AlertDialogTheme);
                    builder.SetView(v);
                    var dialog = builder.Create();
                    dialog.Show();
                    dialog.Window.SetBackgroundDrawableResource(Resource.Drawable.script_info_background);
                    okayBtn.Click += delegate { dialog.Hide(); };
                };
                
            }
            

        /*  public override View GetView(int position, View convertView, ViewGroup parent)
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
            }*/

        private void ShowScriptInfo(int position)
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
        }
    }
}