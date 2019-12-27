using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Util;
using Android.Views;

namespace IronMan_mobile2
{
    public class SwipeController : ItemTouchHelper.SimpleCallback
    {
        public SwipeController(int dragDirs, int swipeDirs) : base(dragDirs, swipeDirs)
        {
        }

        public override int GetMovementFlags(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder)
        {
            return MakeMovementFlags(0, ItemTouchHelper.Left);
        }

        public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder,
            RecyclerView.ViewHolder target)
        {
            return false;
        }

        public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
        {
            Scripts.RemoveScript(viewHolder.AdapterPosition);
            AlertDialog.Builder builder = new AlertDialog.Builder(Scripts.context, Resource.Style.AlertDialogTheme);
            builder.SetTitle("Do you want to delete script?");
            builder.SetCancelable(false)
                .SetPositiveButton("Delete", delegate { })
                .SetNegativeButton("Cancel", delegate
                {
                    Scripts.InsertScript();
                    builder.Dispose();
                });
            var dialog = builder.Create();
            
            dialog.Show();
        }
        
    }
}