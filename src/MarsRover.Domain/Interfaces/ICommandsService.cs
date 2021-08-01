using System.Collections.Generic;

namespace MarsRover.Domain.Interfaces
{
    public interface ICommandsService
    {
        IList<ICommand> GetCommands(string commands);
    }
}
