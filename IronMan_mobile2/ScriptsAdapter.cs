using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace IronMan_mobile2
{
    
    //ViewHolder for RecyclerView
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
            private List<string> list; //list of scripts
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

            //GetItemId and GetItemViewType overriding need for right ViewHolder work
            public override long GetItemId(int position)
            {
                return position;
            }

            public override int GetItemViewType(int position)
            {
                return position;
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                int i = 0;
                ScriptsViewHolder vh = holder as ScriptsViewHolder;
                
                vh.scriptName.Text = list[position];

                //event unsubscriptions to don't repeat the subscription
                vh.btnPlus.Click -= BtnPlusOnClick;
                
                vh.btnPlus.Click += BtnPlusOnClick;
                
                vh.ItemView.Click -= ScriptOnClick;

                vh.ItemView.Click += ScriptOnClick;
                
                //when click on "+" button in script item
                void BtnPlusOnClick(object sender, EventArgs args)
                {
                    i += 1;
                    Scripts.ShowRunBar();
                    if (i % 2 == 1)
                    {
                        vh.btnPlus.SetImageResource(Resource.Drawable.chech_mark); //change "+" to check mark
                        MainActivity.choosenScripts += list[position] + "*";
                    }
                    else
                    {
                        vh.btnPlus.SetImageResource(Resource.Drawable.plus_btn); //change check mark to "+"
                        MainActivity.choosenScripts = MainActivity.choosenScripts.Replace(list[position] + "*", "");
                    }
                }
                
                //when click on item
                void ScriptOnClick(object sender, EventArgs e)
                {
                    //create the "Script Info" dialog
                    LayoutInflater layoutInflater = LayoutInflater.From(context);
                    View v = layoutInflater.Inflate(Resource.Layout.file_info_dialog, null);
                    Button okayBtn = v.FindViewById<Button>(Resource.Id.ok_btn);
                    //view.SetBackgroundResource(Resource.Drawable.ip_background);
                    AlertDialog.Builder builder = new AlertDialog.Builder(context, Resource.Style.AlertDialogTheme);
                    builder.SetView(v);
                    var dialog = builder.Create();
                    dialog.Show();
                    dialog.Window.SetBackgroundDrawableResource(Resource.Drawable.script_info_background);
                    okayBtn.Click += delegate { dialog.Hide(); }; //if click on "Okay" — close "Script Info" dialog
                }

            }
    }
}