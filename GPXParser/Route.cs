using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPXParser
{
    public class Route
    {
        public string Name { get; set; }
        public List<Waypoint> Points { get; set; } = new List<Waypoint>();
        public double TotalDistance => Points.LastOrDefault()?.DistanceFromStart ?? 0;
        public double TotalElevationGain { get; set; }
        public Route() { }
        public Route(string name, List<Waypoint> waypoints)
        {
            Name = name;
            Points = waypoints;
        }
    }
}
