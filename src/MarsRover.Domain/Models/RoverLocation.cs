using MarsRover.Domain.Enums;

namespace MarsRover.Domain.Models
{
    public class RoverLocation
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }
    }
}
