module("WayPoint");

test ("WayPoint initialized correctly from JSON", function () {
	expect(4);

	var curTime = new Date();
	var wp = new WayPoint({ Latitude: 1.2, Longitude: 2.3, Time: "/Date(" + curTime.getTime() + ")/", Speed: 4.5 });

	equal(1.2, wp.latitude);
	equal(2.3, wp.longitude);
	deepEqual(curTime, wp.time);
	equal (4.5, wp.speed);
	
});
