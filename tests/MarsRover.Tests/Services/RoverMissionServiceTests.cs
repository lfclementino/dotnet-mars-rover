using FluentAssertions;
using MarsRover.Domain.Enums;
using MarsRover.Domain.Interfaces;
using MarsRover.Domain.Models;
using MarsRover.Domain.Options;
using MarsRover.Domain.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace MarsRover.Tests.Services
{
    public class RoverMissionServiceTests
    {
        private readonly Mock<ILogger<IRoverMissionService>> _mockLogger;
        private readonly Mock<ILogger<ICommandsService>> _mockCommandLogger;
        private readonly ICommandsService _commandsService;

        public RoverMissionServiceTests()
        {
            var commandsOptions = Options.Create<CommandsOptions>(
                                    new CommandsOptions()
                                    {
                                        AdvanceSteps = 1
                                    });
            _mockCommandLogger = new Mock<ILogger<ICommandsService>>();
            _commandsService = new CommandsService(_mockCommandLogger.Object, commandsOptions);
            _mockLogger = new Mock<ILogger<IRoverMissionService>>();
        }

        [Theory]
        [InlineData(100, 100, 40, 30, Direction.South, "AA", 40, 28, Direction.South)]
        [InlineData(100, 100, 42, 90, Direction.North, "A", 42, 91, Direction.North)]
        public void MoveRover_ValidFloorAndLocation_ShouldReturnTrue(int width, int height, int x, int y, Direction direction, string commands, int xFinal, int yFinal, Direction directionFinal)
        {
            var testFloor = new Floor(width, height);

            var testRover = new Rover();
            testRover.Location.Direction = direction;
            testRover.Location.X = new Axis(x);
            testRover.Location.Y = new Axis(y);

            var roverMission = new RoverMission(testFloor, testRover);

            var roverMissionService = new RoverMissionService(_mockLogger.Object, _commandsService);

            roverMissionService.MoveRover(roverMission, commands);

            roverMission.Rover.Location.X.Value.Should().Be(xFinal);
            roverMission.Rover.Location.Y.Value.Should().Be(yFinal);
            roverMission.Rover.Location.Direction.Should().Be(directionFinal);
        }
    }
}
