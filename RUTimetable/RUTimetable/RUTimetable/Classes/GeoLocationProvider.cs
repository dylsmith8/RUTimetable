using System;
using Plugin.Geolocator;
namespace RUTimetable
{
	public class GeoLocationProvider
	{
		double LAT, LONG;
		public GeoLocationProvider()
		{
			GetLocation();
		}
		public async void GetLocation()
		{
			var locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 50;
			var position = await locator.GetPositionAsync(new TimeSpan(50000));
			LAT = position.Latitude;
			LONG = position.Longitude;
		}
		public LatLongHolder GetLatLong()
		{
			return new LatLongHolder { Latitude = LAT, Longitude = LONG };
		}
	}
}
