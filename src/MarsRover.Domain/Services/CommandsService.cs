using MarsRover.Domain.Commands;
using MarsRover.Domain.Enums;
using MarsRover.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MarsRover.Domain.Services
{
    public class CommandsService : ICommandsService
    {
        private readonly ILogger<ICommandsService> _logger;

        public CommandsService(ILogger<ICommandsService> logger)
        {
            _logger = logger;
        }

        public List<Command> GetCommands(string commands)
        {
            try
            {
                if (commands == null)
                {
                    _logger.LogError("GetCommands: commands is null.");
                    return null;
                }

                var commandsResult = new List<Command>();
                var commandsArray = commands.ToUpper().ToCharArray();
                foreach (var command in commandsArray)
                {
                    switch (command)
                    {
                        case (char)Command.MoveForward:
                            commandsResult.Add(Command.MoveForward);
                            break;
                        case (char)Command.RotateLeft:
                            commandsResult.Add(Command.RotateLeft);
                            break;
                        case (char)Command.RotateRight:
                            commandsResult.Add(Command.RotateRight);
                            break;
                        default:
                            _logger.LogError($"Get Commands: Invalid command '{command}'");
                            break;
                    }
                }

                return commandsResult;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Commands: {ex.Message}");
                return null;
            }
        }

        public ICommand GetCommand(Command command)
        {
            switch (command)
            {
                case Command.MoveForward:
                    return new AdvanceCommand();
                case Command.RotateLeft:
                    return new TurnLeftCommand();
                case Command.RotateRight:
                    return new TurnRightCommand();
                default:
                    return null;
            }
        }
    }
}
