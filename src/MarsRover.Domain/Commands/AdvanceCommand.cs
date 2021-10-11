using MarsRover.Domain.Enums;
using MarsRover.Domain.Exceptions;
using MarsRover.Domain.Interfaces;
using MarsRover.Domain.Models;
using System;

namespace MarsRover.Domain.Commands
{
    public class AdvanceCommand : ICommand
    {
        private int _step;

        public AdvanceCommand(int step)
        {
            _step = step;
        }


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
                        rover.Location.Y.Increase(_step);
                        break;
                    case Direction.East:
                        if (rover.Location.X.Value >= floor.Width)
                        {
                            throw new OutOfBoundsException();
                        }
                        rover.Location.X.Increase(_step);
                        break;
                    case Direction.South:
                        rover.Location.Y.Decrease(_step);
                        break;
                    case Direction.West:
                        rover.Location.X.Decrease(_step);
                        break;
                }
            }
            catch (InvalidAxisValueException ex)
            {
                throw new OutOfBoundsException();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
