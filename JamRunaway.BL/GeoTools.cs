using System;

namespace JamRunaway.BL
{
	public static class GeoTools
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="wp1"></param>
		/// <param name="wp2"></param>
		/// <returns>Distance in meters</returns>
		public static decimal CalcDistance(WayPoint wp1, WayPoint wp2)
		{
			const double r = 6371000;

			double sLat1 = Math.Sin((double)Radians(wp1.Latitude));
			double sLat2 = Math.Sin((double)Radians(wp2.Latitude));
			double cLat1 = Math.Cos((double)Radians(wp1.Latitude));
			double cLat2 = Math.Cos((double)Radians(wp2.Latitude));
			double cLon = Math.Cos((double)(Radians(wp1.Longitude) - Radians(wp2.Longitude)));

			double cosD = sLat1 * sLat2 + cLat1 * cLat2 * cLon;

			double d = Math.Acos(cosD);

			double dist = r * d;

			return (decimal)dist;
		}




		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static decimal Radians(decimal x)
		{
			return x * (decimal)Math.PI / 180;
		}

		 
	}
}