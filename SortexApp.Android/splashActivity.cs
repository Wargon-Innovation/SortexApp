using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortexApp.Droid
{
    [Activity(Label = "Sortex", MainLauncher = true, Theme = "@style/MainTheme.Splash", NoHistory = true, Icon = "@mipmap/sortexlogo")]
    public class splashActivity : AppCompatActivity
    {
      
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
           
            // Create your application here
        }
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartupAsync(); });
            startupWork.Start();
        }

        async void SimulateStartupAsync()
        {
            
            await Task.Delay(500); // Simulate a bit of startup work.
           

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
          
        }
    }
}