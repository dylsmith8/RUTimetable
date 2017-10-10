using System;
using System.Collections.Generic;
namespace RUTimetable
{
	public class GEOJSONTOJSONParser
	{
		GeoJSONData data;
		RealmDataBase db;
		public GEOJSONTOJSONParser(GeoJSONData dat)
		{
			data = dat;
			db = new RealmDataBase();
		}
		/// <summary>
		/// Process this GeoJSON data into JSON.
		/// Disclaimer Realm does not support storing of Point data structures
		/// 
		/// </summary>
		public void Process()
		{
			var list = data.Data.Split('\n');
			var VList = new List<VenueLocation>();
			var readCord = false;
			var tempLoc = new VenueLocation();
			for (int i = 0; i < list.Length; i++)
			{
				if (list[i].Contains("properties"))
				{
					i++;
					tempLoc.Name = new CommaRemover(list[i++]).RemoveComma();
					tempLoc.Description = list[i].Trim();
					continue;
				}
				if (list[i].Contains("coordinates"))
				{
					var removecommaLat =  new CommaRemover(list[++i]).RemoveComma();				
					var removecommaLong  = new CommaRemover(list[++i]).RemoveComma();
					tempLoc.Lat = double.Parse(removecommaLat);
					tempLoc.Long = double.Parse(removecommaLong);
					readCord = true;
					continue;
				}
				if (readCord)
				{
					VList.Add(tempLoc);
					readCord = false;
					tempLoc = new VenueLocation();
				}
			}
			db.StoreVenueLocations(VList);
		}
	}
}
