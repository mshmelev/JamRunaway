using System;
using JamRunaway.BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JamRunaway.Tests
{
	[TestClass]
	public class GeoToolsTest
	{
		[TestMethod]
		public void CalcDistance_Test()
		{
			Assert.AreEqual(GeoTools.CalcDistance(new WayPoint(0, 0, DateTime.Now), new WayPoint(0, 90, DateTime.Now)), GeoTools.CalcDistance(new WayPoint(0, 0, DateTime.Now), new WayPoint(180, 90, DateTime.Now)));
			Assert.AreEqual(20015086, (int)GeoTools.CalcDistance(new WayPoint(0, 0, DateTime.Now), new WayPoint(180, 0, DateTime.Now)));
		}
	}
}
