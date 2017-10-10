using Xamarin.Forms;
using RUTimeTable;
using Plugin.Notifications;

namespace RUTimetable
{
	public partial class App : Application
	{
		public App(GeoJSONData dat)
		{
			InitializeComponent();
			var temp =new GEOJSONTOJSONParser(dat);//the Dat file sent by each platform to the parent App
			temp.Process();
			MainPage = new MainPageCS();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
