using System;

namespace MarsRover.Domain.Models
{
    public class Rover
    {
        public Rover()
        {
            Id = Guid.NewGuid();
            Location = new RoverLocation();
        }
        public Guid Id { get; set; }
        public RoverLocation Location { get; set; } 
    }
}
