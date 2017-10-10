using Xamarin.Forms;
using RUTimeTable;
using Plugin.Notifications;
using TK.CustomMap.Api.Google;

namespace RUTimetable
{
	public class MainPageCS : TabbedPage
	{
		public MainPageCS()
		{
			InflateViews();
		}
		/// <summary>
		/// Inflates the views.
		/// N.B. The MapsMainPage will be added as soon as Xamarin fixes issue where
		/// GooglePlay services v42 works with the latest version of xamarin forms
		/// </summary>
		public void InflateViews()
		{
			Children.Clear();
			GmsDirection.Init("AIzaSyCjMY_194mgeHLsyhlPre7kZ-UVXHCCt0o");
			var navigationPage = (new NavigationPage(new AddTimetable()));
			Children.Add(new NavigationPage(new DaySummary()));
			Children.Add(navigationPage);
			Children.Add(new NavigationPage(new Monday()));
			Children.Add(new NavigationPage(new Tuesday()));
			Children.Add(new NavigationPage(new Wednesday()));
			Children.Add(new NavigationPage(new Thursday()));
			Children.Add(new NavigationPage(new Friday()));
			Children.Add(new NavigationPage(new NewSubject()));
			Children.Add(new NavigationPage(new MapMainPage()));
		}
		void Settings(object sender, System.EventArgs e)
		{

		}
		void Semester1(object sender, System.EventArgs e)
		{

		}
		void Semester2(object sender, System.EventArgs e)
		{

		}
		protected override void OnAppearing()
		{
            InflateViews();
		}

	}
}

