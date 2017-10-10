using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.LocalNotifications;
using Acr.UserDialogs;
using TK.CustomMap;
using System.Threading.Tasks;
using TK.CustomMap.Api;
using TK.CustomMap.Api.Google;
using TK.CustomMap.Api.OSM;
using TK.CustomMap.Interfaces;
using TK.CustomMap.Overlays;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using TK.CustomMap.Droid;
using Plugin.Geolocator;
namespace RUTimetable.Droid
{
	[Activity(Label = "RUTimetable.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;
			base.OnCreate(bundle);
			global::Xamarin.Forms.Forms.Init(this, bundle);
			LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.book;
        	UserDialogs.Init(this);
			LoadApplication(new App(new ResourceHelper(this.ApplicationContext,"RhodesMap.geojson").ReadLocalFile()));
		}
	}
}
