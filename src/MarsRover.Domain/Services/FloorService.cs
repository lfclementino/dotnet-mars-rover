using MarsRover.Domain.Interfaces;
using MarsRover.Domain.Models;
using Microsoft.Extensions.Logging;
using System;

namespace MarsRover.Domain.Services
{
    public class FloorService : IFloorService
    {
        private readonly ILogger<IFloorService> _logger;

        public FloorService(ILogger<IFloorService> logger)
        {
            _logger = logger;
        }

        public Floor Create(int width, int height)
        {
            try
            {
                var floor = new Floor()
                {
                    Width = width,
                    Height = height
                };

                var valid = Validate(floor);
                if (valid)
                {
                    return floor;
                }
                _logger.LogError("Create floor: Invalid floor width/height");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create floor: {ex.Message}");
                return null;
            }
        }

        public bool Validate(Floor floor)
        {
            try
            {
                if (floor != null && floor.Width > 0 && floor.Height > 0)
                {
                    return true;
                }
                _logger.LogError("Validate Floor: Invalid Floor");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Validate Floor: {ex.Message}");
                return false;
            }
        }
    }
}
