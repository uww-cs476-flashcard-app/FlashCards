using Acr.UserDialogs.Infrastructure;
using Android.App;
using Android.Content;
using Android.Icu.Text;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace flashCards.cs
{
    [Obsolete]

    class TimePickerFragment : AppCompatDialogFragment
    {
        //This event will be invoked when we set the date
        public event EventHandler<TimePickerDialog.TimeSetEventArgs> TimeSet = delegate { };

        //Ovverrice the constructor and create a DatePickerDialog using the current date
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            return new TimePickerDialog(Context, (sender, e) => {
                TimeSet(sender, e);
            }, DateTime.Now.Hour, DateTime.Now.Minute, true);
        }
    }
}