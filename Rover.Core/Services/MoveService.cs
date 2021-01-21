using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rover.Core.Services
{
    public class MoveService : IDisposable, Interfaces.Services.IMoveService
    {
        public string Move(string[] data)
        {
            // Check information
            if (data.Length < 3)
                return string.Empty;

            // Check planet coordinates
            var planetCoordinates = data[0];
            var coordinates = planetCoordinates.Split(' ');
            if (!int.TryParse(coordinates[0], out int planetX) || !int.TryParse(coordinates[1], out int planetY))
                return string.Empty;

            var planetCoordinate = new Core.Models.Position.PlanetCoordinateItem() { X = planetX, Y = planetY };

            // Check rover coordinates
            var checkRoversCoordinates = (data.Length - 1) % 2 == 0;
            if (!checkRoversCoordinates)
                return string.Empty;

            // Rovers
            var rovers = new List<Models.Position.RoverItem>();
            for (int i = 1; i < data.Length; i += 2)
            {
                var roverStartPosition = data[i];
                var roverGo = data[i + 1];

                // Check rover coordinates
                if (!roverStartPosition.Contains(" ") || roverStartPosition.Split(' ').Length < 3)
                    return string.Empty;

                var roverCoordinates = roverStartPosition.Split(' ');
                if (!int.TryParse(roverCoordinates[0], out int roverX) || !int.TryParse(roverCoordinates[1], out int roverY))
                    return string.Empty;

                // Check rover direction
                var roverDirectionStr = roverCoordinates[2];
                if (roverDirectionStr.Length != 1 || !char.TryParse(roverDirectionStr, out char roverDirection) || !Core.Const.Global.CardinalDirections.Contains(roverDirection))
                    return string.Empty;

                if (!roverGo.Contains("M") && (!roverGo.Contains("L") || !roverGo.Contains("R")))
                    return string.Empty;

                rovers.Add(new Models.Position.RoverItem()
                {
                    X = roverX,
                    Y = roverY,
                    Direction = roverDirection,
                    Planet = planetCoordinate,
                    Go = roverGo
                });
            }
            return GetLocation(rovers);
        }

        public string GetLocation(List<Core.Models.Position.RoverItem> rovers)
        {
            foreach (var item in rovers)
            {
                foreach (var go in item.Go)
                {
                    switch (go)
                    {
                        case 'L':
                        case 'R':
                            item.Direction = Core.Const.Global.DirectionLookup.SingleOrDefault(x => x.Direction == item.Direction && x.Rotate == go).Result;
                            break;
                        case 'M':
                            switch (item.Direction)
                            {
                                // X
                                case 'W':
                                case 'E':
                                    item.X = item.Direction == 'W' ? item.X - 1 : item.X + 1;
                                    break;
                                // Y
                                case 'N':
                                case 'S':
                                    item.Y = item.Direction == 'S' ? item.Y - 1 : item.Y + 1;
                                    break;
                            }
                            break;
                    }

                    if (item.X > item.Planet.X || item.Y > item.Planet.Y)
                        return string.Empty;
                }
            }
            return string.Join(Environment.NewLine, rovers.Select(x => $"{x.X} {x.Y} {x.Direction}").ToList());
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
