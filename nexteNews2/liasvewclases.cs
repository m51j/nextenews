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
using Android.Media;
using Android.Graphics;
using System.Net;

namespace nexteNews2
{
    public class HomeScreenAdapter : BaseAdapter<TableItem>
    {
        System.Collections.Generic.List<TableItem> items;
        Activity context;
        public HomeScreenAdapter(Activity context, System.Collections.Generic.List<TableItem> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override TableItem this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.listviewLayout, null);
            view.FindViewById<TextView>(Resource.Id.textTitle).Text = item.textTitle;
            view.FindViewById<TextView>(Resource.Id.textAuthor).Text = item.textAuthor;
            view.FindViewById<TextView>(Resource.Id.textContanit).Text = item.textContanit;
            view.FindViewById<TextView>(Resource.Id.textDate).Text = item.textDate;


            //view.FindViewById<TextView>(Resource.Id.Text2).Text = item.SubHeading;
            //var im1 = view.FindViewById<ImageView>(Resource.Id.Image);
            ImageView imgv = view.FindViewById<ImageView>(Resource.Id.imageURL);

      var imageBitmap = GetImageBitmapFromUrl(item .imagetext );
            imgv.SetImageBitmap(imageBitmap);

                Button Readmoreb = view.FindViewById<Button>(Resource.Id.buttonRadMore);
            Readmoreb.Click += delegate {
                  Android.Net.Uri uriphone = Android.Net.Uri.Parse(item.Urltext);
                   Intent calln = new Intent(Intent.ActionView, uriphone);
                context.StartActivity(calln);
            };
            return view;
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;
            if (!(url == "null"))
                using (var webClient = new WebClient())
                {
                    try
                    {
                        var imageBytes = webClient.DownloadData(url);
                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                        }
                    }
                    catch
                    {

                    }

                  
                }

            return imageBitmap;
        }
    }
    public class TableItem
    {
   
        public string textTitle;
        public string textAuthor;
        public string Urltext;
        public string textContanit;
        public string imagetext;
        public string textDate;
        public TableItem(string textTitle, string textAuthor, string Urltext, string textContanit, string imagetext, string textDate)
        {
            this.textTitle = textTitle;
            this.textAuthor = textAuthor;
            this.Urltext = Urltext;
            this.textContanit = textContanit;
            this.imagetext = imagetext;
            this.textDate = textDate;
        }
    }
}