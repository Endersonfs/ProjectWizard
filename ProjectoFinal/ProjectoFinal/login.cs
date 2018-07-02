using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;

namespace ProjectoFinal
{
    [Activity(Label = "Login")]
    public class Login : AppCompatActivity, IOnCompleteListener
    {

        EditText input_email, input_password;
        TextView forgotPassword;
        RelativeLayout loginLayout;
        public static FirebaseApp app;
        FirebaseAuth auth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.login);
            InitFirebaseAuth();

            var btnregister = FindViewById<Button>(Resource.Id.btnSignup);
            btnregister.Click += Btnregister_Click;

            var btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnLogin.Click += BtnLogin_Click;

            input_email = FindViewById<EditText>(Resource.Id.txtEmail);
            input_password = FindViewById<EditText>(Resource.Id.txtPassword);
            forgotPassword = FindViewById<TextView>(Resource.Id.txtForgotPasswordLogin);
            loginLayout = FindViewById<RelativeLayout>(Resource.Id.LoginLayout);

            forgotPassword.Click += ForgotPassword_Click;


        }

        private void InitFirebaseAuth()
        {
            var options = new FirebaseOptions.Builder()
                .SetApplicationId("1:671501226297:android:7d6a044c9c07f354")
                .SetApiKey("AIzaSyAMntWQy4Bp9rVKnnb7IZLx1uz0S2meGec")
                .Build();

            if(app == null)
            {
                app = FirebaseApp.InitializeApp(this, options);
            }
            auth = FirebaseAuth.GetInstance(app);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            LoginUser(input_email.Text, input_password.Text);
        }

        private void Btnregister_Click(object sender, EventArgs e)
        {
            StartActivity(new Android.Content.Intent(this, typeof(Register)));
        }

        private void ForgotPassword_Click(object sender, EventArgs e)
        {
            StartActivity(new Android.Content.Intent(this, typeof(ForgotPassword)));
        }

        private void LoginUser(string email, string pass)
        {
            auth.SignInWithEmailAndPassword(email, pass)
                .AddOnCompleteListener(this);
            
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                StartActivity(new Android.Content.Intent(this, typeof(CentroActivity)));
                Finish();
            }
            else
            {
                Snackbar snackBar = Snackbar.Make(loginLayout, "Login Failed", Snackbar.LengthShort);
                snackBar.Show();
            }
        }
    }
}