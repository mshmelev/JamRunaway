using System;
using System.IO;
using JamRunaway.BL;
using JamRunaway.BL.GeoFileParsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JamRunaway.Tests.GeoFileParsers
{
	[DeploymentItem(@"TestFiles\MissingTime.kml")]
	[DeploymentItem(@"TestFiles\MultiTracks.kml")]
	[TestClass]
	public class KmlParserTest
	{
		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get; set;
		}

		
		 [ClassInitialize()]
		 public static void MyClassInitialize(TestContext testContext)
		 {
			 
		 }
		
		


		[TestMethod]
		public void ExtractWayPoints_MultiTracksTest()
		{
			string testFilePath = Path.Combine(Directory.GetCurrentDirectory(), "MultiTracks.kml");
			var wayPoints = new KmlParser().ExtractWayPoints(File.OpenRead(testFilePath));
			Assert.AreEqual(5, wayPoints.Count);
		}



		[TestMethod]
		public void ExtractWayPoints_MissingTimeTest()
		{
			string testFilePath = Path.Combine(Directory.GetCurrentDirectory(), "MissingTime.kml");
			var wayPoints = new KmlParser().ExtractWayPoints(File.OpenRead(testFilePath));

			Assert.AreEqual(wayPoints[0], new WayPoint (-71.2m, 43.8m, DateTime.Parse("2012-08-27T11:00:00.000Z")));
			Assert.AreEqual(wayPoints[2].Time, wayPoints[1].Time);
		}


		[TestMethod]
		public void ExtractWayPoints_EmptyListTest()
		{
			const string kmlContent = "<kml></kml>";
			var wayPoints = new KmlParser().ExtractWayPoints(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(kmlContent)));
			Assert.AreEqual(0, wayPoints.Count);

		}
	}
}
