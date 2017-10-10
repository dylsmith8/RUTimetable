using System;
using System.Collections.Generic;
using Plugin.LocalNotifications;
using Plugin.Notifications;
using Xamarin.Forms;
namespace RUTimetable
{
	public class NotificationHelper 
	{
		public NotificationHelper()
		{
		}
		public void ScheduleNotification(SubjectReminder s)
		{
			var Dayprovider = new DayProvider();
			var dayconverter = new DayConverter();
			var message = "Your Lecture is starting shortly!";
			var message1 = "Your Practical is starting shortly!";
			var message2 = "Your tutorial is starting shortly!";
			var message3 = "The tutorial/practical you tutor is starting shortly!";
			if (DateTime.Today.DayOfWeek.ToString().Split(',')[0] == s.DayOfWeek)
			{
				var time = s.Subject.Time.Split(':');
				DateTime nextday = Dayprovider.GetNextWeekday(DateTime.Today, dayconverter.ConvertToDayOfWeek(s.DayOfWeek));
				DateTime newnextday = new DateTime(nextday.Year, nextday.Month, nextday.Day, Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2]));
				if (s.Subject.Name.Contains("TUTOR"))
				{
					CrossLocalNotifications.Current.Show(s.Subject.Name, message3, s.ReminderID, newnextday);
				}
				if (s.Subject.Name.Contains("PRACTICAL"))
				{
					CrossLocalNotifications.Current.Show(s.Subject.Name, message1, s.ReminderID, newnextday);
				}
				if (s.Subject.Name.Contains("TUTORIAL"))
				{
					CrossLocalNotifications.Current.Show(s.Subject.Name, message2, s.ReminderID, newnextday);
				}
				if (s.Subject.Name.Contains("LECTURE"))
				{
					CrossLocalNotifications.Current.Show(s.Subject.Name, message, s.ReminderID, newnextday);
				}
			}
			else
			{
				var time = s.Subject.Time.Split(':');
				DateTime nextday = Dayprovider.GetNextWeekday(DateTime.Today.AddDays(1), dayconverter.ConvertToDayOfWeek(s.DayOfWeek));
				DateTime newnextday = new DateTime(nextday.Year, nextday.Month, nextday.Day, Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2]));
				if (s.Subject.Name.Contains("TUTOR"))
				{
					CrossLocalNotifications.Current.Show(s.Subject.Name, message3, s.ReminderID, newnextday);
				}
				if (s.Subject.Name.Contains("PRACTICAL"))
				{
					CrossLocalNotifications.Current.Show(s.Subject.Name, message1, s.ReminderID, newnextday);
				}
				if (s.Subject.Name.Contains("TUTORIAL"))
				{
					CrossLocalNotifications.Current.Show(s.Subject.Name, message2, s.ReminderID, newnextday);
				}
				if (s.Subject.Name.Contains("LECTURE"))
				{
					CrossLocalNotifications.Current.Show(s.Subject.Name, message, s.ReminderID, newnextday);
				}
			}
		}
		public void CancelNotification(SubjectReminder s)
		{
			CrossLocalNotifications.Current.Cancel(s.ReminderID);
		}



	}
}
