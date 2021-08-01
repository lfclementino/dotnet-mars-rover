using MarsRover.Domain.Exceptions;
using MarsRover.Domain.Interfaces;
using MarsRover.Domain.Models;
using Microsoft.Extensions.Logging;
using System;

namespace MarsRover.Domain.Services
{
    public class RoverMissionService : IRoverMissionService
    {
        private readonly ILogger<IRoverMissionService> _logger;
        private readonly ICommandsService _commandsService;

        public RoverMissionService(ILogger<IRoverMissionService> logger, ICommandsService commandsService)
        {
            _logger = logger;
            _commandsService = commandsService;
        }

        public void MoveRover(RoverMission roverMission, string inputCommands)
        {
            try
            {
                var commands = _commandsService.GetCommands(inputCommands);
                foreach (ICommand command in commands)
                {
                    command.Execute(roverMission.Floor, roverMission.Rover);
                }

                _logger.LogInformation($"[FINAL POSITION] True, {roverMission.Rover.Location.Direction}, ({roverMission.Rover.Location.X.Value},{roverMission.Rover.Location.Y.Value})");
            }
            catch (OutOfBoundsException ex)
            {
                _logger.LogWarning($"[WARNING] ROVER OUT OF BOUNDS: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
