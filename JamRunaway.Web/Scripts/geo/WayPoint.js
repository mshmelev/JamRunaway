var WayPoint = (function ()
{
	return function (wpJson)
	{
		var latitude = wpJson.Latitude;
		var longitude = wpJson.Longitude;
		var time = new Date(parseInt(wpJson.Time.substr(6))); 
		var speed = wpJson.Speed;

		return {
			latitude: latitude,
			longitude: longitude,
			time: time,
			speed: speed,

			latLng: function ()
			{
				return new google.maps.LatLng(latitude, longitude);
			}
		};
	};
})();
