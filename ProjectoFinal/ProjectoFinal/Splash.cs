using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ProjectoFinal
{
    [Activity(Label = "Splash", MainLauncher = true, NoHistory = true)]
    public class Splash : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(Splash).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistableBundle)
        {
            base.OnCreate(savedInstanceState, persistableBundle);
            //SetContentView(Resource.Layout.Inicio);
            Log.Debug(TAG, "Splash.OnCreate");

            // Create your application here
            
        }

        protected override void OnResume()
        {
            base.OnResume();
            SetContentView(Resource.Layout.Inicio);
            Task startupWork = new Task(() => { SimulatedStartup(); });
            startupWork.Start();
        }

        async void SimulatedStartup()
        {
            
            Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            await Task.Delay(4000);
            Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
            StartActivity(new Intent(Application.Context, typeof(Login)));
        }
    }
}