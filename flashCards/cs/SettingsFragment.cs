using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace flashCards.cs
{
    public class SettingsFragment : AndroidX.Fragment.App.Fragment
    {
        Button startTimeButton;
        Button endTimeButton;
        Button createTestButton;
        TextView startTime;
        TextView endTime;
       
        View view;

        public static readonly string TAG = "MyTimePickerFragment";
        Action<DateTime> timeSelectedHandler = delegate { };

        [Obsolete]
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.fragment_settings, container, false);


            startTimeButton = view.FindViewById<Button>(Resource.Id.start_button);
            endTimeButton = view.FindViewById<Button>(Resource.Id.end_button);
            createTestButton = view.FindViewById<Button>(Resource.Id.test_button);
            startTime = view.FindViewById<TextView>(Resource.Id.start_time_display);
            endTime = view.FindViewById<TextView>(Resource.Id.end_time_display);


            createTestButton.Click += async (sender, e) =>
            {
                ShowNotification();
            };

            startTimeButton.Click += async (sender, e) =>
            {
                var timePicker = new TimePickerFragment();
                //Attach an event to the fragment
                timePicker.TimeSet += (sender, e) => {
                    //Show the date on the console
                    startTime.Text  = ($"{e.HourOfDay}:{e.Minute}");
                };
                //Show the date picker
                //Needs a Support Fragment managing the lifecycle of the fragment
               

                timePicker.Show(FragmentManager, "timePicker");
            };

            endTimeButton.Click += async (sender, e) =>
            {
                var timePicker = new TimePickerFragment();
                //Attach an event to the fragment
                timePicker.TimeSet += (sender, e) => {
                    //Show the date on the console
                    endTime.Text = ($"{e.HourOfDay}:{e.Minute}");
                };
                //Show the date picker
                //Needs a Support Fragment managing the lifecycle of the fragment


                timePicker.Show(FragmentManager, "timePicker");
            };

            return view;
        }

        void ShowNotification()
        {
            // Build the notification:
            var builder = new NotificationCompat.Builder(this.Activity, MainActivity.CHANNEL_ID)
                          .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                          .SetContentTitle("FlashCard Notification") // Set the title
                          .SetSmallIcon(Resource.Drawable.ic_home_black_24dp) // This is the icon to display
                          .SetContentText($"Test button notification"); // the message to display.

            // Finally, publish the notification:
            var notificationManager = NotificationManagerCompat.From(this.Activity);
            notificationManager.Notify(MainActivity.NOTIFICATION_ID, builder.Build());
        }
    }



}