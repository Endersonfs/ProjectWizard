using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;

namespace ProjectoFinal.Models
{
    public class Usuario
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string email { get; set; }

        public  Usuario(string uid, string name, string email)
        {
            this.name = name;
            this.email = email;
            this.uid = uid;
        }


        public string getname()
        {
            return name;
        }

        public string getemail()
        {
            return email;
        }
        public string getuid()
        {
            return uid;
        }


    }
}