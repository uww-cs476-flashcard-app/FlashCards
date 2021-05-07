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

namespace flashCards.cs
{
    public class ExpandableListAdapter : BaseExpandableListAdapter
    {
        Dictionary<string, List<string>> dictGroup = null;
        List<string> lstGroupID = null;
        Activity activity;

        public ExpandableListAdapter(Activity _activity, Dictionary<string, List<string>> _dictGroup)
        {
            dictGroup = _dictGroup;
            activity = _activity;
            lstGroupID = dictGroup.Keys.ToList();

        }

        public override int GroupCount
        {
            get
            {
                return dictGroup.Count;
            }
        }

        public override bool HasStableIds
        {
            get
            {
                return false;
            }
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return dictGroup[lstGroupID[groupPosition]][childPosition];
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return dictGroup[lstGroupID[groupPosition]].Count;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            var item = dictGroup[lstGroupID[groupPosition]][childPosition];

            if (convertView == null)
                convertView = activity.LayoutInflater.Inflate(Resource.Layout.list_item, null);

            var textBox = convertView.FindViewById<TextView>(Resource.Id.expandedListItem);
            textBox.SetText(item, TextView.BufferType.Normal);

            return convertView;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return lstGroupID[groupPosition];
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            var item = lstGroupID[groupPosition];

            if (convertView == null)
                convertView = activity.LayoutInflater.Inflate(Resource.Layout.list_group, null);

            var textBox = convertView.FindViewById<TextView>(Resource.Id.listTitle);
            textBox.SetText(item, TextView.BufferType.Normal);

            return convertView;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }
}