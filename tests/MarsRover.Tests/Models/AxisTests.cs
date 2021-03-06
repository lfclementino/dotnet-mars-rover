using FluentAssertions;
using MarsRover.Domain.Exceptions;
using MarsRover.Domain.Models;
using Xunit;

namespace MarsRover.Tests.Models
{
    public class AxisTests
    {
        [Theory]
        [InlineData(100)]
        [InlineData(103620)]
        [InlineData(1)]
        public void NewAxis_ValidValue_ShouldSetCorrectValuesOnNewModel(int value)
        {
            var axis = new Axis(value);

            Assert.NotNull(axis);
            Assert.Equal(value, axis.Value);
        }

        [Theory]
        [InlineData(-100)]
        [InlineData(-12344200)]
        public void NewAxis_InvalidValue_ShouldThrowOutOfBoundsException(int value)
        {
            Assert.Throws<InvalidAxisValueException>(() => new Axis(value));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5346643)]
        public void Increase_ShouldSetCorrecValue(int value)
        {
            var step = 1;
            var axis = new Axis(value);

            axis.Increase(step);

            axis.Value.Should().Be(value+1);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5346643)]
        public void Decrease_ShouldSetCorrecValue(int value)
        {
            var step = 1;
            var axis = new Axis(value);

            axis.Decrease(step);

            axis.Value.Should().Be(value - 1);
        }

        [Theory]
        [InlineData(0)]
        public void Decrease_ZeroValue_ShouldThrowOutOfBoundsException(int value)
        {
            var step = 1;
            var axis = new Axis(value);
            
            Assert.Throws<InvalidAxisValueException>(() => axis.Decrease(step));
        }
    }
}
