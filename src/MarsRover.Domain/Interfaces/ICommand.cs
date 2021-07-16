using MarsRover.Domain.Models;

namespace MarsRover.Domain.Interfaces
{
    public interface ICommand
    {
        bool Execute(Floor floor, Rover rover);
    }
}
