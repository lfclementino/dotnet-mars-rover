using System;

namespace MarsRover.Domain.Exceptions
{
    public class InvalidFloorSizeException : Exception
    {
        public InvalidFloorSizeException(string dimensionName)
            : base($"Invalid {dimensionName} Floor value")
        { }
    }
}
