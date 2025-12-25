using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace GPXParser
{
    public class GpxParser : IGpxParser
    {
        private static readonly XNamespace gpx = "http://www.topografix.com/GPX/1/1";
        public Route Parse(Stream gpxStream)
        {
            var waypoints = new List<Waypoint>();
            string routeName = "Route";

            using var reader = XmlReader.Create(
                gpxStream,
                new XmlReaderSettings
                {
                    IgnoreComments = true,
                    IgnoreWhitespace = true
                });

            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element)
                    continue;

                if (reader.LocalName == "name" &&
                    reader.NamespaceURI == gpx.NamespaceName &&
                    reader.Depth == 2) 
                {
                    routeName = reader.ReadElementContentAsString();
                }

                if (reader.LocalName == "trkpt" &&
                    reader.NamespaceURI == gpx.NamespaceName)
                {
                    var lat = double.Parse(reader.GetAttribute("lat")!);
                    var lon = double.Parse(reader.GetAttribute("lon")!);

                    double? ele = null;
                    DateTime? time = null;

                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.EndElement &&
                            reader.LocalName == "trkpt")
                            break;

                        if (reader.NodeType != XmlNodeType.Element && reader.NodeType != XmlNodeType.Text)
                            continue;

                        if (reader.LocalName == "ele")
                            ele = reader.ReadElementContentAsDouble();

                        if (reader.LocalName == "time")
                            time = reader.ReadElementContentAsDateTime();
                    }

                    waypoints.Add(new Waypoint(lat, lon, ele, time));
                }
            }

            return new Route(routeName, waypoints);
        }
    }
}
