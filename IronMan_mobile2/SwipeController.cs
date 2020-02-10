
using System;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
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

        public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target)
        {
            return false;
        }
        
        public override int GetSwipeDirs(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder)
        {
            return base.GetSwipeDirs(recyclerView, viewHolder);
        }

        public override void OnChildDraw(Canvas c, RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, float dX, float dY, int actionState,
            bool isCurrentlyActive)
        {
        }


        public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
        {
            var holder = (ScriptsViewHolder) viewHolder;
            holder.ScriptBackground.Visibility = ViewStates.Visible;
            holder.ScriptForeground.Animate().TranslationX(-250);
            Scripts.ItemTouchHelperAttach(false);
            holder.BtnPlus.Visibility = ViewStates.Gone;
            holder.BtnBack.Visibility = ViewStates.Visible;
        }
    }
}