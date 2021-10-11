using MarsRover.Domain.Commands;
using MarsRover.Domain.Enums;
using MarsRover.Domain.Interfaces;
using MarsRover.Domain.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover.Domain.Services
{
    public class CommandsService : ICommandsService
    {
        private readonly ILogger<ICommandsService> _logger;
        private readonly CommandsOptions _commandsOptions;

        public CommandsService(ILogger<ICommandsService> logger, IOptions<CommandsOptions> commandsOptions)
        {
            _logger = logger;
            _commandsOptions = commandsOptions.Value ?? throw new ArgumentNullException(nameof(commandsOptions));
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
            catch (ArgumentNullException ex)
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
                    return new AdvanceCommand(_commandsOptions.AdvanceSteps);
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
