using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace JamRunaway.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
	        BL.GeoFileParsers.IGeoFileParser geoFileParser = new BL.GeoFileParsers.KmlParser();
			var gpsTracks = BL.TracksFinder.FindTracks(geoFileParser.ExtractWayPoints(System.IO.File.OpenRead(@"d:\aaa.kml")));

			JavaScriptSerializer serializer = new JavaScriptSerializer();
	        string json = serializer.Serialize(gpsTracks);
			return View("Index", (object)json);
        }

    }
}
