using System.Collections.Generic;
using System.IO;

namespace JamRunaway.BL.GeoFileParsers
{
	public interface IGeoFileParser
	{
		List<WayPoint> ExtractWayPoints(Stream input);
	}
}