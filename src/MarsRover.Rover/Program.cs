using MarsRover.Domain.Enums;
using MarsRover.Domain.Interfaces;
using MarsRover.Domain.Models;
using MarsRover.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace MarsRover.Rover
{
    class Program
    {
        private static ILogger<Program> _logger;
        private static IFloorService _floorService;
        private static ICommandsService _commandsService;
        private static IRoverMissionService _roverMissionService;

        static void Main(string[] args)
        {

            var serviceProvider = new ServiceCollection()
                .AddLogging(b =>
                {
                    b.AddConsole();
                    b.SetMinimumLevel(LogLevel.Debug);
                })
                // Services are scoped so we use the same object per request 
                .AddScoped<ICommandsService, CommandsService>()
                .AddScoped<IFloorService, FloorService>()
                .AddScoped<IRoverMissionService, RoverMissionService>()
                .BuildServiceProvider();

            _logger = serviceProvider.GetService<ILogger<Program>>(); ;
            _floorService = serviceProvider.GetService<IFloorService>();
            _commandsService = serviceProvider.GetRequiredService<ICommandsService>();
            _roverMissionService = serviceProvider.GetService<IRoverMissionService>();

            RunRoverMission(1, 1, 4, 3, Direction.North, "AAAALAARARAARAAAAArf");
            RunRoverMission(100, 10, 4, 3, Direction.West, "AAAAAAAALAARARAARAAAAAr");
            RunRoverMission(100, 100, 40, 30, Direction.South, "AAAALAARARAARAAAAArAAAALAARARAARAAAAArAAAALAARARAARAAAAAr");

            Console.ReadLine();
        }

        static void RunRoverMission(int width, int height, int x, int y, Direction initialDirection, string inputCommands)
        {
            try
            {
                // Create Rover and Square Floor
                var floor = _floorService.Create(width, height);
                var rover = new Domain.Models.Rover();
                rover.Location = new RoverLocation()
                {
                    Direction = initialDirection,
                    X = x,
                    Y = y
                };

                if (floor == null || rover == null) return;

                // Initialize Mission
                var roverMission = _roverMissionService.Initialize(floor, rover);
                if (roverMission == null) return;

                _logger.LogInformation($"Mission created with Floor W:{floor.Width} H:{floor.Height} " +
                                       $"and Rover at initial position X:{rover.Location.X} Y:{rover.Location.Y} Dir:{rover.Location.Direction}");

                var commands = _commandsService.GetCommands(inputCommands);
                var result = true;
                foreach (Command command in commands)
                {
                    result = _roverMissionService.MoveRover(roverMission, command);
                    if (!result) break;
                }

                if (result)
                {
                    _logger.LogInformation($"[FINAL POSITION] {result}, {rover.Location.Direction}, ({rover.Location.X},{rover.Location.Y})");
                }
                else
                {
                    _logger.LogWarning($"[WARNING] {result}, OUT OF BOUNDS");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Mission Error: {ex.Message}");
            }
        }
    }
}
