
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
            ((ScriptsViewHolder) viewHolder).ScriptBackground.Visibility = ViewStates.Visible;
            ((ScriptsViewHolder) viewHolder).ScriptForeground.Animate().TranslationX(-250);
            Scripts.ItemTouchHelperAttach(false);
            ((ScriptsViewHolder) viewHolder).BtnPlus.SetImageResource(Resource.Drawable.Back);
            ((ScriptsViewHolder) viewHolder).BtnPlus.Click += delegate
            {
                ((ScriptsViewHolder) viewHolder).ScriptBackground.Visibility = ViewStates.Gone;
                ((ScriptsViewHolder) viewHolder).ScriptForeground.TranslationX = 0;
                Scripts.ItemTouchHelperAttach(true);
            };
            ((ScriptsViewHolder) viewHolder).ScriptBackground.Visibility = ViewStates.Visible;
        }
    }
}