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
using Google.Android.Material.FloatingActionButton;

namespace flashCards.cs
{
    public class CardListFragment : AndroidX.Fragment.App.Fragment
    {
        View view;
        ListView cardsList;
        List<FlashCard> flashCards;
        FloatingActionButton newCardButton;
        string cardsetPath;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.fragment_cardlist, container, false);
            cardsList = view.FindViewById<ListView>(Resource.Id.listView1);
            newCardButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab);
            cardsetPath = Arguments.GetString("cardsetPath");
            LoadAllCards();
            ShowAllCards();

            cardsList.ItemClick += (sender, e) =>
            {
                //switch to cardeditor fragment
                AndroidX.Fragment.App.Fragment cardEditorFrag = new CardEditorFragment();
                Bundle bundle = new Bundle();
                bundle.PutString("cardsetPath", cardsetPath);
                bundle.PutInt("lineNum", e.Position);
                cardEditorFrag.Arguments = bundle;
                Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, cardEditorFrag).AddToBackStack(null).Commit();
            };

            newCardButton.Click += (sender, e) =>
            {
                //switch to cardeditor fragment
                AndroidX.Fragment.App.Fragment cardEditorFrag = new CardEditorFragment();
                Bundle bundle = new Bundle();
                bundle.PutString("cardsetPath", cardsetPath);
                cardEditorFrag.Arguments = bundle;
                Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, cardEditorFrag).AddToBackStack(null).Commit();
            };

            return view;
        }

        void LoadAllCards()
        {
            //use csv reader to load cards into flashcards list
            flashCards = CSVReader.CSVRead(cardsetPath);
            Console.WriteLine(cardsetPath);
        }

        void ShowAllCards()
        {
            //display questions in listview
            string[] questions = new string[flashCards.Count];
            for(int i = 0; i < flashCards.Count; i++) { questions[i] = flashCards[i].Question; }
            ArrayAdapter adapter = new ArrayAdapter<string>(view.Context, Resource.Layout.activity_listview, questions);
            cardsList.Adapter = adapter;
        }
    }

    public struct FlashCard
    {
        public string Question;
        public string Answer;
    }
}