using System;
using System.Net.Http;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.Wearable.Views;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestButton2
{
    [Activity(Label = "TestButton2", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var v = FindViewById<WatchViewStub>(Resource.Id.watch_view_stub);
            v.LayoutInflated += delegate
            {

                // Get our button from the layout resource,
                // and attach an event to it
                Button button = FindViewById<Button>(Resource.Id.myButton);

                button.Click += async delegate
                {
                    var notification = new NotificationCompat.Builder(this)
                        .SetContentTitle("Button tapped")
                        .SetContentText($"Button tapped {count++} times!")
                        .SetSmallIcon(Android.Resource.Drawable.StatNotifyVoicemail)
                        .SetGroup("group_key_demo").Build();

                    var manager = NotificationManagerCompat.From(this);
                    manager.Notify(1, notification);

                    HttpClient client = new HttpClient();
                    Uri uri = new Uri("http://ipinfo.io/json");
                    string obstring = await client.GetStringAsync(uri);
                    IpInfoItemModel info = JsonConvert.DeserializeObject<IpInfoItemModel>(obstring);

                    button.Text = "Check Notification!";
                    button.Text = info.city;
                };
            };
        }
    }
}


