using System;

namespace JamRunaway.BL
{
	public class WayPoint
	{
		public WayPoint()
		{
			
		}

		public WayPoint(decimal longitude, decimal latitude, DateTime time)
		{
			Longitude = longitude;
			Latitude = latitude;
			Time = time;
		}


		public decimal Latitude
		{
			get;
			set;
		}

		public decimal Longitude
		{
			get;
			set;
		}

		public DateTime Time
		{
			get;
			set;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((WayPoint) obj);
		}


		protected bool Equals(WayPoint other)
		{
			return Latitude == other.Latitude && Longitude == other.Longitude && Time.Equals(other.Time);
		}


		public override int GetHashCode()
		{
			unchecked
			{
				int hashCode = Latitude.GetHashCode();
				hashCode = (hashCode * 397) ^ Longitude.GetHashCode();
				hashCode = (hashCode * 397) ^ Time.GetHashCode();
				return hashCode;
			}
		}



	}
}