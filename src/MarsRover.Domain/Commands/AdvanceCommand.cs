using MarsRover.Domain.Enums;
using MarsRover.Domain.Interfaces;
using MarsRover.Domain.Models;
using System;

namespace MarsRover.Domain.Commands
{
    public class AdvanceCommand : ICommand
    {
        public bool Execute(Floor floor, Rover rover)
        {
            try
            {
                switch (rover.Location.Direction)
                {
                    case Direction.North:
                        if (rover.Location.Y >= floor.Height)
                        {
                            return false;
                        }
                        rover.Location.Y += 1;
                        break;
                    case Direction.East:
                        if (rover.Location.X >= floor.Width)
                        {
                            return false;
                        }
                        rover.Location.X += 1;
                        break;
                    case Direction.South:
                        if (rover.Location.Y <= 0)
                        {
                            return false;
                        }
                        rover.Location.Y -= 1;
                        break;
                    case Direction.West:
                        if (rover.Location.X <= 0)
                        {
                            return false;
                        }
                        rover.Location.X -= 1;
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
