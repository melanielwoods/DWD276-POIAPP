using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace POIApp
{
    [Activity(Label = "POIApp", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class POIListActivity : Activity
    {
        private ListView poiListView;
        private ProgressBar progressBar;
        private List<PointOfInterest> poiListData;
        private POIListViewAdapter listAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.POIList);
            poiListView = FindViewById<ListView>(Resource.Id.poiListView);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            DownloadPoisListAsynch(); //call the method below
        }

        public async void DownloadPoisListAsynch()
        {
            progressBar.Visibility = Android.Views.ViewStates.Visible;
            poiListData = GetPoisListTestData();
            progressBar.Visibility = Android.Views.ViewStates.Gone;
            listAdapter = new POIListViewAdapter(this, poiListData);
            poiListView.Adapter = listAdapter;
        }

        private List<PointOfInterest> GetPoisListTestData()
        {
            List<PointOfInterest> listData = new List<PointOfInterest>();
            for (int i = 0; 1 <20; i++)
            {
                PointOfInterest poi = new PointOfInterest();
                poi.ID = i;
                poi.Name = "Name " + i;
                poi.Address = "Address " + i;
                listData.Add(poi);
            }
            return listData;
        }
    }
}

