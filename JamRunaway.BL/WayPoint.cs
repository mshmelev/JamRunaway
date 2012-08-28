using System;

namespace JamRunaway.BL
{
	public class WayPoint
	{
		public WayPoint()
		{
			
		}

		public WayPoint(decimal longitude, decimal latitude, DateTime time, decimal speed= 0)
		{
			Longitude = longitude;
			Latitude = latitude;
			Time = time;
			Speed = speed;
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


		/// <summary>
		/// Speed in m/s
		/// </summary>
		public decimal Speed
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
			return Latitude == other.Latitude 
				&& Longitude == other.Longitude
				&& Time.Equals(other.Time)
				&& Speed== other.Speed;
		}


		public override int GetHashCode()
		{
			unchecked
			{
				int hashCode = Latitude.GetHashCode();
				hashCode = (hashCode * 397) ^ Longitude.GetHashCode();
				hashCode = (hashCode * 397) ^ Time.GetHashCode();
				hashCode = (hashCode * 397) ^ Speed.GetHashCode();
				return hashCode;
			}
		}



	}
}