var GpsTrack = (function ()
{
	return function (gpsTrackJson)
	{
		var wayPoints = [];
		
		// private functions
		var init;


		///
		///
		///
		init = function ()
		{
			var wps = gpsTrackJson.WayPoints;
			var n = wps.length;
			for (var i = 0; i < n; ++i)
				wayPoints.push(WayPoint(wps[i]));
		};

		init();

		return {
			wayPoints: wayPoints,
			
			

			///
			/// diameter is in meters
			///
			findNearestWayPoint: function (latLng, diameter)
			{
				var minDist = Infinity;
				var wayPoint= null;
				var n = wayPoints.length;
				for (var i = 0; i < n; ++i)
				{
					var dist = google.maps.geometry.spherical.computeDistanceBetween(latLng, wayPoints[i].latLng());
					if (dist < minDist)
					{
						minDist = dist;
						wayPoint = wayPoints[i];
						if (dist <= diameter)
							break;
					}
				}

				return wayPoint;
			},
			


			///
			/// diameter is in meters
			///
			findWayPointIndex: function (latLng, diameter)
			{
				var n = wayPoints.length;
				for (var i = 0; i < n; ++i)
				{
					var dist = google.maps.geometry.spherical.computeDistanceBetween(latLng, wayPoints[i].latLng());
					if (dist <= diameter)
						return i;
				}
				return -1;
			},
			


			///
			///
			///
			calcPathStats: function (wpIndex1, wpIndex2)
			{
				if (wayPoints[wpIndex1].time > wayPoints[wpIndex2].time)		// wrong direction
					return null;

				var totalDistance = 0;
				var maxSpeed = wayPoints[wpIndex1].speed;
				var minSpeed = wayPoints[wpIndex1].speed;
				var duration = (wayPoints[wpIndex2].time.getTime() - wayPoints[wpIndex1].time.getTime()) / 1000;

				
				for (var i = wpIndex1+1; i <= wpIndex2; ++i)
				{
					totalDistance += google.maps.geometry.spherical.computeDistanceBetween(wayPoints[i-1].latLng(), wayPoints[i].latLng());
					maxSpeed = Math.max (wayPoints[i].speed, maxSpeed);
					minSpeed = Math.min (wayPoints[i].speed, minSpeed);
				}

				return {
					totalDistance: totalDistance,
					maxSpeed: maxSpeed,
					minSpeed: minSpeed,
					avgSpeed: totalDistance / duration,
					duration: duration
				};
			}
		};
	};
})();
