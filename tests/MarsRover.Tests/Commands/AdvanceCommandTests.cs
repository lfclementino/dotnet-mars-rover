using FluentAssertions;
using MarsRover.Domain.Commands;
using MarsRover.Domain.Enums;
using MarsRover.Domain.Models;
using Xunit;

namespace MarsRover.Tests.Commands
{
    public class AdvanceCommandTests
    {
        [Theory]
        [InlineData(5, 5, 4, 4, Direction.North, true)]
        [InlineData(5, 5, 2, 5, Direction.North, false)]
        [InlineData(5, 5, 2, 20, Direction.North, false)]
        [InlineData(10, 10, 2, 5, Direction.East, true)]
        [InlineData(10, 10, 10, 2, Direction.East, false)]
        [InlineData(10, 10, 44, 2, Direction.East, false)]
        [InlineData(10, 10, 4, 2, Direction.West, true)]
        [InlineData(10, 10, 0, 4, Direction.West, false)]
        [InlineData(10, 10, 3, 8, Direction.South, true)]
        [InlineData(10, 10, 10, 0, Direction.South, false)]
        public void Execute_ShouldReturnResultForFloorAndRover(int width, int height, int x, int y, Direction direction, bool expected)
        {
            var floor = new Floor()
            {
                Width = width,
                Height = height
            };

            var rover = new Rover()
            {
                Location = new RoverLocation()
                {
                    Direction = direction,
                    X = x,
                    Y = y
                }
            };

            var command = new AdvanceCommand();

            var result = command.Execute(floor, rover);

            result.Should().Be(expected);

            if(result)
            {
                switch(direction)
                {
                    case Direction.North:
                        rover.Location.Y.Should().Be(y + 1);
                        rover.Location.X.Should().Be(x);
                        break;
                    case Direction.West:
                        rover.Location.X.Should().Be(x - 1);
                        rover.Location.Y.Should().Be(y);
                        break;
                    case Direction.South:
                        rover.Location.Y.Should().Be(y - 1);
                        rover.Location.X.Should().Be(x);
                        break;
                    case Direction.East:
                        rover.Location.X.Should().Be(x + 1);
                        rover.Location.Y.Should().Be(y);
                        break;
                }
            }
        }
    }
}
