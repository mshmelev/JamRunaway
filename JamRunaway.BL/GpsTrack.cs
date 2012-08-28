using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace JamRunaway.BL
{
	public class GpsTrack
	{
		private const decimal WarmUpMinDistance= 1;

		/// <summary>
		/// .ctor
		/// </summary>
		public GpsTrack()
		{
			
		}


		/// <summary>
		/// .ctor
		/// </summary>
		/// <param name="wayPoints"></param>
		public GpsTrack(IEnumerable<WayPoint> wayPoints)
		{
			var wps = wayPoints.OrderBy (wp => wp.Time).ToList();
			RemoveWarmUpPoints(wps);
			CalcSpeed(wps);
			WayPoints = wps.AsReadOnly();
		}



		/// <summary>
		/// 
		/// </summary>
		public ReadOnlyCollection<WayPoint> WayPoints
		{
			get;
			private set;
		}




		private static void CalcSpeed(List<WayPoint> wayPoints)
		{
			for (int i = 1; i < wayPoints.Count; ++i)
			{
				decimal d = GeoTools.CalcDistance(wayPoints[i], wayPoints[i - 1]);
				decimal t = (decimal)(wayPoints[i].Time - wayPoints[i - 1].Time).TotalSeconds;
				if (t == 0)
					wayPoints[i].Speed = 0;
				else
					wayPoints[i].Speed = d / t;
			}
		}



		private static void RemoveWarmUpPoints(List<WayPoint> wayPoints)
		{
			// remove warm up points
			int startMovementPos = 0;
			for (int i = 1; i < wayPoints.Count; ++i)
			{
				if (GeoTools.CalcDistance(wayPoints[i], wayPoints[i - 1])> WarmUpMinDistance)
				{
					startMovementPos = i - 1;
					break;
				}
			}

			for (int i = 0; i < startMovementPos; ++i)
				wayPoints.RemoveAt(0);

			// remove cool down points
			int endMovementPos = wayPoints.Count-1;
			for (int i = wayPoints.Count-2; i >= 0; --i)
			{
				if (GeoTools.CalcDistance(wayPoints[i], wayPoints[i + 1]) > WarmUpMinDistance)
				{
					endMovementPos = i + 1;
					break;
				}
			}

			for (int i = wayPoints.Count - 1; i > endMovementPos; --i)
				wayPoints.RemoveAt(i);
		}







	}
}