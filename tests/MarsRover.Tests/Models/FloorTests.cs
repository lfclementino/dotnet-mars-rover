using MarsRover.Domain.Models;
using Xunit;

namespace MarsRover.Tests.Models
{
    public class FloorTests
    {
        [Fact]
        public void ShouldCreateNewModel()
        {
            var floor = new Floor();

            Assert.NotNull(floor);
        }

        [Fact]
        public void ShouldSetCorrectValuesOnNewModel()
        {
            var floor = new Floor()
            {
                Height = 100,
                Width = 100
            };

            Assert.NotNull(floor);
            Assert.Equal(100, floor.Width);
            Assert.Equal(100, floor.Height);
        }
    }
}
