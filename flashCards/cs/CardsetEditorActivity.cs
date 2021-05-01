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
using AndroidX.AppCompat.App;
using AndroidX.Fragment.App;

namespace flashCards.cs
{
    [Activity(Label = "activity_cardset_editor")]
    public class CardsetEditorActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_cardset_editor);
            // Create your application here
            string setName = Intent.GetStringExtra("setName");

            //initially display cardlist fragment
            AndroidX.Fragment.App.Fragment selectedFragment = new CardListFragment();
            Bundle bundle = new Bundle();
            bundle.PutString("cardsetPath", setName);
            selectedFragment.Arguments = bundle;
            this.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, selectedFragment).Commit();
        }
    }
}