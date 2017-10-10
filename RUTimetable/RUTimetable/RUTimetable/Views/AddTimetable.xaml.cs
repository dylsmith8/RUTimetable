using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Acr.UserDialogs;
namespace RUTimetable
{
	public partial class AddTimetable : ContentPage
	{
		ActivityIndicator loading;
		bool IsLoading = false;
		public AddTimetable()
		{
			InitializeComponent();
			BindingContext = new AddTimetableViewModel();

		}
		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			
		}

		void Butcancel_Clicked(object sender, EventArgs e)
		{
			return;
		}
		void HandleAction()
		{

		}
		void Semester1(object sender, System.EventArgs e)
		{

		}
		void Semester2(object sender, System.EventArgs e)
		{

		}
		void Settings(object sender, System.EventArgs e)
		{

		}
		protected override void OnAppearing()
		{
			Title = "Add Timetable";
		}

	}
}
