using System.ComponentModel.DataAnnotations;

namespace ArsenalTechnicalAssignment.Data.Data.Models
{
    public class Fixture
    {
        [Key]
        public Guid FixtureId { get; set; }
        public DateTime FixtureDateTime { get; set; }
        public Guid HomeFootballClubId { get; set; }
        public virtual required FootballClub HomeFootballClub { get; set; }
        public Guid AwayFootballClubId { get; set; }
        public virtual required FootballClub AwayFootballClub { get; set; }
        public string? Venue { get; set; }
    }
}
