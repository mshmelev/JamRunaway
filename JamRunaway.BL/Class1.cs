using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Dom.GX;
using SharpKml.Engine;

namespace JamRunaway.BL
{
	public class Class1
	{
		public static void ExtractWayPoints()
		{
			var kmlFile = KmlFile.Load(@"d:\aaa.kml");
			if (!(kmlFile.Root is Kml))
				return;

			var placemarks = new List<Placemark>();
			ExtractPlacemarks(((Kml)kmlFile.Root).Feature, placemarks);

			var tracks= new List<Track>();
			foreach (var placemark in placemarks)
				ExtractTracks(placemark.Geometry, tracks);

			var wayPoints = new List<WayPoint>();
			foreach (var track in tracks)
			{
				DateTime? time = null;
				var children= track.GetChildElements();
				foreach (var child in children)
				{
					if (child is DateTime)
						time = (DateTime)child;
					else if (child is Vector && time!= null)
						wayPoints.Add(new WayPoint
							{
								Latitude = (decimal)((Vector)child).Latitude,
								Longitude = (decimal)((Vector)child).Longitude,
								Time = time.Value
							});
				}

				//track.Children
			}
		}


		private static void ExtractPlacemarks(Feature feature, List<Placemark> placemarks)
		{
			if (feature is Placemark)
			{
				placemarks.Add(feature as Placemark);
			}
			else if (feature is Container)
			{
				foreach (var f in ((Container)feature).Features)
					ExtractPlacemarks(f, placemarks);
			}
		}


		private static void ExtractTracks(Geometry geometry, List<Track> tracks)
		{
			if (geometry is Track)
				tracks.Add(geometry as Track);
			else if (geometry is MultipleTrack)
				tracks.AddRange(((MultipleTrack)geometry).Tracks);
			else if (geometry is MultipleGeometry)
			{
				foreach (var childGeometry in ((MultipleGeometry) geometry).Geometry)
					ExtractTracks(childGeometry, tracks);
			}
		}





	}

}