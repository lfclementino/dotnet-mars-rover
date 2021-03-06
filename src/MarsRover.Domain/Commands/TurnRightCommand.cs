using MarsRover.Domain.Enums;
using MarsRover.Domain.Interfaces;
using MarsRover.Domain.Models;

namespace MarsRover.Domain.Commands
{
    public class TurnRightCommand : ICommand
    {
        public void Execute(Floor floor, Rover rover)
        {
            try
            {
                switch (rover.Location.Direction)
                {
                    case Direction.North:
                        rover.Location.Direction = Direction.East;
                        break;
                    case Direction.West:
                        rover.Location.Direction = Direction.North;
                        break;
                    case Direction.South:
                        rover.Location.Direction = Direction.West;
                        break;
                    case Direction.East:
                        rover.Location.Direction = Direction.South;
                        break;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
