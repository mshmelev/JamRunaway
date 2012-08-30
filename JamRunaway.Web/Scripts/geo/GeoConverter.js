var GeoConverter =
{
	///
	/// Converts speed from m/s to km/h
	///
	toKmh: function (speed)
	{
		speed = speed / 1000 * 3600.0;
		return speed.toFixed(1) + " km/h";
	},
	
	///
	/// Converst distance from meters to kilometers
	///
	toKm: function (distance)
	{
		distance = distance / 1000;
		return distance.toFixed(1) + " km";
	},
	

	///
	/// Converts duration from seconds to a human readable format
	///
	toDuration: function (dur)
	{
		var hours = Math.floor(dur / 3600);
		var minutes = Math.floor(dur / 60 - hours * 60);
		var seconds = dur % 60;

		return hours+":"+ ((minutes<= 9) ? "0" : "")+ minutes+ ":"+ ((seconds<= 9) ? "0" : "")+ seconds;
	}	
};
