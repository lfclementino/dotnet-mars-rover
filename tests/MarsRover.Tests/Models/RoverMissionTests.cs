using MarsRover.Domain.Models;
using Xunit;

namespace MarsRover.Tests.Models
{
    public class RoverMissionTests
    {
        [Fact]
        public void ShouldCreateNewModel()
        {
            var roverMission = new RoverMission();

            Assert.NotNull(roverMission);
        }

        [Fact]
        public void ShouldSetCorrectValuesOnNewModel()
        {

            var rover = new Rover();
            var floor = new Floor()
            {
                Height = 100,
                Width = 100
            };

            var roverMission = new RoverMission(floor, rover);

            Assert.NotNull(roverMission);
            Assert.Equal(rover.Id, roverMission.Rover.Id);
            Assert.Equal(100, roverMission.Floor.Height);
            Assert.Equal(100, roverMission.Floor.Width);
        }
    }
}
