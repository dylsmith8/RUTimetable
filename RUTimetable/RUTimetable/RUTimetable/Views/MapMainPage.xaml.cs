using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.CustomMap.Api;
using TK.CustomMap.Overlays;
using Xamarin.Forms;
using TK.CustomMap;
using Xamarin.Forms.Maps;
namespace RUTimetable
{
	public partial class MapMainPage : ContentPage
	{
		public MapMainPage()
		{
			InitializeComponent();

			this.CreateView();
			this.BindingContext = new MapsViewModel();
		}

		private async void CreateView()
		{
			//var pos = new GeoLocationProvider();
			//pos.GetLocation();
			//var latlong = pos.GetLatLong();
			var currentPostion = new Position(26.520642, -33.311836);//deafualt location is the Drama Department later this will be changedd to use current location check vars above
			var mapView = new TKCustomMap(MapSpan.FromCenterAndRadius(currentPostion, Distance.FromKilometers(2)));
			mapView.IsShowingUser = true;
			mapView.SetBinding(TKCustomMap.CustomPinsProperty, "Pins");
			mapView.SetBinding(TKCustomMap.MapClickedCommandProperty, "MapClickedCommand");
			mapView.SetBinding(TKCustomMap.MapLongPressCommandProperty, "MapLongPressCommand");
			mapView.SetBinding(TKCustomMap.MapCenterProperty, "MapCenter");
			mapView.SetBinding(TKCustomMap.PinSelectedCommandProperty, "PinSelectedCommand");
			mapView.SetBinding(TKCustomMap.SelectedPinProperty, "SelectedPin");
			mapView.SetBinding(TKCustomMap.RoutesProperty, "Routes");
			mapView.SetBinding(TKCustomMap.PinDragEndCommandProperty, "DragEndCommand");
			mapView.SetBinding(TKCustomMap.CirclesProperty, "Circles");
			mapView.SetBinding(TKCustomMap.CalloutClickedCommandProperty, "CalloutClickedCommand");
			mapView.SetBinding(TKCustomMap.PolylinesProperty, "Lines");
			mapView.SetBinding(TKCustomMap.PolygonsProperty, "Polygons");
			mapView.SetBinding(TKCustomMap.MapRegionProperty, "MapRegion");
			mapView.SetBinding(TKCustomMap.RouteClickedCommandProperty, "RouteClickedCommand");
			mapView.SetBinding(TKCustomMap.RouteCalculationFinishedCommandProperty, "RouteCalculationFinishedCommand");
			mapView.SetBinding(TKCustomMap.TilesUrlOptionsProperty, "TilesUrlOptions");
			mapView.SetBinding(TKCustomMap.MapFunctionsProperty, "MapFunctions");
			mapView.IsRegionChangeAnimated = true;
			this.Content = mapView;
			//this._baseLayout.Children.Add(
			//    mapView,
			//    Constraint.RelativeToView(autoComplete, (r, v) => v.X),
			//    Constraint.RelativeToView(autoComplete, (r, v) => autoComplete.HeightOfSearchBar),
			//    heightConstraint: Constraint.RelativeToParent((r) => r.Height - autoComplete.HeightOfSearchBar),
			//    widthConstraint: Constraint.RelativeToView(autoComplete, (r, v) => v.Width));

			//this._baseLayout.Children.Add(
			//    autoComplete,
			//    Constraint.Constant(0),
			//    Constraint.Constant(0));
		}
	}
}