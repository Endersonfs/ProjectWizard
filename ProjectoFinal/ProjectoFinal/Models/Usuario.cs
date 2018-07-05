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
using Java.Util;

namespace ProjectoFinal.Models
{
    public class Usuario
    {
        public string uid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string location { get; set; }
        public string email { get; set; }
        public string genero { get; set; }

        public Usuario()
        {
        }


        public Usuario(string uid, string firstname, string lastname, string location, string email, string genero)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.email = email;
            this.location = location;
            this.uid = uid;
            this.genero = genero;
        }

        public HashMap ToMap()
        {

            HashMap map = new HashMap();
            map.Put("Uid", this.uid);
            map.Put("FirstName", this.firstname);
            map.Put("LastName", this.lastname);
            map.Put("Email", this.email);
            map.Put("Genero", this.genero);
            map.Put("Location", this.location);
            return map;
        }


        public string getfistname()
        {
            return firstname;
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