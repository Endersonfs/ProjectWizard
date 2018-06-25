using Android.App;
using Android.OS;
using Android.Views;
using Android.Support.Design.Widget;
using ProjectoFinal.Resources.Fragments;
using Android.Support.V7.App;
using Android.Widget;
using Android.Support.V7.Widget;
using Android.Support.V4.Widget;

namespace ProjectoFinal
{
    [Activity(Label = "CentroActivity")]
    public class CentroActivity : AppCompatActivity
    {
        BottomNavigationView bottomNavigation;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            DrawerLayout drawerLayouts;

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main);
            // Create your application here

            bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            bottomNavigation.NavigationItemSelected += BottomNavigation_NavigationItemSelected;
            LoadFragment(Resource.Id.menu_home);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetTitle(Resource.String.app_name);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            drawerLayouts = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            // Attach item selected handler to navigation view
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            // Create ActionBarDrawerToggle button and add it to the toolbar
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayouts, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            drawerLayouts.SetDrawerListener(drawerToggle);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
            drawerToggle.SyncState();
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