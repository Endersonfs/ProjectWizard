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
        TextView btnHaveAccount;
        EditText inputEmail, inputPassword;
        RelativeLayout signupLayout;
        string emailError = "";
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        ProgressDialog process;
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

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

            inputEmail = FindViewById<EditText>(Resource.Id.txtEmailRegister);
            inputPassword = FindViewById<EditText>(Resource.Id.txtPasswordRegister);

            signupLayout = FindViewById<RelativeLayout>(Resource.Id.registerLayout);

            //action listener
            btnSignup.SetOnClickListener(this);
            btnHaveAccount.SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            if(v.Id == Resource.Id.txtHaveAccount)
            {
                StartActivity(new Intent(this, typeof(Login)));
                Finish();
            }
            else if (v.Id == Resource.Id.btnSignup)
            {
                SignUpUser(inputEmail.Text, inputPassword.Text);
            }
        }

        public bool isValidEmail(string email)
        {
            return Android.Util.Patterns.EmailAddress.Matcher(email).Matches();
        }

        private Boolean Error()
        {
            var emailvalidate = isValidEmail(inputEmail.Text);
            if (inputEmail.Text.ToString().Trim().Equals("") && emailvalidate !=true)
            {
                emailError = " o email invalido";
                return false;
            }
            if (inputPassword.Text.ToString().Trim().Equals(""))
            {
                return false;
            }
            return true;
        }

        private void SignUpUser(string email, string pass)
        {
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            process = new ProgressDialog(this);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
            if (Error())
            {
                auth.CreateUserWithEmailAndPassword(email, pass)
                    .AddOnCompleteListener(this, this);
                process.SetMessage("Validando informacion, espere.");
                process.Show();
            }
            else
            {
                if (process.IsShowing) { process.Dismiss(); }
                Snackbar snackBar = Snackbar.Make(signupLayout, "Register Failed, campos vacios "+emailError, Snackbar.LengthShort);
                snackBar.Show();
            }
        }

        public void OnComplete(Task task)
        {
            if(task.IsSuccessful == true)
            {
                if (process.IsShowing) { process.Dismiss(); }
                Snackbar snackBar = Snackbar.Make(signupLayout, "Register Success", Snackbar.LengthShort);
                snackBar.Show();
                inputEmail.Text = "";
                inputPassword.Text = "";
            }
            else
            {
                if (process.IsShowing) { process.Dismiss(); }
                Snackbar snackBar = Snackbar.Make(signupLayout, "Register failed", Snackbar.LengthShort);
                snackBar.Show();
            }
        }
    }
}