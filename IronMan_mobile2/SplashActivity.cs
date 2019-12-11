using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;

namespace IronMan_mobile2
{
    [Activity(Theme = "@style/SplashScreen", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }
    }
}