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
using System.Collections.Generic;

namespace TestButton2
{
    [Activity(Label = "TestButton2", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        string accessToken = "e54ec36b6b139319129d8cd075cb88f095a9dce7"; //This is your Particle Cloud Access Token
        string deviceId = "28003d001847343338333633"; //This is your Particle Device Id
        string partilceFunc = "led"; //This is the name of your Particle Function
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
                    //var notification = new NotificationCompat.Builder(this)
                    //    .SetContentTitle("Button tapped")
                    //    .SetContentText($"Button tapped {count++} times!")
                    //    .SetSmallIcon(Android.Resource.Drawable.StatNotifyVoicemail)
                    //    .SetGroup("group_key_demo").Build();

                    //var manager = NotificationManagerCompat.From(this);
                    //manager.Notify(1, notification);

                    HttpClient client = new HttpClient();
                    Uri uri = new Uri("http://ipinfo.io/json");
                    string obstring = await client.GetStringAsync(uri);
                    IpInfoItemModel info = JsonConvert.DeserializeObject<IpInfoItemModel>(obstring);
                    if (count == 1)
                    {
                        LedOn();
                        button.Text = info.city;
                    }
                    else
                    {
                        LedOff();
                        button.Text = "Off";
                    }
                    count *= -1;

                    //button.Text = "Check Notification!";
                   
                };
            };
        }
        public void LedOn(string changeValue = "on")
        {
            changeValue = "on";
            //string accessToken = "e54ec36b6b139319129d8cd075cb88f095a9dce7"; //This is your Particle Cloud Access Token
            //string deviceId = "28003d001847343338333633"; //This is your Particle Device Id
            //string partilceFunc = "led"; //This is the name of your Particle Function

            HttpClient client = new HttpClient
            {
                BaseAddress =
                new Uri("https://api.particle.io/")
            };

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("access_token", accessToken),
                new KeyValuePair<string, string>("args", changeValue )
            });

            var result = client.PostAsync("v1/devices/" + deviceId + "/" + partilceFunc, content);
        }
        public void LedOff (string changeValue = "off")
        {
            changeValue = "off";
            //string accessToken = "e54ec36b6b139319129d8cd075cb88f095a9dce7"; //This is your Particle Cloud Access Token
            //string deviceId = "28003d001847343338333633"; //This is your Particle Device Id
            //string partilceFunc = "led"; //This is the name of your Particle Function

            HttpClient client = new HttpClient
            {
                BaseAddress =
                new Uri("https://api.particle.io/")
            };

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("access_token", accessToken),
                new KeyValuePair<string, string>("args", changeValue )
            });

            var result = client.PostAsync("v1/devices/" + deviceId + "/" + partilceFunc, content);
        }
    }

}


