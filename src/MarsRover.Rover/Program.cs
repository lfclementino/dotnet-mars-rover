using MarsRover.Domain.Enums;
using MarsRover.Domain.Interfaces;
using MarsRover.Domain.Models;
using MarsRover.Domain.Options;
using MarsRover.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace MarsRover.Rover
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddLogging(b =>
                {
                    b.AddConsole();
                    b.SetMinimumLevel(LogLevel.Debug);
                })
                // Configure Options
                .Configure<CommandsOptions>(configuration.GetSection("Commands"))
                // Services are scoped so we use the same object per request 
                .AddScoped<ICommandsService, CommandsService>()
                .AddScoped<IRoverMissionService, RoverMissionService>()
                .BuildServiceProvider();

            RunRoverMission(serviceProvider, 1, 1, 4, 3, Direction.North, "AAAALAARARAARAAAAArf");
            RunRoverMission(serviceProvider, 100, 10, 4, 3, Direction.West, "eWAAAAAAAALAARARAARAAAAAr");
            RunRoverMission(serviceProvider, 100, 100, 40, 30, Direction.South, "AAAALAARARAARAAAAArAAAALAARARAARAAAAArAAAALAARARAARAAAAAr");

            Console.ReadLine();
        }

        static void RunRoverMission(ServiceProvider serviceProvider, int width, int height, int x, int y, Direction initialDirection, string inputCommands)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
                logger.LogInformation("*** Starting Mission ***");

                try
                {
                    var roverMissionService = scope.ServiceProvider.GetService<IRoverMissionService>();

                    // Create Rover and Square Floor
                    var floor = new Floor(width, height);

                    var rover = new Domain.Models.Rover();
                    rover.Location = new RoverLocation()
                    {
                        Direction = initialDirection,
                        X = new Axis(x),
                        Y = new Axis(y)
                    };

                    // Initialize Mission
                    var roverMission = new RoverMission(floor, rover);

                    logger.LogInformation($"Mission created with Floor W:{floor.Width} H:{floor.Height} " +
                                           $"and Rover at initial position X:{rover.Location.X.Value} Y:{rover.Location.Y.Value} Dir:{rover.Location.Direction}");

                    roverMissionService.MoveRover(roverMission, inputCommands);
                }
                catch (Exception ex)
                {
                    logger.LogError($"Mission Error: {ex.Message}");
                }
                finally
                {
                    logger.LogInformation("*** End of Mission ***");
                }
            }
        }
    }
}
