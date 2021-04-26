using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.BottomNavigation;
using Google.Android.Material.FloatingActionButton;

namespace flashCards
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        TextView textMessage;
        ListView cardSetsListView;
        FloatingActionButton newSetButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            textMessage = FindViewById<TextView>(Resource.Id.message);
            cardSetsListView = FindViewById<ListView>(Resource.Id.listView1);
            newSetButton = FindViewById<FloatingActionButton>(Resource.Id.fab);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    textMessage.SetText(Resource.String.title_home);
                    cardSetsListView.Visibility = ViewStates.Gone;
                    textMessage.Visibility = ViewStates.Visible;
                    return true;
                case Resource.Id.cardSet_dashboard:
                    ShowCardSets();
                    return true;
                case Resource.Id.setting_dashboard:
                    textMessage.SetText(Resource.String.title_settings);
                    cardSetsListView.Visibility = ViewStates.Gone;
                    textMessage.Visibility = ViewStates.Visible;
                    return true;
            }
            return false;
        }

        public void ShowCardSets()
        {
            string[] entries = new string[] { "1", "2", "3" };      //will be replaced by code to load existing card sets
            if(entries.Length == 0)
            {
                textMessage.SetText(Resource.String.title_cardSet);
                textMessage.Visibility = ViewStates.Visible;
            }
            else
            {
                textMessage.Visibility = ViewStates.Gone;
                cardSetsListView.Visibility = ViewStates.Visible;
                ArrayAdapter adapter = new ArrayAdapter<string>(this, Resource.Layout.activity_listview, entries);
                cardSetsListView.Adapter = adapter;

                //TODO add onClickListener
            }
        }
    }
}

