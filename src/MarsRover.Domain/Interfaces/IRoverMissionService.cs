using MarsRover.Domain.Enums;
using MarsRover.Domain.Models;

namespace MarsRover.Domain.Interfaces
{
    public interface IRoverMissionService
    {
        RoverMission Initialize(Floor floor, Rover rover);
        bool ValidateLocationAndFloor(Floor floor, RoverLocation location);
        bool MoveRover(RoverMission roverMission, Command command);
    }
}
