using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using flashCards.cs;
using Google.Android.Material.BottomNavigation;
using Google.Android.Material.FloatingActionButton;
using AndroidX.Fragment.App;
using Acr.UserDialogs;
using Plugin.GoogleClient;

namespace flashCards
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        internal static readonly int NOTIFICATION_ID = 1000;
        internal static readonly string CHANNEL_ID = "location_notification";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            SetContentView(Resource.Layout.activity_main);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            GoogleClientManager.Initialize(this);
            CrossGoogleClient.Current.LoginAsync();
            CreateNotificationChannel();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            AndroidX.Fragment.App.Fragment selectedFragment = null;
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    selectedFragment = new HomeFragment();
                    break;
                case Resource.Id.cardSet_dashboard:
                    selectedFragment = new CardSetsFragment();
                    break;
                case Resource.Id.setting_dashboard:
                    selectedFragment = new SettingsFragment();
                    break;
            }
            this.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, selectedFragment).Commit();
            return true;
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            GoogleClientManager.OnAuthCompleted(requestCode, resultCode, data);
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var name = Resources.GetString(Resource.String.channel_name);
            var description = GetString(Resource.String.channel_description);
            var channel = new NotificationChannel(CHANNEL_ID, name, NotificationImportance.Default)
            {
                Description = description
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}

