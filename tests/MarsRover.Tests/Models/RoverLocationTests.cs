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
                X = new Axis(200),
                Y = new Axis(200)
            };

            Assert.NotNull(roverLocation);
            Assert.Equal(Direction.North, roverLocation.Direction);
            Assert.Equal(200, roverLocation.X.Value);
            Assert.Equal(200, roverLocation.Y.Value);
        }
    }
}
