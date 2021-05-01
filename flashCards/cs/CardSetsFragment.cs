using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
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
    public class CardSetsFragment : AndroidX.Fragment.App.Fragment
    {
        private string CARDSETS_DIRECTORY;
        View view;
        FloatingActionButton createSetButton;
        ListView cardSetsView;

        List<string> allSets = new List<string>() { "1", "2", "3" };
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            CARDSETS_DIRECTORY = this.Context.GetExternalFilesDir(null) + "/Cardsets";
            LoadAllCardSets();
            view = inflater.Inflate(Resource.Layout.fragment_cardsets, container, false);
            createSetButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab);
            createSetButton.Click += async (sender, e) =>
            {
                PromptResult result = await UserDialogs.Instance.PromptAsync(new PromptConfig
                {
                    InputType = InputType.Name,
                    OkText = "Create",
                    CancelText = "Cancel",
                    Title = "New Cardset"
                });
                if (result.Ok && !string.IsNullOrWhiteSpace(result.Text))
                {
                    //Create new .CSV file in cardsets directory
                    try
                    {
                        FileStream fs = File.Create(CARDSETS_DIRECTORY + "/" + result.Text + ".CSV");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    allSets.Add(result.Text);
                    ShowCardSets();
                }
            };
            cardSetsView = view.FindViewById<ListView>(Resource.Id.listView1);
            cardSetsView.ItemClick += (sender, e) =>
            {
                string clickedSet = Convert.ToString(cardSetsView.GetItemAtPosition(e.Position));
                //switch to cardset editor activity
                Intent intent = new Intent(Context, typeof(CardsetEditorActivity));
                intent.PutExtra("setName", CARDSETS_DIRECTORY + "/" + clickedSet + ".csv");
                StartActivity(intent);
            };
            
            ShowCardSets();
            return view;
        }

        void LoadAllCardSets()
        {
            Directory.CreateDirectory(CARDSETS_DIRECTORY);
            //load all filenames from cardsets directory
            string[] allFiles = Directory.GetFiles(CARDSETS_DIRECTORY, "*.CSV");
            allSets.Clear();
            foreach(string setPath in allFiles)
            {
                string name = Path.GetFileName(setPath);
                allSets.Add(name.Substring(0, name.Length - 4));
            }
        }

        void ShowCardSets()
        {
            ArrayAdapter adapter = new ArrayAdapter<string>(view.Context, Resource.Layout.activity_listview, allSets);
            cardSetsView.Adapter = adapter;
        }
    }
}