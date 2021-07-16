using FluentAssertions;
using MarsRover.Domain.Commands;
using MarsRover.Domain.Enums;
using MarsRover.Domain.Models;
using Xunit;

namespace MarsRover.Tests.Commands
{
    public class TurnLeftCommandTests
    {
        [Theory]
        [InlineData(5, 5, 2, 2, Direction.North, true, Direction.West)]
        [InlineData(5, 5, 2, 2, Direction.East, true, Direction.North)]
        [InlineData(5, 5, 2, 2, Direction.West, true, Direction.South)]
        [InlineData(5, 5, 2, 2, Direction.South, true, Direction.East)]
        public void Execute_ShouldReturnResultForFloorAndRover(int width, int height, int x, int y, Direction direction, bool expected, Direction finalDirection)
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

            var command = new TurnLeftCommand();

            var result = command.Execute(floor, rover);

            result.Should().Be(expected);
            rover.Location.Direction.Should().Be(finalDirection);
        }
    }
}
