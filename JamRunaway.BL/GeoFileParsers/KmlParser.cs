using System;
using System.Collections.Generic;
using System.IO;
using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Dom.GX;
using SharpKml.Engine;

namespace JamRunaway.BL.GeoFileParsers
{
	public class KmlParser : IGeoFileParser
	{

		public List<WayPoint> ExtractWayPoints(Stream input)
		{
			var kmlFile = KmlFile.Load(input);
			if (!(kmlFile.Root is Kml))
				return new List<WayPoint>();

			var placemarks = new List<Placemark>();
			ExtractPlacemarks(((Kml)kmlFile.Root).Feature, placemarks);

			var tracks = new List<Track>();
			foreach (var placemark in placemarks)
				ExtractTracks(placemark.Geometry, tracks);

			return ExtractWayPoints(tracks);
		}


		private static List<WayPoint> ExtractWayPoints(IEnumerable<Track> tracks)
		{
			var wayPoints = new List<WayPoint>();
			foreach (var track in tracks)
			{
				DateTime? time = null;
				var children = track.GetChildElements();
				foreach (var child in children)
				{
					if (child is DateTime)
					{
						time = (DateTime) child;
					}
					else if (child is Vector && time != null)
					{
						wayPoints.Add(new WayPoint
							{
								Latitude = (decimal) ((Vector) child).Latitude,
								Longitude = (decimal) ((Vector) child).Longitude,
								Time = time.Value
							});
					}
				}
			}

			return wayPoints;
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