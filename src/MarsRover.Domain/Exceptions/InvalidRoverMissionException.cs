using MarsRover.Domain.Enums;
using System;

namespace MarsRover.Domain.Exceptions
{
    public class InvalidRoverMissionException : Exception
    {
        public InvalidRoverMissionException(int roverX, int roverY, Direction roverDirection, int floorWidth, int floorHeight)
            : base($"Initialize Mission: Invalid Rover position X:{roverX} Y:{roverY} Dir:{roverDirection} for the given floor W:{floorWidth} H:{floorHeight}")
        { }
    }
}
