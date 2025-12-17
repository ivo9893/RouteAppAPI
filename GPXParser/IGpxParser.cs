using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPXParser
{
    public interface IGpxParser
    {
        public Route Parse(Stream gpxStream);
    }
}
