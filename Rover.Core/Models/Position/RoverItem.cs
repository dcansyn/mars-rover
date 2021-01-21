using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Models.Position
{
    public class RoverItem
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Direction { get; set; }
        public string Go { get; set; }
        public PlanetCoordinateItem Planet { get; set; }
    }
}
