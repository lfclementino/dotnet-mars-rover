using System;

namespace MarsRover.Domain.Exceptions
{
    public class OutOfBoundsException : Exception
    {
        public OutOfBoundsException()
        : base("Rover position Out of Bounds")
        { }
    }
}
