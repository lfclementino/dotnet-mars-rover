using MarsRover.Domain.Commands;
using MarsRover.Domain.Enums;
using MarsRover.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover.Domain.Services
{
    public class CommandsService : ICommandsService
    {
        private readonly ILogger<ICommandsService> _logger;

        public CommandsService(ILogger<ICommandsService> logger)
        {
            _logger = logger;
        }

        public IList<ICommand> GetCommands(string commands)
        {
            try
            {
                if (string.IsNullOrEmpty(commands))
                {
                    throw new ArgumentNullException("Get Commands: input commands can't be null or empty.");
                }

                var commandsResult = commands.ToUpper().Select(cmd => GetCommand(cmd))
                                                       .OfType<ICommand>()
                                                       .ToList();
                return commandsResult;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"Get Commands: {ex.Message}");
            }
        }

        private ICommand GetCommand(char command)
        {
            switch ((Command)command)
            {
                case Command.MoveForward:
                    return new AdvanceCommand();
                case Command.RotateLeft:
                    return new TurnLeftCommand();
                case Command.RotateRight:
                    return new TurnRightCommand();
                default:
                    _logger.LogError($"Get Commands: Invalid command '{command}'");
                    return null;
            }
        }
    }
}
