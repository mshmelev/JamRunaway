module ("GpsTrack", {
	setup: function ()
	{
		this.track = GpsTrack({
			WayPoints: [
				{ Latitude: 0, Longitude: 0, Time: "/Date(0)/", Speed: 0 },
				{ Latitude: 1, Longitude: 0, Time: "/Date(0)/", Speed: 0 },
				{ Latitude: 2, Longitude: 0, Time: "/Date(0)/", Speed: 0 },
				{ Latitude: 3, Longitude: 0, Time: "/Date(0)/", Speed: 0 },
				{ Latitude: 4, Longitude: 0, Time: "/Date(0)/", Speed: 0 }
			]
		});

	}
});


test("findNearestWayPoint", function ()
{
	expect(4);

	deepEqual(this.track.wayPoints[0], this.track.findNearestWayPoint(new google.maps.LatLng(0, 0)));
	deepEqual(this.track.wayPoints[4], this.track.findNearestWayPoint(new google.maps.LatLng(10, 10)));
	
	deepEqual(this.track.wayPoints[3], this.track.findNearestWayPoint(new google.maps.LatLng(2.9, 0)));
	deepEqual(this.track.wayPoints[2], this.track.findNearestWayPoint(new google.maps.LatLng(2.9, 0), 200*1000));
});