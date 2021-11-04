using System;

namespace MarsRover.Domain.Exceptions
{
    public class OutOfBoundsException : Exception
    {
        public OutOfBoundsException(string message)
        : base($"Rover position Out of Bounds: {message}")
        { }
    }
}
