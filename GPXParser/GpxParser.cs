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
        private const double EarthRadius = 6371000;
        private const double DegToRad = Math.PI / 180.0;

        private const double MinDistanceThreshold = 2.0; 
        private const double MinElevationThreshold = 0.5;

        public Route Parse(Stream gpxStream)
        {
            var route = new Route();
            route.Points.Capacity = 3600;

            using var reader = XmlReader.Create(gpxStream, new XmlReaderSettings
            {
                IgnoreComments = true,
                IgnoreWhitespace = true,
                DtdProcessing = DtdProcessing.Ignore
            });

            var nameTable = reader.NameTable;
            var atomTrkpt = nameTable.Add("trkpt");
            var atomWpt = nameTable.Add("wpt");
            var atomLat = nameTable.Add("lat");
            var atomLon = nameTable.Add("lon");
            var atomEle = nameTable.Add("ele");
            var atomTime = nameTable.Add("time");
            var atomName = nameTable.Add("name");

            Waypoint currentPoint = null;

            double prevLat = double.NaN;
            double prevLon = double.NaN;
            double prevEle = double.NaN;

            double runningDistance = 0;
            double runningElevationGain = 0;
            bool nameFound = false;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (ReferenceEquals(reader.LocalName, atomTrkpt) || ReferenceEquals(reader.LocalName, atomWpt))
                    {
                        currentPoint = new Waypoint();
                        if (double.TryParse(reader.GetAttribute(atomLat), out double lat)) currentPoint.Latitude = lat;
                        if (double.TryParse(reader.GetAttribute(atomLon), out double lon)) currentPoint.Longitude = lon;
                    }
                    else if (ReferenceEquals(reader.LocalName, atomEle) && currentPoint != null)
                    {
                        if (double.TryParse(reader.ReadElementContentAsString(), out double ele)) currentPoint.Elevation = ele;
                    }
                    else if (ReferenceEquals(reader.LocalName, atomTime) && currentPoint != null)
                    {
                        if (DateTime.TryParse(reader.ReadElementContentAsString(), out DateTime t)) currentPoint.Time = t;
                    }
                    else if (!nameFound && ReferenceEquals(reader.LocalName, atomName))
                    {
                        route.Name = reader.ReadElementContentAsString();
                        nameFound = true;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement)
                {
                    if ((ReferenceEquals(reader.LocalName, atomTrkpt) || ReferenceEquals(reader.LocalName, atomWpt)) && currentPoint != null)
                    {
               
                        bool isFirstPoint = double.IsNaN(prevLat);
                        double dist = 0;

                        if (!isFirstPoint)
                        {
                            double latRad = currentPoint.Latitude * DegToRad;
                            double prevLatRad = prevLat * DegToRad;
                            double lonDiff = (currentPoint.Longitude - prevLon) * DegToRad;
                            double x = lonDiff * Math.Cos((prevLatRad + latRad) * 0.5);
                            double y = latRad - prevLatRad;
                            dist = Math.Sqrt(x * x + y * y) * EarthRadius;
                        }

                       if (isFirstPoint || dist >= MinDistanceThreshold)
                        {
                            runningDistance += dist;

                           if (currentPoint.Elevation.HasValue)
                            {
                                if (!double.IsNaN(prevEle))
                                {
                                    double eleDiff = currentPoint.Elevation.Value - prevEle;

                                    if (Math.Abs(eleDiff) >= MinElevationThreshold)
                                    {
                                        if (eleDiff > 0)
                                        {
                                            runningElevationGain += eleDiff;
                                        }
                                        prevEle = currentPoint.Elevation.Value;
                                    }
                                }
                                else
                                {
                                    prevEle = currentPoint.Elevation.Value;
                                }
                            }

                            currentPoint.Latitude = Math.Round(currentPoint.Latitude, 6);
                            currentPoint.Longitude = Math.Round(currentPoint.Longitude, 6);
                            currentPoint.DistanceFromStart = Math.Round(runningDistance, 2);
                            if (currentPoint.Elevation.HasValue)
                                currentPoint.Elevation = Math.Round(currentPoint.Elevation.Value, 1);

                            route.Points.Add(currentPoint);

                            prevLat = currentPoint.Latitude;
                            prevLon = currentPoint.Longitude;
                        }

              
                        currentPoint = null;
                    }
                }
            }

            route.TotalElevationGain = Math.Round(runningElevationGain, 1);
            return route;
        }
    }
        
}
