using System;
using JamRunaway.BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JamRunaway.Tests
{
	[TestClass]
	public class GpsTrackTest
	{
		[TestMethod]
		public void Speed_Test()
		{
			var track = new GpsTrack(new WayPoint[]
				{
					new WayPoint(0, 0, DateTime.Now),
					new WayPoint(1, 0, DateTime.Now),
					new WayPoint(2, 0, DateTime.Now.AddSeconds(2)),
				});

			Assert.AreEqual(0, track.WayPoints[0].Speed);
			Assert.AreEqual(0, track.WayPoints[1].Speed);
			Assert.AreEqual(55597, (int)track.WayPoints[2].Speed);
		}


		[TestMethod]
		public void RemoveWarmUpPoints_Test()
		{
			var track = new GpsTrack(new WayPoint[]
				{
					new WayPoint(0, 0, DateTime.Now),
					new WayPoint(0.000001m, 0, DateTime.Now.AddSeconds(1)),
					new WayPoint(0.000002m, 0, DateTime.Now.AddSeconds(2)),
					new WayPoint(1, 0, DateTime.Now.AddSeconds(3)),
					new WayPoint(2, 0, DateTime.Now.AddSeconds(4)),
					new WayPoint(3, 0, DateTime.Now.AddSeconds(5)),
					new WayPoint(3.000001m, 0, DateTime.Now.AddSeconds(6)),
					new WayPoint(3.000002m, 0, DateTime.Now.AddSeconds(7)),
					new WayPoint(3.000003m, 0, DateTime.Now.AddSeconds(8)),
				});

			Assert.AreEqual(4, track.WayPoints.Count);
			Assert.AreEqual(0.000002m, track.WayPoints[0].Longitude);
			Assert.AreEqual(1, track.WayPoints[1].Longitude);
			Assert.AreEqual(2, track.WayPoints[2].Longitude);
			Assert.AreEqual(3, track.WayPoints[3].Longitude);
		}
	}
}
