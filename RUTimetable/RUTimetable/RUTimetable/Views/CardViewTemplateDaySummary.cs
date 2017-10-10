﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading;
using GeoJSON.Net;
using Acr.UserDialogs;

namespace RUTimetable
{
	public partial class CardViewTemplateDaySummary : ContentView
	{
		string day = "";//used for setting dayof week reminder falls in
		Subject sub;
		public CardViewTemplateDaySummary()
		{
			InitializeComponent();
		}
		/// <summary>
		/// Views Subject Venue location on Campus Map.
		/// This feature is still under development not really it works 
		/// just xamarin forms hasnt fixed the issue with google maps not
		/// working on their latest version
		/// Im till trying to move this into a ViewModel
		/// </summary>
		public async void ViewOnCampusMap(object sender, EventArgs e)
		{
			await UserDialogs.Instance.AlertAsync("Feature Disable Still under Development", "Feature disabled", "OK");
	
		}
		/// <summary>
		///Updates the Switch toggle state.
		/// Unfortunately im not yet able to bind this as a command just 
		/// But will soon as i figure it out
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void SwitchToggled(object sender, ToggledEventArgs e)
		{
			sub = new Subject { Name = Name.Text, Time = Time.Text, Period = Period.Text,Semester=DateTime.Now.Month>6?2:1 };
			var helper = new ReminderHelper();
    		day = new RealmDataBase().GetDay(sub);
			if (Switch.IsToggled)
			{
				
				helper.SetReminder(sub,day);
			}
			else
			{
				helper.RemoveReminder(sub, day);
			}
		}
		void View_Clicked(object sender, System.EventArgs e)
		{
		}

	}
}
