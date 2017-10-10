using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using RUTimeTable;
using Xamarin.Forms;

namespace RUTimetable
{
	public class DayViewModel : INotifyPropertyChanged
	{
		RealmDataBase db;
		string day = "";
		int semester = 0;
		public ICommand ViewOnCampus { get; private set; }
		public ObservableCollection<Subject> subjects;
		public ObservableCollection<Subject> Subjects
		{
			get { return subjects; }
			set
			{
				subjects = value;
				OnPropertyChanged(nameof(Subjects));
			}
		}

		//public IEnumerable<Subject> Subjects { get; private set; }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RUTimetable.DayViewModel"/> class.
		/// </summary>
		/// <param name="day">Day.</param>
		/// <param name="semester">Semester.</param>
		public DayViewModel(string day, int semester)
		{
			db = new RealmDataBase();
			var Day = db.GetDay(day, semester);
			Subjects = new ObservableCollection<Subject>() { };
			subjects = new ObservableCollection<Subject>() { };
           	this.day = day;
			this.semester = semester;
			Populate();
		}
		public int Count()
		{
			return Subjects==null?0:Subjects.ToList().Count;
		}
		/// <summary>
		/// Populate the specified day.
		/// </summary>
		/// <returns></returns>
		/// <param name="day">Day.</param>
		public void Populate()
		{
			Subjects.Clear();
			subjects.Clear();
			var dayT= db.GetDay(day, semester);
			if (dayT == null) return;
			var dayT2 = dayT.Subjects.ToList();
			for (int i = 0; i < dayT2.Count; i++)
			{
				Subjects.Add(dayT2[i]);
			}

		}

		public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		/// Ons the property changed.
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		void OnPropertyChanged(string propertyName)
		{
			//PropertyChangedEventHandler eventHandler = this.PropertyChanged;
			if (PropertyChanged != null)
		    {
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));		    }
		}
	}
}
