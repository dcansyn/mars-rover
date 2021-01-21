using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Const
{
    public class Global
    {
        public static char[] CardinalDirections = new char[] { 'N', 'E', 'S', 'W' };
        public static List<(char Direction, char Rotate, char Result)> DirectionLookup = new List<(char Direction, char Rotate, char Result)>
        {
                ('N', 'L', 'W'),
                ('W', 'L', 'S'),
                ('S', 'L', 'E'),
                ('E', 'L', 'N'),
                ('N', 'R', 'E'),
                ('E', 'R', 'S'),
                ('S', 'R', 'W'),
                ('W', 'R', 'N')
        };
    }
}
