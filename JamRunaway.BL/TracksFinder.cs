using System;
using System.Collections.Generic;
using System.Linq;

namespace JamRunaway.BL
{
	public static class TracksFinder
	{
		public static readonly TimeSpan TrackTimeSpanSplitter = TimeSpan.FromHours(1);


		public static List<GpsTrack> FindTracks(List<WayPoint> wayPoints)
		{
			var gpsTracks = new List<GpsTrack>();

			var sortedPoints= wayPoints.OrderBy (wp => wp.Time);
			var trackPoints= new List<WayPoint>();
			foreach (var wayPoint in sortedPoints)
			{
				if (trackPoints.Count == 0)
				{
					trackPoints.Add(wayPoint);
				}
				else if (wayPoint.Time - trackPoints.Last().Time > TrackTimeSpanSplitter)
				{
					AddGpsTrack(trackPoints, gpsTracks);
					trackPoints = new List<WayPoint> { wayPoint };
				}
				else
				{
					trackPoints.Add(wayPoint);
				}
			}
			AddGpsTrack(trackPoints, gpsTracks);

			return gpsTracks;
		}


		private static void AddGpsTrack(List<WayPoint> trackPoints, List<GpsTrack> gpsTracks)
		{
			if (trackPoints.Count > 1)
				gpsTracks.Add(new GpsTrack {WayPoints = trackPoints});
		}
	}
}