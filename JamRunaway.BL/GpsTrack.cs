using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace JamRunaway.BL
{
	public class GpsTrack
	{
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
			WayPoints = wayPoints.OrderBy (wp => wp.Time)
				.ToList()
				.AsReadOnly();
			CalcSpeed();
		}



		/// <summary>
		/// 
		/// </summary>
		public ReadOnlyCollection<WayPoint> WayPoints
		{
			get;
			private set;
		}




		private void CalcSpeed()
		{
			for (int i= 1; i< WayPoints.Count; ++i)
			{
				decimal d = GeoTools.CalcDistance(WayPoints[i], WayPoints[i - 1]);
				decimal t = (decimal)(WayPoints[i].Time - WayPoints[i-1].Time).TotalSeconds;
				if (t == 0)
					WayPoints[i].Speed = 0;
				else
					WayPoints[i].Speed = d/t;
			}
		}







	}
}