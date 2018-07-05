using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Database;
using ProjectoFinal.Models;
using System;
using static Android.Views.View;

namespace ProjectoFinal.Resources.Fragments
{
    public class FragmentAdd : Fragment, IOnClickListener
    {
        Button btnsavebook, btncanelarbook;
        static EditText txtlocationbook, txtbookname, txtdescripcionbok;
        static Spinner spinner;
        static string spinerData;
        static ArrayAdapter adapter;
        static CheckBox chkventa, chkintercambio;
        static bool venta, cambio;
        LinearLayout booklayout;

        Libro libro;

        FirebaseAuth auth;
        private DatabaseReference mDatabse;
        private const string FirebaseURL = "https://projectofinal-32957.firebaseio.com/";

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            auth = FirebaseAuth.GetInstance(Login.app);

        }
        public static FragmentAdd NewInstance()
        {
            var frag1 = new FragmentAdd { Arguments = new Bundle() };
            return frag1;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.FragmentAdd, container, false);

            btnsavebook = v.FindViewById<Button>(Resource.Id.btnsavebook);
            btncanelarbook = v.FindViewById<Button>(Resource.Id.btncancelarbook);
            txtlocationbook = v.FindViewById<EditText>(Resource.Id.txtlocationbook);
            txtbookname = v.FindViewById<EditText>(Resource.Id.txtbookname);
            txtdescripcionbok = v.FindViewById<EditText>(Resource.Id.txtdescripcionbook);
            booklayout = v.FindViewById<LinearLayout>(Resource.Id.bookLayout); ;
            chkventa = v.FindViewById<CheckBox>(Resource.Id.chkventas);
            chkintercambio = v.FindViewById<CheckBox>(Resource.Id.chkintercambio);
            //spinner
            spinner = v.FindViewById<Spinner>(Resource.Id.spnbookgenero);
            spinner.Prompt = "Choose your favorite";
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            adapter = ArrayAdapter.CreateFromResource(this.Context, Resource.Array.prompt_data, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            btnsavebook.SetOnClickListener(this);

            chkventa.Click += (o, e) =>
            {
                if (chkventa.Checked)
                {
                    Toast.MakeText(this.Context, "Selected", ToastLength.Short).Show();
                    venta = true;
                }
                else
                {
                    Toast.MakeText(this.Context, "Not selected", ToastLength.Short).Show();
                    venta = false;
                }
            };
            chkintercambio.Click += (o, e) =>
            {
                if (chkintercambio.Checked)
                {
                    Toast.MakeText(this.Context, "Selected", ToastLength.Short).Show();
                    cambio = true;
                }
                else
                {
                    Toast.MakeText(this.Context, "Not selected", ToastLength.Short).Show();
                    cambio = false;
                }
            };
            return v;
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = sender as Spinner;
            Toast.MakeText(this.Context, "Your choose: " + spinner.GetItemAtPosition(e.Position), ToastLength.Short).Show();
            spinerData = Convert.ToString(spinner.GetItemAtPosition(e.Position));
        }

        public void OnClick(View v)
         {
            if(v.Id == Resource.Id.btnsavebook)
            {
                Snackbar snackBar = Snackbar.Make(booklayout, ""+txtlocationbook.Text, Snackbar.LengthShort);
                snackBar.Show();

                var uid = Guid.NewGuid().ToString();
                var nombre = txtbookname.Text.Trim();
                var descripcion = txtdescripcionbok.Text.Trim();
                var ubicacion = txtlocationbook.Text.Trim();
                var duegno = auth.CurrentUser.Uid;
                var genero = Convert.ToString(spinerData);
                writeNewUser(uid,nombre,descripcion,ubicacion,venta,cambio,duegno,genero);
            }
        }

        private async void writeNewUser(string uid, string nombre, string descripcion, string ubicacion,
            bool venta, bool intercambio, string duegno, string genero)
        {
            mDatabse = FirebaseDatabase.GetInstance(FirebaseURL).GetReference("projectofinal-32957");

            libro = new Libro(uid,nombre,descripcion,ubicacion,
            venta,intercambio,duegno, genero);
            //esto funciona
            //var resp = mDatabse.Child("test");
            //await resp.SetValueAsync("juan");

            var resp = mDatabse.Child("Libro/" + libro.uid);
            await resp.SetValueAsync(libro.uid);

            resp = mDatabse.Child($"Libro/{libro.uid}/Name");
            await resp.SetValueAsync(libro.name);

            resp = mDatabse.Child($"Libro/{libro.uid}/Description");
            await resp.SetValueAsync(libro.description);

            resp = mDatabse.Child($"Libro/{libro.uid}/Location");
            await resp.SetValueAsync(libro.location);

            resp = mDatabse.Child($"Libro/{libro.uid}/Sale");
            await resp.SetValueAsync(libro.sale);

            resp = mDatabse.Child($"Libro/{libro.uid}/Change");
            await resp.SetValueAsync(libro.change);

            resp = mDatabse.Child($"Libro/{libro.uid}/Owner");
            await resp.SetValueAsync(libro.owner);

            resp = mDatabse.Child($"Libro/{libro.uid}/Gender");
            await resp.SetValueAsync(libro.gender);

            //Deberia insertar esto
            //mDatabse.Child("Usuario").Child(userId).SetValue(usuario);
        }

    }
}