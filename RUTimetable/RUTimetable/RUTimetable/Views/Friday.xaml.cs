using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RUTimetable
{
	public partial class Friday : ContentPage
	{
		DayViewModel model;

		int semester = 1;
		SemesterChangeHandler ChangeHandler;
		public Friday()
		{
			InitializeComponent();
			model = new DayViewModel("Friday", DateTime.UtcNow.Month>6?2:1);
			ChangeHandler = new SemesterChangeHandler();
			if (model.Count() == 0)
			{
				var contentPage = new StackLayout
				{
					HorizontalOptions = LayoutOptions.FillAndExpand,
					VerticalOptions = LayoutOptions.CenterAndExpand,
					Orientation = StackOrientation.Vertical,
					Children = {
						new Label{
							Text="No Time table Yet or You are a Postgrad student",
							VerticalTextAlignment=TextAlignment.Center,
							HorizontalTextAlignment=TextAlignment.Center,
							VerticalOptions=LayoutOptions.FillAndExpand,
							HorizontalOptions=LayoutOptions.CenterAndExpand
						}
					}

				};
				Content = contentPage;
			}
			else
			{
				BindingContext = model;
			}
			Device.StartTimer(TimeSpan.FromSeconds(1), () =>{
			Device.BeginInvokeOnMainThread(() => RefreshUI());
			return true; 
			});
		}
			private async void RefreshUI()
			{

				Device.BeginInvokeOnMainThread(() =>
				{

					var anythingToload = ChangeHandler.GetSemester();
					if (anythingToload.semester > 0)
					{
						model = new DayViewModel("Friday", anythingToload.semester);
						Content = listView;
						BindingContext = model;
						return;
					}
					model.Populate();
					if (model.Count() == 0) return;
					Content = listView;
					BindingContext = model;

				});
			}
			void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
			{
				var s = listView.SelectedItem;
				var test = s as CardViewTemplate;
				//test.BackgroundColor = Color.White;
			}
			/// <summary>
			/// Semester1 the specified sender and e.
			/// </summary>
			/// <returns>The semester1.</returns>
			/// <param name="sender">Sender.</param>
			/// <param name="e">E.</param>
			void Semester1(object sender, System.EventArgs e)
			{
				ChangeHandler.CommunicateWithOtherTabs(1);
			}
			/// <summary>
			/// Semester2 the specified sender and e.
			/// </summary>
			/// <returns>The semester2.</returns>
			/// <param name="sender">Sender.</param>
			/// <param name="e">E.</param>
			void Semester2(object sender, System.EventArgs e)
			{
			ChangeHandler.CommunicateWithOtherTabs(2);
		    }
			/// <summary>
			/// Settings the specified sender and e.
			/// </summary>
			/// <returns>The settings.</returns>
			/// <param name="sender">Sender.</param>
			/// <param name="e">E.</param>
			void Settings(object sender, System.EventArgs e)
			{	
				
		    }
			protected override void OnAppearing()
			{
				Title = "Friday";
				RefreshUI();	
			}
	}
}
