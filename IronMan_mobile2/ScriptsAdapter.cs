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
        private bool isSwipeEnabled = true;

       /* public bool IsSwipeEnabled
        {
            get => isSwipeEnabled;
            set
            {
                if (!value)
                {
                    ScriptBackground.Visibility = ViewStates.Visible;
                    ScriptForeground.Animate().TranslationX(-250);
                    isSwipeEnabled = false;
                    Scripts.ItemTouchHelperAttach(false);
                    BtnPlus.Click += delegate
                    {
                        ScriptBackground.Visibility = ViewStates.Gone;
                        ScriptForeground.TranslationX = 0;
                        isSwipeEnabled = true;
                        Scripts.ItemTouchHelperAttach(true);
                    };
                }
                else
                {
                    ScriptBackground.Visibility = ViewStates.Gone;
                    ScriptForeground.TranslationX = 0;
                    isSwipeEnabled = true;
                    Scripts.ItemTouchHelperAttach(true);
                }
            }
        }*/

        public TextView ScriptName { get; }
        public ImageButton BtnPlus { get; }

        public bool BtnPlusIsClicked;

        public LinearLayout ScriptBackground, ScriptForeground;

        public Button Delete;

        public ScriptsViewHolder(View itemView) : base(itemView)
        {
            ScriptName = itemView.FindViewById<TextView>(Resource.Id.textView5);
            BtnPlus = itemView.FindViewById<ImageButton>(Resource.Id.plus_btn);
            ScriptBackground = itemView.FindViewById<LinearLayout>(Resource.Id.script_background);
            ScriptForeground = itemView.FindViewById<LinearLayout>(Resource.Id.script_foreground);
            Delete = itemView.FindViewById<Button>(Resource.Id.deleteBtn);
            ScriptBackground.Visibility = ViewStates.Gone;
        }
    }

    public sealed class ScriptsAdapter : RecyclerView.Adapter
    {
        public static int ScriptSelectedCounter;
        private readonly List<ScriptItem> list;
        public readonly Context context;
        public ScriptsAdapter(Context context, List<ScriptItem> list)
        {
            ScriptSelectedCounter = 0;
            this.list = list;
            this.context = context;
        }
        

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.script, parent, false);
            var vh = new ScriptsViewHolder(itemView);
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
                vh.ScriptName.Text = list[position].ScriptName;


                if (!vh.Delete.HasOnClickListeners)
                {
                    vh.Delete.Click -= BtnDeleteOnClick;

                    vh.Delete.Click += BtnDeleteOnClick;
                }
            
                void BtnDeleteOnClick(object sender, EventArgs e)
                {
                    vh.ScriptForeground.TranslationX = 0;
                    vh.BtnPlus.SetImageResource(Resource.Drawable.plus_btn);
                    Scripts.ItemTouchHelperAttach(true);
                    DeleteScriptConnection.StartConnectionAsync(MainActivity.Ip, list[position]);
                    list.RemoveAt(position);
                    NotifyDataSetChanged();
                    NotifyItemChanged(position);
                    NotifyItemRangeChanged(position, list.Count);
                    NotifyItemRemoved(position);
                }

                //event unsubscription to don't repeat the subscription

                if (!vh.BtnPlus.HasOnClickListeners)
                {
                    vh.BtnPlus.Click -= BtnPlusOnClick;

                    vh.BtnPlus.Click += BtnPlusOnClick;
                }

                if (!vh.ScriptName.HasOnClickListeners)
                {
                    vh.ScriptName.Click -= ScriptOnClick;

                    vh.ScriptName.Click += ScriptOnClick;
                }

                //when click on "+" button in script item
                void BtnPlusOnClick(object sender, EventArgs args)
                {
                    if (vh.BtnPlusIsClicked == false)
                    {
                        vh.BtnPlusIsClicked = true;
                        ScriptSelectedCounter++;

                        Scripts.SetRunBarVisibility(VisibilityFlags.Visible);

                        //change "+" to check mark
                        vh.BtnPlus.SetImageResource(Resource.Drawable.chech_mark);

                        Scripts.SelectedScripts.Add(list[position]);
                    }
                    else
                    {
                        vh.BtnPlusIsClicked = false;
                        ScriptSelectedCounter--;

                        //change check mark to "+"
                        vh.BtnPlus.SetImageResource(Resource.Drawable.plus_btn);

                        Scripts.SelectedScripts.Remove(list[position]);
                    }

                    if (ScriptSelectedCounter == 0) Scripts.SetRunBarVisibility(VisibilityFlags.Invisible);
                }
            }

            //when click on item
            void ScriptOnClick(object sender, EventArgs e)
            {
                //create the "Script Info" dialog
                LayoutInflater layoutInflater = LayoutInflater.From(context);
                View v = layoutInflater.Inflate(Resource.Layout.file_info_dialog, null);
                Button okayBtn = v.FindViewById<Button>(Resource.Id.ok_btn);
                TextView dateCreated = v.FindViewById<TextView>(Resource.Id.creating_date);
                TextView content = v.FindViewById<TextView>(Resource.Id.script_info);

                dateCreated.Text = list[position].ScriptDateCreated;
                content.Text = list[position].ScriptData;

                AlertDialog.Builder builder = new AlertDialog.Builder(context, Resource.Style.AlertDialogTheme);
                builder.SetView(v);
                var dialog = builder.Create();
                dialog.Show();
                dialog.Window.SetBackgroundDrawableResource(Resource.Drawable.script_info_background);

                //if click on "Okay" — close "Script Info" dialog
                okayBtn.Click += delegate { dialog.Hide(); };
            }
        }
    }
}