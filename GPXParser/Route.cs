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
        public List<Waypoint> Waypoints { get; set; }

        public Route(string name, List<Waypoint> waypoints)
        {
            Name = name;
            Waypoints = waypoints;
        }
    }
}
