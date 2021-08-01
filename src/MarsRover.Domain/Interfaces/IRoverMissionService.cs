using MarsRover.Domain.Models;

namespace MarsRover.Domain.Interfaces
{
    public interface IRoverMissionService
    {
        void MoveRover(RoverMission roverMission, string inputCommands);
    }
}
