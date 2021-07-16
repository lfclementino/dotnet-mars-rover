using MarsRover.Domain.Enums;
using MarsRover.Domain.Models;
using Xunit;

namespace MarsRover.Tests.Models
{
    public class RoverLocationTests
    {
        [Fact]
        public void ShouldCreateNewModel()
        {
            var roverLocation = new RoverLocation();

            Assert.NotNull(roverLocation);
        }

        [Fact]
        public void ShouldSetCorrectValuesOnNewModel()
        {
            var roverLocation = new RoverLocation()
            {
                Direction = Direction.North,
                X = 200,
                Y = 200
            };

            Assert.NotNull(roverLocation);
            Assert.Equal(Direction.North, roverLocation.Direction);
            Assert.Equal(200, roverLocation.X);
            Assert.Equal(200, roverLocation.Y);
        }
    }
}
