using MarsRover.Domain.Enums;

namespace MarsRover.Domain.Models
{
    public class RoverLocation
    {
        public Axis X { get; set; }
        public Axis Y { get; set; }
        public Direction Direction { get; set; }
    }
}
