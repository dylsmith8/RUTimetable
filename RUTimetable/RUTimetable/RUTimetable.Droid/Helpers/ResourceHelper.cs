using System;
using System.IO;
using Android.Content;
using Android.Content.Res;
using Android.Media;

namespace RUTimetable.Droid
{
	public class ResourceHelper
	{
		Context context;
		string FileName;
		public ResourceHelper(Context ctx,string FileName)
		{
			context = ctx;
			this.FileName = FileName;
		}
		/// <summary>
		/// Reads the local file.
		/// </summary>
		/// <returns>GeoJSONData in raw format.</returns>
		public GeoJSONData ReadLocalFile()
		{
			GeoJSONData data = new GeoJSONData();
			string content="";
			using (var input = context.Assets.Open(FileName))
			using (StreamReader sr = new System.IO.StreamReader(input))
			{
				string temp = sr.ReadLine();
				while (true)
				{
					if (string.IsNullOrEmpty(temp)) break;
					content += temp + "\n";
					temp = sr.ReadLine();
				}
			}
			return new GeoJSONData { Data = content };
		}

	}
}
