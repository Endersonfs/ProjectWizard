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
        static EditText inputName, inputEmail, inputLastname, inputLocation;
        static Spinner spinner;
        static string spinerData;
        static ArrayAdapter adapter;
        private Usuario usuario;
        private Button btnUpdate;

        FirebaseAuth auth;
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        ProgressDialog process;
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos


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

            //input y botones
            inputEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            inputName = FindViewById<EditText>(Resource.Id.txtFirstName);
            inputLastname = FindViewById<EditText>(Resource.Id.txtLastname);
            inputLocation = FindViewById<EditText>(Resource.Id.txtLocationUser);
            btnUpdate = FindViewById<Button>(Resource.Id.btnUpdate);
            btnUpdate.SetOnClickListener(this);

            //spinner
            spinner = FindViewById<Spinner>(Resource.Id.spnGenero);
            spinner.Prompt = "Choose your favorite";
            spinner.ItemSelected +=  new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.prompt_data, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            ///spinner.SetSelection(adapter.GetPosition("Mars"));


            //cargar
            FullCharger();
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = sender as Spinner;
            Toast.MakeText(this, "Your choose: "+spinner.GetItemAtPosition(e.Position), ToastLength.Short ).Show();
            spinerData = Convert.ToString(spinner.GetItemAtPosition(e.Position));
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            StartActivity(new Intent(this, typeof(CentroActivity)));
            Finish();
        }

        private void GetData()
        {
            FirebaseDatabase
                .GetInstance(FirebaseURL)
                .GetReference("projectofinal-32957/Usuario/")
                .Child(auth.CurrentUser.Uid)
                .AddListenerForSingleValueEvent(new DataValueEventListener());
        }

        private async void writeNewUser(string userId, string firstname, string lastname, string location, string email, string genero)
        {
            mDatabse = FirebaseDatabase.GetInstance(FirebaseURL).GetReference("projectofinal-32957");

            usuario = new Usuario(userId, firstname, lastname, location, email,genero);
            //esto funciona
            //var resp = mDatabse.Child("test");
            //await resp.SetValueAsync("juan");

            var resp = mDatabse.Child("Usuario/"+usuario.uid);
            await resp.SetValueAsync(usuario.uid);

            resp = mDatabse.Child($"Usuario/{usuario.uid}/FirstName");
            await resp.SetValueAsync(usuario.firstname);

            resp = mDatabse.Child($"Usuario/{usuario.uid}/LastName");
            await resp.SetValueAsync(usuario.lastname);

            resp = mDatabse.Child($"Usuario/{usuario.uid}/Location");
            await resp.SetValueAsync(usuario.location);

            resp = mDatabse.Child($"Usuario/{usuario.uid}/Email");
            await resp.SetValueAsync(usuario.email);

            resp = mDatabse.Child($"Usuario/{usuario.uid}/Genero");
            await resp.SetValueAsync(usuario.genero);

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
                var genero = Convert.ToString(spinerData);
                writeNewUser(uid, firstname,lastname, location, email, genero);
            }
        }

        class DataValueEventListener : Java.Lang.Object, IValueEventListener
        {
            private Usuario snak;

            //private Usuario snak;

            public void OnCancelled(DatabaseError error)
            {
                // Handle error however you have to
            }

            public void OnDataChange(DataSnapshot snapshot)
            {
                if (snapshot == null) return;
                snak = new Usuario();
                snak.uid = snapshot.Key;
                snak.email = snapshot.Child("Email").GetValue(true).ToString();
                snak.firstname = snapshot.Child("FirstName").GetValue(true).ToString();
                snak.lastname = snapshot.Child("LastName").GetValue(true).ToString();
                snak.location = snapshot.Child("Location").GetValue(true).ToString();
                snak.genero = snapshot.Child("Genero").GetValue(true).ToString();
                LlenaCampos(snak.firstname, snak.lastname, snak.email, snak.location, snak.genero);


            }
        }

        static void LlenaCampos(string fname, string lname, string email, string location, string genero)
        {
            inputEmail.Text = email;
            inputName.Text = fname;
            inputLastname.Text = lname;
            inputLocation.Text = location;
            spinner.SetSelection(adapter.GetPosition(genero));
        }

        private void FullCharger()
        {
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            process = new ProgressDialog(this);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
            process.SetMessage("Cargando informacion, espere.");
            process.Show();
            GetData();
            Task.Delay(3000);
            if (process.IsShowing) { process.Dismiss(); }

        }
    }
}