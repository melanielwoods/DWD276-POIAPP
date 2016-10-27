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
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Android.Net;
using Newtonsoft.Json.Serialization;

namespace POIApp
{
    public class POIService
    {
        public const string GET_POIS = "http://private-e451d-poilist.apiary-mock.com/com.packt.poiapp/api/poi/pois";
        private const string CREATE_POI = "http://private-e451d-poilist.apiary-mock.com/com.packt.poiapp/api/poi/create";
        private const string DELETE_POI = "http://private-e451d-poilist.apiary-mock.com/com.packt.poiapp/api/poi/delete";

        private List<PointOfInterest> poiListData = new List<PointOfInterest>();

        public async Task<List<PointOfInterest>> GetPOIListAsync()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = null;
            response = await httpClient.GetAsync(GET_POIS);

            if (response != null)
            {
                string content = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(content);
                IList<JToken> results = jsonResponse["pois"].ToList();
                foreach (JToken token in results)
                {
                    PointOfInterest poi = token.ToObject<PointOfInterest>();
                    poiListData.Add(poi);
                }
                return poiListData;
            }
            else
            {
                return null;
            }
        }

        public bool isConnected(Context activity)
        {
            //checking to see if the device is connected to the internet
            var connectivityManager = (ConnectivityManager)activity.GetSystemService(Context.ConnectivityService);
            var activeConnection = connectivityManager.ActiveNetworkInfo;
            var actuallyConnected = (activeConnection != null && activeConnection.IsConnected);
            return actuallyConnected;
        }

        //convert all json keys into lowercase. Make sure this code is always at the bottom
        public class POIContractResolver:DefaultContractResolver
        {
            protected override string ResolvePropertyName(string key)
            {
                return key.ToLower();

            }
        }
    }
}