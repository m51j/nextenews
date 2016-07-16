using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using nexteNews2.nextenews1;

namespace nexteNews2
{
    [Activity(Label = "Add New", Icon = "@drawable/icon", Theme = "@style/MyCustomTheme")]
    public class AddActivity : Activity
    {

        bool adding = false;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.addnewslay);

            // Get our button from the layout resource,
            // and attach an event to it

            Button button = FindViewById<Button>(Resource.Id.Addnewpost);

            button.Click += Button_Click;       
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (!adding)
            {
                adding = true;
            var TitleText = FindViewById<EditText>(Resource.Id.TitleText);
            var Authortext = FindViewById<EditText>(Resource.Id.Authortext);
            var Urltext = FindViewById<EditText>(Resource.Id.Urltext);
            var contentText = FindViewById<EditText>(Resource.Id.contentText);
                var ImageAdd = FindViewById<EditText>(Resource.Id.ImageAdd);
              WebServiceDB ws = new WebServiceDB();
                ws.AddpostCompleted += Ws_AddpostCompleted;
                ws.AddpostAsync(TitleText.Text, Authortext.Text, Urltext.Text, contentText.Text, "info@nex-te.com", ImageAdd.Text);
        }
        }

        private void Ws_AddpostCompleted(object sender, AddpostCompletedEventArgs e)
        {
            var callDialog = new AlertDialog.Builder(this);
            callDialog.SetTitle("Notify");
            callDialog.SetMessage(e.Result.Message);
            callDialog.SetNeutralButton("Ok", delegate {

            });
            callDialog.Show();
        }
    }
}

