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
using System.Threading;
using nexteNews2.nextenews1;

namespace nexteNews2
{
    [Service]
    class MyServices : IntentService
    {
        protected override void OnHandleIntent(Intent intent)
        {
         
        }
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            WebServiceDB ws = new WebServiceDB();
            ws.LoginNotifyCompleted += Ws_LoginNotifyCompleted;
       

            // countine
            new Task(() =>
            {
               while(true)
                {
                    ISharedPreferences pref = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(this);
                    int lasuser = pref.GetInt("lasuser", 0);

                    ws.LoginNotifyAsync(lasuser);
                    Thread.Sleep(10000);
                  

               
                }

            }).Start();

            return StartCommandResult.Sticky;
        }

        private void Ws_LoginNotifyCompleted(object sender, LoginNotifyCompletedEventArgs e)
        {
            int lasuser = e.Result.PostID;
            if (lasuser > 0)
            {
                //Intent intents = new Intent();
                //intents.SetAction("com.alr.text");
                //intents.PutExtra("MyData", "new user with id:" + lasuser.ToString() + " his name:" + e.Result.Message);
                //ISharedPreferences pref = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(this);
                //ISharedPreferencesEditor editor = pref.Edit();
                //editor.PutInt("lasuser", lasuser);
                //editor.Apply();
                //SendBroadcast(intents);

                // Set up an intent so that tapping the notifications returns to this app:
                Intent intent = new Intent(this, typeof(MainActivity));

                // Create a PendingIntent; we're only using one PendingIntent (ID = 0):
                const int pendingIntentId = 0;
                PendingIntent pendingIntent =
                    PendingIntent.GetActivity(this, pendingIntentId, intent, PendingIntentFlags.OneShot);

                // Instantiate the builder and set notification elements, including pending intent:
                Notification.Builder builder = new Notification.Builder(this)
                    .SetContentIntent(pendingIntent)
                    .SetContentTitle("new post added")
                    .SetContentText(e.Result.Message )
                    .SetSmallIcon(Resource.Drawable.Icon);

                // Build the notification:
                Notification notification = builder.Build();

                // Get the notification manager:
                NotificationManager notificationManager =
                    GetSystemService(Context.NotificationService) as NotificationManager;

                // Publish the notification:
                const int notificationId = 0;
                notificationManager.Notify(notificationId, notification);
            }
        }
    }
}