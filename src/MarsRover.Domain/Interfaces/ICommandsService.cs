using MarsRover.Domain.Enums;
using System.Collections.Generic;

namespace MarsRover.Domain.Interfaces
{
    public interface ICommandsService
    {
        List<Command> GetCommands(string commands);
    }
}
