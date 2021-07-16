using FluentAssertions;
using MarsRover.Domain.Interfaces;
using MarsRover.Domain.Models;
using MarsRover.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MarsRover.Tests.Services
{
    public class FloorServiceTests : IDisposable
    {
        private readonly Mock<ILogger<IFloorService>> _mockLogger;
        private readonly IFloorService _floorService;

        public FloorServiceTests()
        {
            _mockLogger = new Mock<ILogger<IFloorService>>();
            _floorService = new FloorService(_mockLogger.Object);
        }

        public void Dispose()
        {
            _mockLogger.VerifyAll();
        }

        [Theory]
        [InlineData(null)]
        public void Validate_IsNull_ShouldReturnFalse(Floor floor)
        {
            var result = _floorService.Validate(floor);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(0, 0)]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(10, -1)]
        [InlineData(-1, 10)]
        public void Validate_InvalidWidthHeight_ShouldReturnFalse(int width, int height)
        {
            var testFloor = new Floor()
            {
                Width = width,
                Height = height
            };

            var result = _floorService.Validate(testFloor);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(43, 132)]
        [InlineData(32564577, 1234)]
        [InlineData(1, 1)]
        [InlineData(1234, 1)]
        [InlineData(1, 1234)]
        public void Validate_ValidWidthHeight_ShouldReturnTrue(int width, int height)
        {
            var testFloor = new Floor()
            {
                Width = width,
                Height = height
            };

            var result = _floorService.Validate(testFloor);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(5, 5)]
        [InlineData(2, 7)]
        [InlineData(7, 2)]
        [InlineData(100, 100)]
        public void Create_ValidWidthHeight_ShouldReturnFloorWithSameValues(int width, int height)
        {
            var floor = _floorService.Create(width, height);

            floor.Should().NotBeNull();
            floor.Width.Should().Equals(width);
            floor.Height.Should().Equals(height);
        }

        [Theory]
        [InlineData(-5, -5)]
        [InlineData(-1, -1)]
        [InlineData(-32427, 200)]
        [InlineData(0, 100)]
        [InlineData(0, 0)]
        public void Create_InvalidWidthHeight_ShouldReturnNullFloor(int width, int height)
        {
            var floor = _floorService.Create(width, height);

            floor.Should().BeNull();
        }
    }
}
