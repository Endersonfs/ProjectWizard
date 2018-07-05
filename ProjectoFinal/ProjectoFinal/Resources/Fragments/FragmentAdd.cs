using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using static Android.Views.View;

namespace ProjectoFinal.Resources.Fragments
{
    public class FragmentAdd : Fragment, IOnClickListener
    {
        Button btnsavebook, btncanelarbook;
        EditText txtlocationbook, txtbookname, txtdescripcionbok;
        CheckBox chkventa, chkintercambio;
        LinearLayout booklayout;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
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

            btnsavebook.SetOnClickListener(this);
            return v;

        }

        public void OnClick(View v)
        {
            if(v.Id == Resource.Id.btnsavebook)
            {
                Snackbar snackBar = Snackbar.Make(booklayout, ""+txtlocationbook.Text, Snackbar.LengthShort);
                snackBar.Show();
            }
        }
    }
}