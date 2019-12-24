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
    public sealed class ScriptsViewHolder : RecyclerView.ViewHolder
    {
        public TextView ScriptName { get; }
        public ImageButton BtnPlus { get; }
        public int btnCounter = 0;
 
        public ScriptsViewHolder(View itemView) : base(itemView)
        {
            ScriptName = itemView.FindViewById<TextView>(Resource.Id.textView5);
            BtnPlus = itemView.FindViewById<ImageButton>(Resource.Id.plus_btn);
        }
    }
    public sealed class ScriptsAdapter : RecyclerView.Adapter
    {
            public static int scriptSelectedCounter;
            private readonly List<string> list; //list of scripts
            private readonly Context context; 

            public ScriptsAdapter(Context context, List<string> list)
            {
                scriptSelectedCounter = 0;
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

                if (holder is ScriptsViewHolder vh)
                {
                    vh.ScriptName.Text = list[position];

                    //event unsubscription to don't repeat the subscription
                    vh.BtnPlus.Click -= BtnPlusOnClick;

                    vh.BtnPlus.Click += BtnPlusOnClick;

                    vh.ItemView.Click -= ScriptOnClick;

                    vh.ItemView.Click += ScriptOnClick;

                    //when click on "+" button in script item
                    void BtnPlusOnClick(object sender, EventArgs args)
                    {
                        if (vh.btnCounter % 2 == 0)
                        {
                            scriptSelectedCounter++;
                            vh.btnCounter++;
                            Scripts.SetRunBarVisibility(VisibilityFlags.Visible);
                            
                            //change "+" to check mark
                            vh.BtnPlus.SetImageResource(Resource.Drawable.chech_mark);
                            //Scripts.SelectedScripts += list[position] + "*";
                            
                            Scripts.SelectedScripts.Add(list[position]);
                        }
                        else
                        {
                            vh.btnCounter--;
                            scriptSelectedCounter--;
                            //change check mark to "+"
                            vh.BtnPlus.SetImageResource(Resource.Drawable.plus_btn);
                           /* Scripts.SelectedScripts =
                                Scripts.SelectedScripts.Replace(list[position] + "*", "");*/

                           Scripts.SelectedScripts.Remove(list[position]);
                        } 
                        
                        if(scriptSelectedCounter == 0) Scripts.SetRunBarVisibility(VisibilityFlags.Invisible);
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