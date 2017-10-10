using System;
using System.Collections.Generic;
using System.Linq;
using Realms;
using RUTimetable;
using Timetable;
namespace RUTimetable
{
	public class RealmDataBase
	{

		Student student;
		Realm db;
		public bool LoadedSemester1 = false;
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RealmDataBase"/> class.
		/// </summary>
		public RealmDataBase()
		{
			student = new Student();
			try
			{
				db = Realm.GetInstance();//open realm db
			}
			catch (Realms.Exceptions.RealmMigrationNeededException)
			{

			}
			catch (Exception e)
			{
				
			}
		}


		/// <summary>
		/// Gets the student.
		/// </summary>
		/// <returns>The student.</returns>
		public Student GetStudent()
		{
			return db.All<Student>().ToList().Count > 0 ? db.All<Student>().ToList()[0] : null;
		}
		/// <summary>
		/// Stores the timetable to the database.
		/// </summary>
		/// <param name="Semester">Semester.</param>
		/// <param name="WhichSemester">Which semester.</param>
		public Student StoreTimetableToDataBase(Dictionary<string, List<Subject>> Semester, int WhichSemester, Student Student)
		{
			var check = db.All<Student>().ToList();
			db.Write(() =>
			 {	
				if(Student.WeekSemester1.Count==0)db.RemoveAll();

				 //check if there is an exising timetable if yea delete it but before deleting 	
			//	 if (check.Count() > 1)
				 {
				//	RemoveAllLeaveOne();//dont know why imdoing this :(
				 }
				 int dayCounter = 0;
				 foreach (var dictionary in Semester)
				 {
					 string day = dictionary.Key;//get the day we currently looking at i.e Monday
					 dayCounter++;
					 Day temp = new Day();

					 temp.day = day;
					for (int i = 0;i < dictionary.Value.Count;i++)
					{
						temp.Subjects.Add(dictionary.Value.ToList()[i]);
					}
					 temp.Semester = WhichSemester;
					 if (WhichSemester == 1) Student.WeekSemester1.Add(temp);
					 else Student.WeekSemester2.Add(temp);
				 }
				 db.Add(Student, true);
			 });
			db.Refresh();
			return Student;
		}

		public void RemoveAllLeaveOne()
		{
			var all = db.All<Student>().ToList();
			for (int i = 1; i < all.Count; i++)
			{
				db.Write(() => {
					db.Remove(all[i]);
				});
			}
		}

		/// <summary>
		/// Gets the day.
		/// </summary>
		/// <returns>The day.</returns>
		/// <param name="day">Day.</param>
		/// <param name="semester">Semester.78</param>
		public Day GetDay(string day, int semester)
		{
			var students = db.All<Student>().ToList();
			if (students.ToList().Count > 0)
			{
				bool boolean =students[0].WeekSemester1.ToList().Count >0 && students[0].WeekSemester2.ToList().Count >0;
				if (boolean && (day != "Saturday" && day != "Sunday"))//Some how this always evaluates to true so i have to negate the statement
				{
					var Day = students.ToList()[0];
					return semester == 1 ? Day.WeekSemester1[new DayConverter().ConvertDayToInt(day)] : Day.WeekSemester2[new DayConverter().ConvertDayToInt(day)];
				}
			}	
			return null;
		}
		/// <summary>
		/// Sets the reminder.
		/// </summary>
		/// <param name="reminder">Reminder.</param>
		public void SetReminder(SubjectReminder reminder)
		{
			db.Write(() => {
			 student = db.All<Student>().ToList()[0];
				student.Reminders.ToList().Add(reminder); 
			});
		}
		public IEnumerable<Subject> GetSubjects(string day, int semester)
		{
			var temp = db.All<Student>().ElementAt(0);
			return (semester == 1 ? temp.WeekSemester1.ElementAt(new DayConverter().ConvertDayToInt(day)).Subjects : temp.WeekSemester2.ElementAt(new DayConverter().ConvertDayToInt(day)).Subjects);

		}
		/// <summary>
		/// Removes the reminder.
		/// </summary>
		/// <param name="reminder">Reminder.</param>
		public void RemoveReminder(SubjectReminder reminder)
		{
			db.Write(() =>
			{
				 student = db.All<Student>().ToList()[0];
				bool removed =student.Reminders.ToList().Remove(reminder);
			});
			db.Refresh();
		}
		/// <summary>
		/// Gets the day.
		/// </summary>
		/// <returns>The day.</returns>
		/// <param name="s">S.</param>
		public string GetDay(Subject s)
		{
			var temp = "";
			student = db.All<Student>().ToList()[0];
				var s1 = student.WeekSemester1;
				var s2 = student.WeekSemester2;
				for (int i = 0; i < s1.Count; i++)
				{
					var day = s1[i];
					for (int a = 0; a < day.Subjects.Count; a++)
					{
					var subject = day.Subjects.ToList()[a];
					var name=subject.Name;
					var Period = subject.Period;
					var Time = subject.Time;
					var semester = subject.Semester;
					if (name == s.Name && Period == s.Period && Time == s.Time && semester == s.Semester) return new DayConverter().ConvertToDayOfWeek(day.day).ToString();
					}

			     }
				for (int i = 0; i<s2.Count; i++)
				{
					var day = s2[i];
					for (int a = 0; a<day.Subjects.Count; a++)
					{
					var subject = day.Subjects.ToList()[a];
					var name = subject.Name;
					var Period = subject.Period;
					var Time = subject.Time;
					var semester = subject.Semester;
					if (name == s.Name && Period == s.Period && Time == s.Time && semester == s.Semester) return new DayConverter().ConvertToDayOfWeek(day.day).ToString();
					}
			     }
			return "";

		}
		/// <summary>
		/// Gets the state of the reminder.
		/// </summary>
		/// <returns><c>true</c>, if reminder state was gotten, <c>false</c> otherwise.</returns>
		/// <param name="s">S.</param>
		public bool GetReminderState(SubjectReminder s)
		{
			var subject = s.Subject;
			 student = db.All<Student>().ToList()[0];
			var reminders = student.Reminders;
			var state = false;
			if (reminders == null) return state;
			for (int i = 0; i < reminders.Count; i++)
			{
				var sub = reminders[i].Subject;
				if (sub.Name == subject.Name && sub.Period == subject.Period && sub.Time == subject.Time && s.Semester == reminders[i].Semester && s.DayOfWeek == reminders[i].DayOfWeek)
				{
					state= reminders[i].OnOf;
				}
			}
			return state;
		}
		/// <summary>
		/// Gets the reminders.
		/// </summary>
		/// <returns>The reminders.</returns>
		public List<SubjectReminder> GetReminders()
		{
			var students = db.All<Student>().ToList();
			if (student == null) return null;
			return students[0].Reminders.ToList();
		}
		/// <summary>
		/// Create a new Subject Object with  the specified name, period, time and semester.
		/// </summary>
		/// <returns>The write.</returns>
		/// <param name="name">Name.</param>
		/// <param name="period">Period.</param>
		/// <param name="time">Time.</param>
		/// <param name="semester">Semester.</param>
		public void Write(string name, string period, string time, int semester,string day)
		{
			db.Write(() => {
			 student = db.All<Student>().ToList()[0];
				var sub = new Subject { Name=name,Period="Period: "+period,Time=time,Semester=semester};
				var temp = GetDay(day, semester);
				temp.Subjects.Add(sub);
			});
		}
		/// <summary>
		/// Write the specified subject.
		/// </summary>
		/// <returns>The write.</returns>
		/// <param name="sub">Sub.</param>
		public void Write(Subject sub,string day,int semester)
		{
			db.Write(() => {
				var stdnt = db.All<Student>().ToList()[0];
				var smster = semester == 1 ? stdnt.WeekSemester1.ToList() : stdnt.WeekSemester2.ToList();
				smster[new DayConverter().ConvertDayToInt(day)].Subjects.Add(sub);
			});
		}

		/// <summary>
		/// Gets the student subject list.
		/// </summary>
		/// <returns>The student subjects list.</returns>
		public List<List<Day>> GetStudentSubjectLists()
		{
			var myList = new List<List<Day>>();
			var student = db.All<Student>().ToList()[0];
			myList.Add(student.WeekSemester1.ToList());
			myList.Add(student.WeekSemester2.ToList());
			return myList;
		}
		/// <summary>
		/// Stores all the Lecture Venue locations on Rhodes Campus.
		/// </summary>
		/// <param name="locs">Locs.</param>
		public void StoreVenueLocations(List<VenueLocation> locs)
		{
			db.Write(() => {
				var loc =db.All<VenueData>().ToList();
				if (loc.Count == 0) //first time app is used create a new VenueData object
				{
					var VenueD = new VenueData();
					for (int i = 0; i < locs.Count; i++) VenueD.Venues.Add(locs[i]);
					db.Add(VenueD);
					db.Refresh();
				}
				else
				{
					var temp = loc[0];
					temp.Venues.Clear();//clear the current list and add the updated venues
					for (int i = 0; i<locs.Count; i++) temp.Venues.Add(locs[i]);
					db.Add(temp);
					db.Refresh();
				}
			});
		}
		/// <summary>
		/// Stores semester user Clicked on on the ToolBarMenutItems.
		/// This will then be picked up by the other Pages and UI updated Accordingly
		/// </summary>
		/// <param name="semester">Semester.</param>
		public void SemesterToLoad(int semester)
		{
			db.Write(() => {
				db.RemoveAll<Semester>();
				db.Add(new Semester { semester=semester});
				db.Refresh();
			});
		}
		/// <summary>
		/// Gets the semester to load.
		/// </summary>
		/// <returns>The semester to load.</returns>
		public Semester GetSemesterToLoad()
		{
			var sem = new Semester();
			db.Write(() => {
				var all = db.All<Semester>().ToList();
				if (all.Count > 0) sem = all[0];
				db.Refresh();
			});
			return sem;
		}
	}

}
