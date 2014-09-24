using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views.InputMethods;
using Android.Widget;
using ChatClient.Shared;

namespace ChatClient.Droid
{
    [Activity(Label = "Chat Client", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            var client = new Client("Android");

            var input = FindViewById<EditText>(Resource.Id.Input);
            var messages = FindViewById<ListView>(Resource.Id.Messages);

            var inputManager = (InputMethodManager)GetSystemService(InputMethodService);
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string>());

            messages.Adapter = adapter;

            await client.Connect();

            input.EditorAction +=
              delegate
              {
                  inputManager.HideSoftInputFromWindow(input.WindowToken, HideSoftInputFlags.None);

                  if (string.IsNullOrEmpty(input.Text))
                      return;

                  client.Send(input.Text);

                  input.Text = "";
              };

            client.OnMessageReceived +=
              (sender, message) => RunOnUiThread(() =>
                adapter.Add(message));
        }
    }
}
