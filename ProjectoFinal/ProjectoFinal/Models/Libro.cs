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

namespace ProjectoFinal.Models
{
    public class Libro
    {
        public string name { get; set; }
        public string uid { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public bool sale { get; set; }
        public string gender { get; set; }
        public bool change { get; set; }
        public string owner { get; set; }

        public Libro() { }

        public Libro(string uid, string nombre, string descripcion, string ubicacion,
            bool venta, bool intercambio, string duegno, string gender)
        {
            this.uid = uid;
            this.name = nombre;
            this.description = descripcion;
            this.location = ubicacion;
            this.sale = venta;
            this.change = intercambio;
            this.owner = duegno;
            this.gender = gender;
        }
    }
}