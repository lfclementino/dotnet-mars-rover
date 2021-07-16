using FluentAssertions;
using MarsRover.Domain.Commands;
using MarsRover.Domain.Enums;
using MarsRover.Domain.Interfaces;
using MarsRover.Domain.Models;
using MarsRover.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MarsRover.Tests.Services
{
    public class RoverMissionServiceTests
    {
        private readonly Mock<ILogger<IRoverMissionService>> _mockLogger;
        private readonly Mock<IFloorService> _mockFloorService;
        private readonly Mock<ICommandsService> _mockCommandsService;

        public RoverMissionServiceTests()
        {
            _mockLogger = new Mock<ILogger<IRoverMissionService>>();
            _mockFloorService = new Mock<IFloorService>();
            _mockCommandsService = new Mock<ICommandsService>();
        }

        [Theory]
        [InlineData(100, 100, 40, 30)]
        [InlineData(100, 100, 42, 90)]
        public void Initialize_ValidFloorAndRover_ShouldReturnValidRoverMission(int width, int height, int x, int y)
        {
            _mockFloorService.Setup(x => x.Validate(It.IsAny<Floor>())).Returns(true);

            var testFloor = new Floor()
            {
                Width = width,
                Height = height
            };

            var testRover = new Rover();
            testRover.Location.Direction = Direction.North;
            testRover.Location.X = x;
            testRover.Location.Y = y;

            var roverMissionService = new RoverMissionService(_mockLogger.Object, _mockFloorService.Object, _mockCommandsService.Object);

            var roverMission = roverMissionService.Initialize(testFloor, testRover);

            roverMission.Should().NotBeNull();
            roverMission.Rover.Should().Be(testRover);
            roverMission.Floor.Should().Be(testFloor);
        }

        [Theory]
        [InlineData(100, 100, 400, 30)]
        [InlineData(100, 100, 42, 900)]
        public void Initialize_InvalidRover_ShouldReturnNullRoverMission(int width, int height, int x, int y)
        {
            _mockFloorService.Setup(x => x.Validate(It.IsAny<Floor>())).Returns(true);

            var testFloor = new Floor()
            {
                Width = width,
                Height = height
            };

            var testRover = new Rover();
            testRover.Location.Direction = Direction.North;
            testRover.Location.X = x;
            testRover.Location.Y = y;

            var roverMissionService = new RoverMissionService(_mockLogger.Object, _mockFloorService.Object, _mockCommandsService.Object);

            var roverMission = roverMissionService.Initialize(testFloor, testRover);

            roverMission.Should().BeNull();
        }

        [Theory]
        [InlineData(-100, 100, 40, 30)]
        [InlineData(100, -100, 42, 90)]
        public void Initialize_InvalidFloor_ShouldReturnNullRoverMission(int width, int height, int x, int y)
        {
            _mockFloorService.Setup(x => x.Validate(It.IsAny<Floor>())).Returns(false);

            var testFloor = new Floor()
            {
                Width = width,
                Height = height
            };

            var testRover = new Rover();
            testRover.Location.Direction = Direction.North;
            testRover.Location.X = x;
            testRover.Location.Y = y;

            var roverMissionService = new RoverMissionService(_mockLogger.Object, _mockFloorService.Object, _mockCommandsService.Object);

            var roverMission = roverMissionService.Initialize(testFloor, testRover);

            roverMission.Should().BeNull();
        }

        [Theory]
        [InlineData(100, 100, 40, 30)]
        [InlineData(100, 100, 42, 90)]
        public void ValidateLocationAndFloor_ValidFloorAndLocation_ShouldReturnTrue(int width, int height, int x, int y)
        {
            _mockFloorService.Setup(x => x.Validate(It.IsAny<Floor>())).Returns(false);

            var testFloor = new Floor()
            {
                Width = width,
                Height = height
            };

            var testRover = new Rover();
            testRover.Location.Direction = Direction.North;
            testRover.Location.X = x;
            testRover.Location.Y = y;

            var roverMissionService = new RoverMissionService(_mockLogger.Object, _mockFloorService.Object, _mockCommandsService.Object);

            var result = roverMissionService.ValidateLocationAndFloor(testFloor, testRover.Location);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(100, 100, 140, 30)]
        [InlineData(100, 100, 42, 290)]
        public void ValidateLocationAndFloor_ValidFloorAndInvalidLocation_ShouldReturnFalse(int width, int height, int x, int y)
        {
            _mockFloorService.Setup(x => x.Validate(It.IsAny<Floor>())).Returns(true);

            var testFloor = new Floor()
            {
                Width = width,
                Height = height
            };

            var testRover = new Rover();
            testRover.Location.Direction = Direction.North;
            testRover.Location.X = x;
            testRover.Location.Y = y;

            var roverMissionService = new RoverMissionService(_mockLogger.Object, _mockFloorService.Object, _mockCommandsService.Object);

            var result = roverMissionService.ValidateLocationAndFloor(testFloor, testRover.Location);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(100, 100, 40, 30, Direction.South, Command.MoveForward)]
        [InlineData(100, 100, 42, 90, Direction.North, Command.MoveForward)]
        public void MoveRover_ValidFloorAndLocation_ShouldReturnTrue(int width, int height, int x, int y, Direction direction, Command command)
        {
            _mockFloorService.Setup(x => x.Validate(It.IsAny<Floor>())).Returns(true);

            _mockCommandsService.Setup(x => x.GetCommand(It.IsAny<Command>())).Returns(new AdvanceCommand());

            var testFloor = new Floor()
            {
                Width = width,
                Height = height
            };

            var testRover = new Rover();
            testRover.Location.Direction = direction;
            testRover.Location.X = x;
            testRover.Location.Y = y;

            var roverMissionService = new RoverMissionService(_mockLogger.Object, _mockFloorService.Object, _mockCommandsService.Object);

            var roverMission = roverMissionService.Initialize(testFloor, testRover);

            var result = roverMissionService.MoveRover(roverMission, command);

            result.Should().BeTrue();
        }
    }
}
