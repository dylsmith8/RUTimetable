using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using Timetable.Exception;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Acr.UserDialogs;
namespace RUTimetable
{
	/// <summary>
	/// Time table getter.
	/// </summary>
	public class TimeTableGetter
	{
		RealmDataBase db;
		public bool IsBusy = false;
		public TimeTableGetter(string studentNumber)
		{
			db = new RealmDataBase();
			try
			{
				IntermediateCall(studentNumber);
			}
			catch (TimeTableException e)
			{
				UserDialogs.Instance.Alert(e.Message);
			}
		}
		public async void IntermediateCall(string s)
		{
			await GetTimetable(s);
		}
		/// <summary>
		/// Gets the time table with venues.
		/// This will be used to supply data to the Google maps
		/// </summary>
		/// <returns>The time table with venues.</returns>
		/// tp
		public async void GetTimeTableWithVenues()
		{ 
			var client = new HttpClient();
			string URL = "https://scifac.ru.ac.za/wwwtime/timetable.php";
			var values = new List<KeyValuePair<string, string>>();
			TimeTableParser temp = new TimeTableParser();
			var acc =temp.GetAcronyms();
			var formContent = new FormUrlEncodedContent(new[]
			{
				new KeyValuePair<string, string>("mnemonic[]", acc.Count>0?acc[0]:"")
            });
			var myHttpClient = new HttpClient();
			var responsefromserver = await myHttpClient.PostAsync(URL, formContent);
			client.Dispose();
			var stringContent = await responsefromserver.Content.ReadAsStringAsync();
			var html = new HtmlDocument();
			html.LoadHtml(stringContent);
			var root = html.DocumentNode;
			var nodes = root.Descendants();
			var totalNodes = nodes.Count();
		    var table = root.Descendants("table").ToArray();
			var o = 1;
		}
		/// <summary>
		/// Gets the timnetable from the time table website this is the brain of the App everything depends of on this fetching the data correctly.
		/// </summary>
		/// <param name="g"> String g is the student No passed in from the AddTimetable Page</tb>.</param>
		public async Task GetTimetable(string g)
		{				
			UserDialogs.Instance.ShowLoading("Fetching Timetable Please wait!", MaskType.Black);
			var client = new HttpClient();
			string URL = "https://scifac.ru.ac.za/timetable/personal/lookup.php";
			var formContent = new FormUrlEncodedContent(new[]
			{
				new KeyValuePair<string, string>("studentnumber", g)
			});
			IsBusy = true;
			var myHttpClient = new HttpClient();
			var responsefromserver = await myHttpClient.PostAsync(URL, formContent);
			//byte[] responseBytes = webClient.UploadValues(URL, "POST", formData);
			//string responsefromserver = Encoding.UTF8.GetString(responseBytes, 0, responseBytes.Length);
			//   textBox1.Text = responsefromserver;
			client.Dispose();
			var stringContent = await responsefromserver.Content.ReadAsStringAsync();
			var html = new HtmlDocument();
			html.LoadHtml(stringContent);
			var root = html.DocumentNode;
			var nodes = root.Descendants();
			var totalNodes = nodes.Count();
			//var Heading = root.Descendants("h1").ToArray()[1].InnerText;
			object[] arr = root.Descendants("table").ToArray();//check to see if the student is registered or not
			if (arr.Length == 0) throw new TimeTableException("Time table is currently unavailable please try again in a few days or later on today");
			var table = root.Descendants("table").ToArray()[0].InnerText;
			var Parser = new TimeTableParser(table);
			Student student = new Student();
			student = db.StoreTimetableToDataBase(Parser.GetSemester(1), 1, student);
			db.StoreTimetableToDataBase(Parser.GetSemester(2), 2,student);
		    GetTimeTableWithVenues();
			UserDialogs.Instance.HideLoading();
		}
	}
}
