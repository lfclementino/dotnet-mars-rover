using MarsRover.Domain.Enums;
using MarsRover.Domain.Interfaces;
using MarsRover.Domain.Models;
using Microsoft.Extensions.Logging;
using System;

namespace MarsRover.Domain.Services
{
    public class RoverMissionService : IRoverMissionService
    {
        private readonly ILogger<IRoverMissionService> _logger;
        private readonly IFloorService _floorService;
        private readonly ICommandsService _commandsService;

        public RoverMissionService(ILogger<IRoverMissionService> logger, IFloorService floorService, ICommandsService commandsService)
        {
            _logger = logger;
            _floorService = floorService;
            _commandsService = commandsService;
        }

        public RoverMission Initialize(Floor floor, Rover rover)
        {
            try
            {
                var validFloor = _floorService.Validate(floor);
                if (!validFloor)
                {
                    _logger.LogError("Initialize Mission: Invalid floor.");
                    return null;
                }

                var validFloorForRover = ValidateLocationAndFloor(floor, rover.Location);
                if (!validFloorForRover)
                {
                    _logger.LogError($"Initialize Mission: Invalid Rover position X:{rover.Location.X} - Y:{rover.Location.Y} - Direction:{rover.Location.Direction} for the given floor Width:{floor.Width} - Height:{floor.Height}");
                    return null;
                }

                var roverMission = new RoverMission(floor, rover);

                return roverMission;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Initialize Rover Mission: {ex.Message}");
                return null;
            }
        }

        public bool ValidateLocationAndFloor(Floor floor, RoverLocation location)
        {
            try
            {
                var isValidDirection = IsValidDirection(location.Direction);
                var isValidX = IsValidX(floor.Width, location.X);
                var isValidY = IsValidY(floor.Height, location.Y);

                var result = isValidDirection && isValidX && isValidY;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Validate location And floor for Rover: {ex.Message}");
                return false;
            }
        }

        public bool MoveRover(RoverMission roverMission, Command command)
        {
            try
            {
                var cmd = _commandsService.GetCommand(command);
                var result = cmd.Execute(roverMission.Floor, roverMission.Rover);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Move Rover: {ex.Message}");
                return false;
            }
        }

        private bool IsValidDirection(Direction direction)
        {
            if (direction == Direction.West ||
                direction == Direction.East ||
                direction == Direction.North ||
                direction == Direction.South)
            {
                return true;
            }

            return false;
        }
        private bool IsValidX(int width, int xPosition)
        {
            return xPosition >= 0 && xPosition <= width;
        }
        private bool IsValidY(int height, int yPosition)
        {
            return yPosition >= 0 && yPosition <= height;
        }
    }
}
