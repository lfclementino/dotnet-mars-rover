using MarsRover.Domain.Exceptions;
using MarsRover.Domain.Models;
using Xunit;

namespace MarsRover.Tests.Models
{
    public class FloorTests
    {
        [Theory]
        [InlineData(100, 100)]
        [InlineData(103620, 1347850)]
        [InlineData(1, 1)]
        public void NewFloor_ValidWidthHeight_ShouldSetCorrectValuesOnNewModel(int width, int height)
        {
            var floor = new Floor(width, height);

            Assert.NotNull(floor);
            Assert.Equal(width, floor.Width);
            Assert.Equal(height, floor.Height);
        }

        [Theory]
        [InlineData(-100, 100)]
        [InlineData(-100, -100)]
        [InlineData(100, -100)]
        [InlineData(0, -100)]
        public void NewFloor_InvalidWidthHeight_ShouldThrowInvalidFloorSizeException(int width, int height)
        {
            Assert.Throws<InvalidFloorSizeException>(() => new Floor(width, height));
        }
    }
}
