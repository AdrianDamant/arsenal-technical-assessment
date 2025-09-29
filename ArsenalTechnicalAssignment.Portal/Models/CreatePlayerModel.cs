using ArsenalTechnicalAssignment.Data.Data.Enums;

namespace ArsenalTechnicalAssignment.Portal.Models
{
    public class CreatePlayerModel
    {
        public Guid PlayerId { get; set; }
        public required string PlayerName { get; set; }
        public Position Position { get; set; }
        public int JerseyNumber { get; set; }
        public int GoalsScored { get; set; }
        public List<Position> Positions { get; set; } = new() { Position.Defender, Position.Forward, Position.Goalkeeper, Position.Midfielder };
    }
}
