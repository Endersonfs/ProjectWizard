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
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Database;
using Java.Lang;
using Newtonsoft.Json;
using Org.Json;
using ProjectoFinal.Models;
using static Android.Views.View;

namespace ProjectoFinal
{
    [Activity(Label = "PerfilActivity", ParentActivity = typeof(CentroActivity))]
    public class PerfilActivity : AppCompatActivity, IOnClickListener
    {
        private EditText inputName, inputEmail, inputLastname, inputLocation;
        private Usuario usuario;
        private Button btnUpdate;

        FirebaseAuth auth;

        private DatabaseReference mDatabse;
        private const string FirebaseURL = "https://projectofinal-32957.firebaseio.com/";


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Perfil);

            //auth
            auth = FirebaseAuth.GetInstance(Login.app);

            //Barra
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            // SupportActionBar.SetTitle(Resource.String.app_name);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            inputEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            inputName = FindViewById<EditText>(Resource.Id.txtFirstName);
            inputLastname = FindViewById<EditText>(Resource.Id.txtLastname);
            inputLocation = FindViewById<EditText>(Resource.Id.txtLocationUser);
            btnUpdate = FindViewById<Button>(Resource.Id.btnUpdate);

            btnUpdate.SetOnClickListener(this);

            Task.Delay(2000);
            inputEmail.Text = auth.CurrentUser.Email;


        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            StartActivity(new Intent(this, typeof(CentroActivity)));
            Finish();
        }

        private async void writeNewUser(string userId, string firstname, string lastname, string location, string email)
        {
            mDatabse = FirebaseDatabase.GetInstance(FirebaseURL).GetReference("projectofinal-32957");

            usuario = new Usuario(userId, firstname, lastname, location, email);

            //esto funciona
            //var resp = mDatabse.Child("test");
            //await resp.SetValueAsync("juan");

            var resp = mDatabse.Child("Usuario/"+usuario.uid);
            await resp.SetValueAsync(usuario.uid);

            var resp3 = mDatabse.Child($"Usuario/{usuario.uid}/FirstName");
            await resp3.SetValueAsync(usuario.firstname);

            var resp4 = mDatabse.Child($"Usuario/{usuario.uid}/LastName");
            await resp4.SetValueAsync(usuario.lastname);

            var resp5 = mDatabse.Child($"Usuario/{usuario.uid}/Location");
            await resp5.SetValueAsync(usuario.location);

            var resp2 = mDatabse.Child($"Usuario/{usuario.uid}/Email");
            await resp2.SetValueAsync(usuario.email);

            //Deberia insertar esto
            //mDatabse.Child("Usuario").Child(userId).SetValue(usuario);
        }

        public void OnClick(View v)
        {
            if(v.Id == Resource.Id.btnUpdate)
            {
                var uid = auth.CurrentUser.Uid;
                var firstname = inputName.Text.Trim();
                var email = inputEmail.Text.Trim();
                var lastname = inputLastname.Text.Trim();
                var location = inputLocation.Text.Trim();
                writeNewUser(uid, firstname,lastname, location, email);
            }
        }
    }
}