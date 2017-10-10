using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;
using System.Threading.Tasks;
namespace RUTimetable
{
	public partial class UpdateSubjectTaken : ContentPage
	{
		int semester = 0,period=0;
		string day = "";
		public UpdateSubjectTaken()
		{
			InitializeComponent();
			Semester.ItemsSource = MiscellaneousProvider.SemesterProvider;
			Period.ItemsSource = MiscellaneousProvider.PeriodProvider;
			Day.ItemsSource = MiscellaneousProvider.Days;
		}
		void Handle_Click_Period(object sender, EventArgs e)
		{
			var picker = (Picker)sender;
			int selectedIndex = picker.SelectedIndex;
			if (selectedIndex != -1)
			{
				period = Convert.ToInt32(picker.ItemsSource[selectedIndex]);
			}

		}

		void Handle_Click_Semester(object sender, EventArgs e)
		{
			var picker = (Picker)sender;
			int selectedIndex = picker.SelectedIndex;
			if (selectedIndex != -1)
			{
				semester = Convert.ToInt32(picker.ItemsSource[selectedIndex]);
			}

		}

		//
		void Handle_Click_Day(object sender, EventArgs e)
		{
			var picker = (Picker)sender;
			int selectedIndex = picker.SelectedIndex;
			if (selectedIndex != -1)
			{
				day = picker.ItemsSource[selectedIndex].ToString();
			}

		}

		void HandleAction()
		{ 
		}

		//
		public async Task Loading()
		{
			Random rnd = new Random();
			int wait1 = rnd.Next(5000, 10000);
			await Task.Delay(wait1);
			int wait2 = rnd.Next(5000, 10000);
			await Task.Delay(wait2);
		}


		async void Save_Clicked(object sender, System.EventArgs e)
		{
			RealmDataBase db = new RealmDataBase();
			var student = db.GetStudent();
			if (student != null && !string.IsNullOrEmpty(Subject.Text) && !string.IsNullOrEmpty(day) && period > 0 && semester > 0)
			{
				UserDialogs.Instance.ShowLoading("Saving Please Wait", MaskType.Black);
				await Loading();
				db.Write(Subject.Text, period.ToString(), new TimeProvider().GetPeriodTime(period.ToString()), semester, day);
				UserDialogs.Instance.HideLoading();
			}

			else
			{
				await DisplayAlert("Nigga is you dumb?", "Nigga is you dumb? fill all the required information yesses!!", "Cancel");
			}		}
	}
}
