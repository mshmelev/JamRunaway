var TrackAnalyzer = (function ()
{
	var gpsTracks = [];
	var map;
	var srcMarker = null;
	var dstMarker = null;


	var trackSearchAccuracy = 100;

	// private functions declaration
	var createGoogleMap, onMapClick, addMarker, showTracks, onMarkerDropped, findNearestTracksPoint, showStats;
	


	///
	///
	///
	createGoogleMap = function (mapElementId)
	{
		var mapOptions = {
			zoom: 10,
			center: new google.maps.LatLng(43.785175, -79.341235),
			mapTypeId: google.maps.MapTypeId.ROADMAP
		};
		map = new google.maps.Map(document.getElementById(mapElementId), mapOptions);
		google.maps.event.addListener(map, "click", onMapClick);
	};
	

	///
	///
	///
	showTracks = function ()
	{
		for (var i = 0; i < gpsTracks.length; ++i)
		{
			var path = [];
			for (var j = 0; j < gpsTracks[i].wayPoints.length; ++j)
				path.push(gpsTracks[i].wayPoints[j].latLng());

			new google.maps.Polyline({
				path: path,
				map: map,
				strokeColor: "#ff0000",
				strokeOpacity: 0.7,
				strokeWeight: 2,
				clickable: false
			});
		}
	};


	///
	///
	///
	onMapClick = function (ev)
	{
		addMarker(ev.latLng);
	};
	
	
	///
	///
	///
	addMarker = function (latLng)
	{
		var pt = findNearestTracksPoint(latLng);
		if (pt == null)
			return;

		var marker = new google.maps.Marker({
			position: pt,
			draggable: true
		});
		google.maps.event.addListener(marker, "dragend", onMarkerDropped);

		if (srcMarker == null) {
			srcMarker = marker;
			srcMarker.setIcon(new google.maps.MarkerImage("http://maps.gstatic.com/mapfiles/markers2/marker_greenA.png"));
		} else {
			if (dstMarker)
				dstMarker.setMap(null);
			dstMarker = marker;
			dstMarker.setIcon(new google.maps.MarkerImage("http://maps.gstatic.com/mapfiles/markers2/marker_greenB.png"));
		}

		marker.setMap(map);
		
		showStats();
	};
	

	///
	///
	///
	onMarkerDropped = function (ev)
	{
		var marker = this;
		var pt = findNearestTracksPoint(ev.latLng);
		if (pt == null)
		{
			marker.setMap(null);
			if (marker == srcMarker)
				srcMarker = null;
			if (marker == dstMarker)
				dstMarker = null;
		}
		else
		{
			marker.setPosition(pt);
		}
		showStats();
	};
	


	///
	///
	///
	findNearestTracksPoint = function (latLng)
	{
		var wayPoint = null;
		for (var i = 0; i < gpsTracks.length; ++i)
		{
			wayPoint = gpsTracks[i].findNearestWayPoint(latLng, trackSearchAccuracy);
			if (wayPoint && google.maps.geometry.spherical.computeDistanceBetween(latLng, wayPoint.latLng()) < trackSearchAccuracy)
				break;
		}

		return wayPoint ? wayPoint.latLng() : null;
	};


	showStats = function ()
	{
		if (srcMarker == null || dstMarker == null)
			return;

		var latLng1 = srcMarker.position;
		var latLng2 = dstMarker.position;

		for (var i = 0; i < gpsTracks.length; ++i)
		{
			var pos1 = gpsTracks[i].findWayPointIndex (latLng1, trackSearchAccuracy);
			if (pos1 == -1)
				continue;
			var pos2 = gpsTracks[i].findWayPointIndex (latLng2, trackSearchAccuracy);
			if (pos2 == -1)
				continue;

			var res = gpsTracks[i].calcPathStats(pos1, pos2);
			if (res)
			{
				alert("Total Distance: " + GeoConverter.toKm (res.totalDistance) +
					"\nMax Speed: " + GeoConverter.toKmh (res.maxSpeed) +
					"\nMin Speed: " + GeoConverter.toKmh (res.minSpeed)+
					"\nAvg Speed: " + GeoConverter.toKmh (res.avgSpeed)+
					"\nTime: " + GeoConverter.toDuration (res.duration));
			}
			


		}
	};






	// -------------------------------------------------------------------------------------------------------------------------
	//
	//   Public section
	//
	return {
		gpsTracks : gpsTracks,
		
		///
		///
		///
		init: function (gpsTracksJson, mapElementId)
		{
			for (var i= 0; i< gpsTracksJson.length; ++i)
				gpsTracks.push(GpsTrack(gpsTracksJson[i]));

			google.maps.event.addDomListener(window, "load", function () {
				createGoogleMap(mapElementId);
				showTracks();
			});
		}
	};
})();
