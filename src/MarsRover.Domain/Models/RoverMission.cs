namespace MarsRover.Domain.Models
{
    public class RoverMission
    {
        public RoverMission()
        { }

        public RoverMission(Floor floor, Rover rover)
        {
            Floor = floor;
            Rover = rover;
        }
        public Floor Floor { get; set; }
        public Rover Rover { get; set; }
    }
}
