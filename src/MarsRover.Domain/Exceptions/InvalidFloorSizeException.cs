using System;

namespace MarsRover.Domain.Exceptions
{
    public class InvalidFloorSizeException : Exception
    {
        public InvalidFloorSizeException()
            : base()
        { }

        public InvalidFloorSizeException(string dimension)
            : base($"Invalid Floor {dimension}")
        { }
    }
}
