using MarsRover.Domain.Enums;
using MarsRover.Domain.Exceptions;
using MarsRover.Domain.Models;
using Xunit;

namespace MarsRover.Tests.Models
{
    public class RoverMissionTests
    {
        [Theory]
        [InlineData(4, 3, 10, 30)]
        [InlineData(44, 323, 1000, 1000)]
        public void NewRoverMission_ValidValues_ShouldSetCorrectValuesOnNewModel(int x, int y, int width, int height)
        {

            var rover = new Rover();
            rover.Location = new RoverLocation()
            {
                Direction = Direction.North,
                X = new Axis(x),
                Y = new Axis(y)
            };
            var floor = new Floor(width, height);

            var roverMission = new RoverMission(floor, rover);

            Assert.NotNull(roverMission);
            Assert.Equal(rover.Id, roverMission.Rover.Id);
            Assert.Equal(x, rover.Location.X.Value);
            Assert.Equal(y, rover.Location.Y.Value);
            Assert.Equal(height, roverMission.Floor.Height);
            Assert.Equal(width, roverMission.Floor.Width);
        }

        [Theory]
        [InlineData(40, 50, 10, 10)]
        [InlineData(432, 500, 100, 100)]
        public void NewRoverMission_OutOfBoundsValues_ShouldThrowInvalidRoverMissionException(int x, int y, int width, int height)
        {

            var rover = new Rover();
            rover.Location = new RoverLocation()
            {
                Direction = Direction.North,
                X = new Axis(x),
                Y = new Axis(y)
            };
            var floor = new Floor(width, height);

            Assert.Throws<InvalidRoverMissionException>(() => new RoverMission(floor, rover));
        }
    }
}
