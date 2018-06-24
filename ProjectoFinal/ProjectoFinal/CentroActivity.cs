using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.Transitions;
using Android.Support.Fragment;
using ProjectoFinal.Resources.Fragments;

namespace ProjectoFinal
{
    [Activity(Label = "CentroActivity")]
    public class CentroActivity : Activity
    {
        BottomNavigationView bottomNavigation;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main);
            // Create your application here

            bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            bottomNavigation.NavigationItemSelected += BottomNavigation_NavigationItemSelected;
            LoadFragment(Resource.Id.menu_home);

        }

        private void BottomNavigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }

        void LoadFragment(int id)
        {
            Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.menu_home:
                    fragment = FragmentHome.NewInstance();
                    break;
                case Resource.Id.menu_add_location:
                    fragment = FragmentAdd.NewInstance();
                    break;
                case Resource.Id.menu_message:
                    fragment = FragmentMessage.NewInstance();
                    break;
                case Resource.Id.menu_settings:
                    fragment = FragmentSettings.NewInstance();
                    break;
            }
            if (fragment == null)
                return;
            FragmentManager.BeginTransaction()
               .Replace(Resource.Id.content_frame, fragment)
               .Commit();
        }

    }
}