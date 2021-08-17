using System;

namespace MarsRover.Domain.Exceptions
{
    public class InvalidAxisValueException : Exception
    {
        public InvalidAxisValueException()
            : base()
        { }

        public InvalidAxisValueException(string value)
            : base($"Invalid Axis value: {value}")
        { }
    }
}
