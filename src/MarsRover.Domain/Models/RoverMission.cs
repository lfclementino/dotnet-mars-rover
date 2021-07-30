using MarsRover.Domain.Enums;
using MarsRover.Domain.Exceptions;
using System;

namespace MarsRover.Domain.Models
{
    public class RoverMission
    {

        public RoverMission(Floor floor, Rover rover)
        {
            Floor = floor;
            Rover = rover;

            try
            {
                ValidateLocationAndFloor(Floor, Rover.Location);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Floor Floor { get; }
        public Rover Rover { get; }

        private void ValidateLocationAndFloor(Floor floor, RoverLocation location)
        {
            try
            {
                var isValidDirection = IsValidDirection(location.Direction);
                var isValidX = IsValidX(floor.Width, location.X.Value);
                var isValidY = IsValidY(floor.Height, location.Y.Value);

                var result = isValidDirection && isValidX && isValidY;

                if(!result)
                {
                    throw new InvalidRoverMissionException(location.X.Value, location.Y.Value, location.Direction, floor.Width, floor.Height);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private bool IsValidDirection(Direction direction)
        {
            if (direction == Direction.West ||
                direction == Direction.East ||
                direction == Direction.North ||
                direction == Direction.South)
            {
                return true;
            }

            return false;
        }
        private bool IsValidX(int width, int x)
        {
            return x <= width;
        }
        private bool IsValidY(int height, int y)
        {
            return y <= height;
        }
    }
}
