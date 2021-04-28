using System;
using System.Collections.Generic;
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
        View view;
        FloatingActionButton newSetMenu;
        FloatingActionButton createSetButton;
        FloatingActionButton importSetButton;
        ListView cardSetsView;

        List<string> allSets = new List<string>() { "1", "2", "3" };
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.fragment_cardsets, container, false);
            newSetMenu = view.FindViewById<FloatingActionButton>(Resource.Id.fab);
            newSetMenu.Click += (sender, e) => {ShowActionMenu();};
            createSetButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab1);
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
                    allSets.Add(result.Text);
                    ShowCardSets();
                }
            };
            importSetButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab2);
            cardSetsView = view.FindViewById<ListView>(Resource.Id.listView1);
            cardSetsView.ItemClick += (sender, e) =>
            {
                //TODO implement cardset editor
            };
            
            ShowCardSets();
            return view;
        }

        void ShowCardSets()
        {
            ArrayAdapter adapter = new ArrayAdapter<string>(view.Context, Resource.Layout.activity_listview, allSets);
            cardSetsView.Adapter = adapter;
        }

        void ShowActionMenu()
        {
            if(createSetButton.Visibility == ViewStates.Invisible)
            {
                createSetButton.SetVisibility(ViewStates.Visible);
                importSetButton.SetVisibility(ViewStates.Visible);
            }
            else
            {
                createSetButton.SetVisibility(ViewStates.Invisible);
                importSetButton.SetVisibility(ViewStates.Invisible);
            }
        }
    }
}