using MarsRover.Domain.Models;

namespace MarsRover.Domain.Interfaces
{
    public interface IFloorService
    {
        Floor Create(int width, int height);
        bool Validate(Floor floor);
    }
}
