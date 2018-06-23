using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System;
using Android.Content;

namespace ProjectoFinal
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            var btn = FindViewById<Button>(Resource.Id.button1);
            btn.Click += Btn_Click;

        }

        private void Btn_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Login));
            StartActivity(intent);
        }
    }
}

