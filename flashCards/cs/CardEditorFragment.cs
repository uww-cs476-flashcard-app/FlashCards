using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace flashCards.cs
{
    public class CardEditorFragment : AndroidX.Fragment.App.Fragment
    {
        View view;
        EditText questionEditText;
        EditText answerEditText;
        Button saveButton;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view =  inflater.Inflate(Resource.Layout.fragment_card_editor, container, false);
            questionEditText = view.FindViewById<EditText>(Resource.Id.editText1);
            answerEditText = view.FindViewById<EditText>(Resource.Id.editText2);
            saveButton = view.FindViewById<Button>(Resource.Id.button1);
            saveButton.Click += (sender, e) =>
            {
                //use CSVReader to write edited question and answer to file
                CSVReader.CSVWrite(questionEditText.Text, answerEditText.Text, Arguments.GetString("cardsetPath"));

                //return to cardlist fragment
                Activity.SupportFragmentManager.PopBackStackImmediate();
            };

            return view;
        }
    }
}