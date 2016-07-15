using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;

namespace nexteNews2
{
    [Activity(Label = "NexteNews", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyCustomTheme")]
    public class MainActivity : Activity
    {
        int count = 1;
        System.Collections.Generic.List<TableItem> tableItems = new System.Collections.Generic.List<TableItem>();
        HomeScreenAdapter adp;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            StartService(new Intent(this, typeof(MyServices)));
            // Get our button from the layout resource,
            // and attach an event to it




            //nexteNews2.WebServiceDB ws = new nexteNews2.WebServiceDB();
            //ws.GetAllDataCompleted += Ws_GetAllDataCompleted;
            //ws.GetAllDataAsync();
        }
        protected override void OnResume()
        {
            base.OnResume();
            nexteNews21.WebServiceDB ws = new nexteNews21.WebServiceDB();
            ws.GetAllDataCompleted += Ws_GetAllDataCompleted;
            ws.GetAllDataAsync();
        }
        private void Ws_GetAllDataCompleted(object sender, nexteNews21.GetAllDataCompletedEventArgs e)
        {
            tableItems.Clear();
            foreach (nexteNews21.ReturnData2 item in e.Result)
            {
                tableItems.Add(new TableItem(item.Title , item.Author , item.URL , item.Content , item.img , item.date.ToShortDateString() + "  " + item.date.ToShortTimeString()));

            }
            ListView listView = FindViewById<ListView>(Resource.Id.listView1);
            adp = new HomeScreenAdapter(this, tableItems);
            listView.Adapter = adp;
        }
 
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
      
            switch (item.ItemId)
            {
                case Resource.Id.addnews:
                    Intent scnd = new Intent(this, typeof(AddActivity));
             
                    StartActivity(scnd);
                    break;
           


            }
            return base.OnOptionsItemSelected(item);
        }
    }
}

