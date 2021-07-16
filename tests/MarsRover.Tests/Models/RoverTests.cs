using MarsRover.Domain.Enums;
using MarsRover.Domain.Models;
using System;
using Xunit;

namespace MarsRover.Tests.Models
{
    public class RoverTests
    {
        [Fact]
        public void ShouldCreateNewModel()
        {
            var rover = new Rover();

            Assert.NotNull(rover);
            Assert.False(rover.Id == default(Guid));

        }

        [Fact]
        public void ShouldSetCorrectValuesOnNewModel()
        {
            var guid = Guid.NewGuid();
            var location = new RoverLocation()
            {
                Direction = Direction.North,
                X = 2,
                Y = 4
            };

            var rover = new Rover()
            {
                Id = guid,
                Location = location
            };

            Assert.NotNull(rover);
            Assert.Equal(guid, rover.Id);
            Assert.Equal(location.Direction, rover.Location.Direction);
            Assert.Equal(location.X, rover.Location.X);
            Assert.Equal(location.Y, rover.Location.Y);
        }
    }
}
