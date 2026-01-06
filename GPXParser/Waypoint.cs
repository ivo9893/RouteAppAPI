using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPXParser
{
    public class  Waypoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Elevation { get; set; }
        public double DistanceFromStart { get; set; }
        public DateTime? Time { get; set; }
    }
}
