using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ProjectoFinal
{
    [Activity(Label = "Login")]
    public class Login : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.login);

            var btn = FindViewById<Button>(Resource.Id.btnSignup);
            btn.Click += Btn_Click;


        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CentroActivity));
            StartActivity(intent);

        }

        private void Btnregister_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Register));
            StartActivity(intent);
        }
    }
}