using MarsRover.Domain.Enums;
using MarsRover.Domain.Exceptions;
using MarsRover.Domain.Interfaces;
using MarsRover.Domain.Models;
using System;

namespace MarsRover.Domain.Commands
{
    public class AdvanceCommand : ICommand
    {
        public void Execute(Floor floor, Rover rover)
        {
            try
            {
                switch (rover.Location.Direction)
                {
                    case Direction.North:
                        if (rover.Location.Y.Value >= floor.Height)
                        {
                            throw new OutOfBoundsException();
                        }
                        rover.Location.Y.Increase();
                        break;
                    case Direction.East:
                        if (rover.Location.X.Value >= floor.Width)
                        {
                            throw new OutOfBoundsException();
                        }
                        rover.Location.X.Increase();
                        break;
                    case Direction.South:
                        rover.Location.Y.Decrease();
                        break;
                    case Direction.West:
                        rover.Location.X.Decrease();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
