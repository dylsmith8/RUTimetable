﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RUTimetable
{
	public partial class DaySummary : ContentPage
	{
		DaySummaryViewModel model;
		public DaySummary()
		{
			InitializeComponent();
			 model = new DaySummaryViewModel();
			if (model.Summary.Count == 0)
			{
				var contentPage = new StackLayout
				{
					HorizontalOptions = LayoutOptions.FillAndExpand,
					VerticalOptions = LayoutOptions.CenterAndExpand,
					Orientation = StackOrientation.Vertical,
					Children = {
						new Label{
							Text="No summary for the Day Eitheir you havent added your Timetable or its the Weekend",
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

				//run every 45 mins
				Device.StartTimer(TimeSpan.FromSeconds(1), () =>
				{
					Device.BeginInvokeOnMainThread(() => RefreshUI());
					return true;
				});
		}
		private async void RefreshUI()
		{

			Device.BeginInvokeOnMainThread(() =>
			{
				model.GetSummary();
				if (model.Summary.Count== 0) return;
				Content = listView;
				BindingContext = model;

			});	
		}
		protected override void OnAppearing()
		{
			Title = "Day Summary";
			RefreshUI();	
				
		}
	}
}
