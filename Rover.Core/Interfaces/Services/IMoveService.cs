using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Interfaces.Services
{
    public interface IMoveService
    {
        string Move(string[] data);
        string GetLocation(List<Core.Models.Position.RoverItem> rovers);
    }
}
