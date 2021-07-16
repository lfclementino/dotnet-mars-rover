using FluentAssertions;
using MarsRover.Domain.Commands;
using MarsRover.Domain.Enums;
using MarsRover.Domain.Models;
using Xunit;

namespace MarsRover.Tests.Commands
{
    public class TurnRightCommandTests
    {
        [Theory]
        [InlineData(5, 5, 2, 2, Direction.North, true, Direction.East)]
        [InlineData(5, 5, 2, 2, Direction.East, true, Direction.South)]
        [InlineData(5, 5, 2, 2, Direction.West, true, Direction.North)]
        [InlineData(5, 5, 2, 2, Direction.South, true, Direction.West)]
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

            var command = new TurnRightCommand();

            var result = command.Execute(floor, rover);

            result.Should().Be(expected);
            rover.Location.Direction.Should().Be(finalDirection);
        }
    }
}
