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


namespace POIApp
{
    public class POIListViewAdapter: BaseAdapter<PointOfInterest>
    {
        private readonly Activity context;
        private List<PointOfInterest> poiListData;

        public POIListViewAdapter(Activity _context, List<PointOfInterest> _poiListData) : base() {
            this.context = _context;
            this.poiListData = _poiListData;
        }

        public override int Count
        {
            get
            {
                return poiListData.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override PointOfInterest this[int index]
        {
            get
            {
                return poiListData[index];
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.POIListItem, null);
            }
            //Get the POI data
            PointOfInterest poi = this[position];

            //Set the text of the nameTextView to the POI's name
            view.FindViewById<TextView>(Resource.Id.nameTextView).Text = poi.Name;

            //Set the text of the addrTextView to the POI's address if it isn't empty
            if (String.IsNullOrEmpty(poi.Address))
            {
                view.FindViewById<TextView>(Resource.Id.addrTextView).Visibility = ViewStates.Gone;
            }
            else
            {
                view.FindViewById<TextView>(Resource.Id.addrTextView).Text = poi.Address;
            }

            //Get reference to ImageView
            var imageView = view.FindViewById<ImageView>(Resource.Id.poiImageView);

            if (!String.IsNullOrEmpty(poi.Image))
            {
                Koush.UrlImageViewHelper.SetUrlDrawable(imageView, poi.Image, Resource.Drawable.ic_launcher);
            }

            return view;
        }
    }
}