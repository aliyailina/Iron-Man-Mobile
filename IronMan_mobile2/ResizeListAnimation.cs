using Android.Views;
using Android.Views.Animations;

namespace IronMan_mobile2
{
    public class ResizeListAnimation : Animation
    {
        private int mFromHeight;
        private int mToHeight;
        private View mView;

        public ResizeListAnimation(View view, int fromHeight, int toHeight)
        {
            mFromHeight = fromHeight;
            mToHeight = toHeight;
            mView = view;
        }

        protected override void ApplyTransformation(float interpolatedTime, Transformation t)
        {
            int newHeight;
            
            if (mView.Height != mToHeight)
            {
                newHeight = (int) (mFromHeight + (mToHeight - mFromHeight) * interpolatedTime);
                mView.LayoutParameters.Height = newHeight;
                mView.RequestLayout();
            }
        }

        public override bool WillChangeBounds()
        {
            return true;
        }
    }
}