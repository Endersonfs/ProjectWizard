using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ProjectoFinal.Resources.Fragments
{
    public class FragmentHome : Fragment, IOnMapReadyCallback
    {
        private GoogleMap _map;
        private MapFragment _mapFragment;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            _mapFragment = FragmentManager.FindFragmentByTag("map") as MapFragment;
            if (_mapFragment == null)
                           {
               GoogleMapOptions mapOptions = new GoogleMapOptions()
                    .InvokeMapType(GoogleMap.MapTypeSatellite)
                    .InvokeZoomControlsEnabled(false)
                    .InvokeCompassEnabled(true);
                
                FragmentTransaction fragTx = FragmentManager.BeginTransaction();
                _mapFragment = MapFragment.NewInstance(mapOptions);
                fragTx.Add(Resource.Id.map, _mapFragment, "map");
                fragTx.Commit();
            }
            _mapFragment.GetMapAsync(this);

        }

        public static FragmentHome NewInstance()
        {
            var frag1 = new FragmentHome { Arguments = new Bundle() };
            return frag1;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment

            return inflater.Inflate(Resource.Layout.Fragemethome, container, false);

        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            MapFragment mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);


        }

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;
            MarkerOptions markerOptions = new MarkerOptions();
            markerOptions.SetPosition(new LatLng(16.03, 108));
            markerOptions.SetTitle("test");
            markerOptions.Draggable(true);
            markerOptions.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.ic_book_cyan_A700_24dp));
            _map.AddMarker(markerOptions);
            //zoom
            _map.UiSettings.ZoomControlsEnabled = true;
            _map.UiSettings.CompassEnabled = true;
            _map.MoveCamera(CameraUpdateFactory.ZoomIn());
            if (_map != null)
            {
                _map.MapType = GoogleMap.MapTypeNormal;
            }
        }
    }
}