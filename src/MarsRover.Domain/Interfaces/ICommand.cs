using MarsRover.Domain.Models;

namespace MarsRover.Domain.Interfaces
{
    public interface ICommand
    {
        void Execute(Floor floor, Rover rover);
    }
}
