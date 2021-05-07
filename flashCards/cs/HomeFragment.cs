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

namespace flashCards.cs
{
    public class HomeFragment : AndroidX.Fragment.App.Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_home, container, false);

            ShowNotification();

            return view;
        }
        void ShowNotification()
        {
            // Build the notification:
            var builder = new NotificationCompat.Builder(this.Activity, MainActivity.CHANNEL_ID)
                          .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                          .SetContentTitle("FlashCard Notification") // Set the title
                          .SetSmallIcon(Resource.Drawable.ic_home_black_24dp) // This is the icon to display
                          .SetContentText($"Notifcation from FlashCard App."); // the message to display.

            // Finally, publish the notification:
            var notificationManager = NotificationManagerCompat.From(this.Activity);
            notificationManager.Notify(MainActivity.NOTIFICATION_ID, builder.Build());
        }
    }
}