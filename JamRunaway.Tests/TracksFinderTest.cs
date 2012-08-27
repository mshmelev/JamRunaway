using System;
using System.Collections.Generic;
using JamRunaway.BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JamRunaway.Tests
{
	[TestClass]
	public class TracksFinderTest
	{
		[TestMethod]
		public void FindTracks_TestEmpty()
		{
			var gpsTracks= TracksFinder.FindTracks(new List<WayPoint>());
			Assert.AreEqual(0, gpsTracks.Count);

			gpsTracks = TracksFinder.FindTracks(new List<WayPoint> {new WayPoint (0, 0, DateTime.Now)});
			Assert.AreEqual(0, gpsTracks.Count);
		}


		[TestMethod]
		public void FindTracks_TestSingleTrack()
		{
			var gpsTracks = TracksFinder.FindTracks(new List<WayPoint> {
				new WayPoint(0, 0, DateTime.Now),
				new WayPoint(0, 0, DateTime.Now.AddSeconds(1)),
				new WayPoint(0, 0, DateTime.Now.AddSeconds(2)),
			});

			Assert.AreEqual(1, gpsTracks.Count);
			Assert.AreEqual(3, gpsTracks[0].WayPoints.Count);
		}


		[TestMethod]
		public void FindTracks_TestMultipleTracks()
		{
			var gpsTracks = TracksFinder.FindTracks(new List<WayPoint> {
				new WayPoint(0, 0, DateTime.Now),
				new WayPoint(0, 0, DateTime.Now.AddSeconds(1)),
				new WayPoint(0, 0, DateTime.Now.AddSeconds(2)),
				
				new WayPoint(0, 0, DateTime.Now.Add(TracksFinder.TrackTimeSpanSplitter).AddSeconds(3)),
				new WayPoint(0, 0, DateTime.Now.Add(TracksFinder.TrackTimeSpanSplitter).AddSeconds(4)),
				
				new WayPoint(0, 0, DateTime.Now.Add(TracksFinder.TrackTimeSpanSplitter).Add(TracksFinder.TrackTimeSpanSplitter).AddSeconds(5)),
			});

			Assert.AreEqual(2, gpsTracks.Count);
			Assert.AreEqual(3, gpsTracks[0].WayPoints.Count);
			Assert.AreEqual(2, gpsTracks[1].WayPoints.Count);


		}

	}
}
