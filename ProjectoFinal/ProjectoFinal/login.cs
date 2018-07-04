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
using static Android.Views.View;

namespace ProjectoFinal
{
    [Activity(Label = "Login", NoHistory = true)]
    public class Login : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        
        EditText input_email, input_password;
        TextView forgotPassword;
        RelativeLayout loginLayout;
        Button btnregister, btnLogin;
        public static FirebaseApp app;
        FirebaseAuth auth;
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        ProgressDialog process;
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.login);
            InitFirebaseAuth();

            btnregister = FindViewById<Button>(Resource.Id.btnSignup);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);

            input_email = FindViewById<EditText>(Resource.Id.txtEmail);
            input_password = FindViewById<EditText>(Resource.Id.txtPassword);
            forgotPassword = FindViewById<TextView>(Resource.Id.txtForgotPasswordLogin);
            loginLayout = FindViewById<RelativeLayout>(Resource.Id.LoginLayout);

            btnregister.SetOnClickListener(this);
            btnLogin.SetOnClickListener(this);
            forgotPassword.SetOnClickListener(this);


        }
        protected override void OnStart()
        {
            base.OnStart();
            FirebaseUser firebaseUser = auth.CurrentUser;
            updateUI(firebaseUser);

        }

        private void updateUI(FirebaseUser user)
        {
            if (user != null)
            {

                Toast.MakeText(this, "Logeado", ToastLength.Short).Show();
                Intent intent = new Intent(this, typeof(CentroActivity));
                StartActivity(intent);


            }
            else
            {
                Toast.MakeText(this, "NO LOGIN", ToastLength.Short).Show();

            }
        }

        private void InitFirebaseAuth()
        {
            var options = new FirebaseOptions.Builder()
                .SetApplicationId("1:671501226297:android:7d6a044c9c07f354")
                .SetApiKey("AIzaSyAMntWQy4Bp9rVKnnb7IZLx1uz0S2meGec")
                .SetDatabaseUrl("https://projectofinal-32957.firebaseio.com/")
                .Build();

            if(app == null)
            {
                app = FirebaseApp.InitializeApp(this, options);
            }
            auth = FirebaseAuth.GetInstance(app);
        }

        public void OnClick(View v)
        {
            if(v.Id == Resource.Id.btnLogin)
            {
                LoginUser(input_email.Text, input_password.Text);
            }
            else if(v.Id == Resource.Id.btnSignup)
            {
                StartActivity(new Intent(this, typeof(Register)));
                Finish();
            }
            else if(v.Id == Resource.Id.txtForgotPasswordLogin)
            {
                StartActivity(new Intent(this, typeof(ForgotActivity)));
                Finish();
            }
        }

        private Boolean Error()
        {
            
            if (input_email.Text.ToString().Trim().Equals(""))
            {
                return false;
            }
            if (input_password.Text.ToString().Trim().Equals(""))
            {
                return false;
            }
            return true;
        }

        private void LoginUser(string email, string pass)
        {
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            process = new ProgressDialog(this);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
            if (Error()) {
                auth.SignInWithEmailAndPassword(email, pass)
                    .AddOnCompleteListener(this);
                process.SetMessage("Validando informacion, espere.");
                process.Show();
            }
            else
            {
                if (process.IsShowing) { process.Dismiss(); }
                Snackbar snackBar = Snackbar.Make(loginLayout, "Login Failed, campos vacios o email invalido", Snackbar.LengthShort);
                snackBar.Show();
            }
            
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                if (process.IsShowing) { process.Dismiss(); }
                Toast.MakeText(this, ""+auth.CurrentUser.Email, ToastLength.Short).Show();
                StartActivity(new Android.Content.Intent(this, typeof(CentroActivity)));
                Finish();
                input_email.Text = "";
                input_password.Text = "";
            }
            else
            {
                if (process.IsShowing) { process.Dismiss(); }
                Snackbar snackBar = Snackbar.Make(loginLayout, "Login Failed", Snackbar.LengthShort);
                snackBar.Show();
            }
        }


        
    }
}