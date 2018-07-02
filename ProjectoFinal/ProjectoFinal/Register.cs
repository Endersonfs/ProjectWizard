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
    [Activity(Label = "Register")]
    public class Register : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        Button btnSignup;
        TextView btnHaveAccount, btnForgot;
        EditText inputEmail, inputPassword;
        RelativeLayout signupLayout;

        FirebaseAuth auth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.register);

            //fire
            auth = FirebaseAuth.GetInstance(Login.app);

            //datos
            btnSignup = FindViewById<Button>(Resource.Id.btnSignup);

            btnHaveAccount = FindViewById<TextView>(Resource.Id.txtHaveAccount);
            btnForgot = FindViewById<TextView>(Resource.Id.txtForgotPassSignup);

            inputEmail = FindViewById<EditText>(Resource.Id.txtEmailRegister);
            inputPassword = FindViewById<EditText>(Resource.Id.txtPasswordRegister);

            signupLayout = FindViewById<RelativeLayout>(Resource.Id.registerLayout);

            //action listener
            btnSignup.SetOnClickListener(this);
            btnHaveAccount.SetOnClickListener(this);
            btnForgot.SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            if(v.Id == Resource.Id.txtHaveAccount)
            {
                StartActivity(new Intent(this, typeof(Login)));
                Finish();
            }
            else if(v.Id == Resource.Id.txtForgotPassSignup)
            {
                StartActivity(new Intent(this, typeof(ForgotActivity)));
                Finish();
            }
            else if (v.Id == Resource.Id.btnSignup)
            {
                SignUpUser(inputEmail.Text, inputPassword.Text);
            }
        }

        private void SignUpUser(string email, string pass)
        {
            auth.CreateUserWithEmailAndPassword(email, pass)
                .AddOnCompleteListener(this,this);
        }

        public void OnComplete(Task task)
        {
            if(task.IsSuccessful == true)
            {
                Snackbar snackBar = Snackbar.Make(signupLayout, "Register Success", Snackbar.LengthShort);
                snackBar.Show();
            }
            else
            {
                Snackbar snackBar = Snackbar.Make(signupLayout, "Register failed", Snackbar.LengthShort);
                snackBar.Show();
            }
        }
    }
}