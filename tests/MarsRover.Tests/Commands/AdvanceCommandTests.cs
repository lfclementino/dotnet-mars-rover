using FluentAssertions;
using MarsRover.Domain.Commands;
using MarsRover.Domain.Enums;
using MarsRover.Domain.Exceptions;
using MarsRover.Domain.Models;
using Xunit;

namespace MarsRover.Tests.Commands
{
    public class AdvanceCommandTests
    {
        [Theory]
        [InlineData(5, 5, 4, 4, Direction.North)]
        [InlineData(10, 10, 2, 5, Direction.East)]
        [InlineData(10, 10, 4, 2, Direction.West)]
        [InlineData(10, 10, 3, 8, Direction.South)]
        public void Execute_ShouldNotThrowException(int width, int height, int x, int y, Direction direction)
        {
            var floor = new Floor(width, height);

            var rover = new Rover()
            {
                Location = new RoverLocation()
                {
                    Direction = direction,
                    X = new Axis(x),
                    Y = new Axis(y)
                }
            };

            var command = new AdvanceCommand();

            command.Execute(floor, rover);

            switch (direction)
            {
                case Direction.North:
                    rover.Location.Y.Value.Should().Be(y + 1);
                    rover.Location.X.Value.Should().Be(x);
                    break;
                case Direction.West:
                    rover.Location.X.Value.Should().Be(x - 1);
                    rover.Location.Y.Value.Should().Be(y);
                    break;
                case Direction.South:
                    rover.Location.Y.Value.Should().Be(y - 1);
                    rover.Location.X.Value.Should().Be(x);
                    break;
                case Direction.East:
                    rover.Location.X.Value.Should().Be(x + 1);
                    rover.Location.Y.Value.Should().Be(y);
                    break;
            }
            //}
        }

        [Theory]
        [InlineData(5, 5, 2, 5, Direction.North)]
        [InlineData(5, 5, 2, 20, Direction.North)]
        [InlineData(10, 10, 10, 2, Direction.East)]
        [InlineData(10, 10, 44, 2, Direction.East)]
        [InlineData(10, 10, 0, 4, Direction.West)]
        [InlineData(10, 10, 10, 0, Direction.South)]
        public void Execute_ShouldThrowException_OutOfBoundsException(int width, int height, int x, int y, Direction direction)
        {
            var floor = new Floor(width, height);

            var rover = new Rover()
            {
                Location = new RoverLocation()
                {
                    Direction = direction,
                    X = new Axis(x),
                    Y = new Axis(y)
                }
            };

            var command = new AdvanceCommand();

            Assert.Throws<OutOfBoundsException>(() => command.Execute(floor, rover));
        }
    }
}
