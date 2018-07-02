using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using static Android.Views.View;

namespace ProjectoFinal
{
    [Activity(Label = "ForgotActivity")]
    public class ForgotActivity : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        private EditText inputEmail;
        private Button btnReset;
        private TextView btnback;
        private RelativeLayout forgotLayout;
        string emailError = "";

        FirebaseAuth auth;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ForgotPassword);

            //init
            auth = FirebaseAuth.GetInstance(Login.app);

            //datos
            inputEmail = FindViewById<EditText>(Resource.Id.txtEmailForgot);
            btnReset = FindViewById<Button>(Resource.Id.btnReset);
            btnback = FindViewById<TextView>(Resource.Id.txtBack);
            forgotLayout = FindViewById<RelativeLayout>(Resource.Id.forgotLayout);

            btnReset.SetOnClickListener(this);
            btnback.SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            if(v.Id == Resource.Id.txtBack)
            {
                StartActivity(new Intent(this, typeof(Login)));
                Finish();
            }
            else if(v.Id == Resource.Id.btnReset)
            {
                ResetPassword(inputEmail.Text);
            }
        }

        public bool isValidEmail(string email)
        {
            return Android.Util.Patterns.EmailAddress.Matcher(email).Matches();
        }

        private Boolean Error()
        {
            var emailvalidate = isValidEmail(inputEmail.Text);
            if (inputEmail.Text.ToString().Trim().Equals(""))
            {
                emailError = "campo vacio";
                return false;
            }
            else if(emailvalidate != true)
            {
                emailError = "email invalido";
                return false;
            }
            return true;
        }
        private void ResetPassword(string email)
        {
            if (Error())
            {
                auth.SendPasswordResetEmail(email)
                    .AddOnCompleteListener(this, this);
            }
            else
            {
                Snackbar snackBar = Snackbar.Make(forgotLayout, "Reset password failed, "+ emailError, Snackbar.LengthShort);
                snackBar.Show();
            }
        }

        public void OnComplete(Task task)
        {
            if(task.IsSuccessful == false)
            {
                Snackbar snackbar = Snackbar.Make(forgotLayout, "Reset password failed", Snackbar.LengthShort);
                snackbar.Show();
            }
            else
            {
                Snackbar snackbar = Snackbar.Make(forgotLayout, "Reset password link set to email: "+inputEmail.Text, Snackbar.LengthShort);
                snackbar.Show();
                inputEmail.Text = "";
            }
        }
    }
}