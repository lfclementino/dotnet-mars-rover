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
        [InlineData(5, 5, 2, 2, Direction.North, Direction.East)]
        [InlineData(5, 5, 2, 2, Direction.East, Direction.South)]
        [InlineData(5, 5, 2, 2, Direction.West, Direction.North)]
        [InlineData(5, 5, 2, 2, Direction.South, Direction.West)]
        public void Execute_ShouldReturnResultForFloorAndRover(int width, int height, int x, int y, Direction direction, Direction finalDirection)
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

            var command = new TurnRightCommand();

            command.Execute(floor, rover);

            //result.Should().Be(expected);
            rover.Location.Direction.Should().Be(finalDirection);
        }
    }
}
