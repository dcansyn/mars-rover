using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rover.Core;
using System;

namespace Rover.Test
{
    [TestClass]
    public class RoverTest
    {
        [TestMethod]
        public void MoveTest()
        {
            var data = new string[]
                {
                    "5 5",
                    "1 2 N",
                    "LMLMLMLMM",
                    "3 3 E",
                    "MMRMMRMRRM"
                };

            using (var service = new Core.Services.MoveService())
            {
                var result = service.Move(data);
                Assert.AreEqual($"1 3 N{Environment.NewLine}5 1 E", result);
            }
        }
    }
}
