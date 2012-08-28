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
	}
}
