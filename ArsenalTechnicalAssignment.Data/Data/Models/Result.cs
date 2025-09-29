using System.ComponentModel.DataAnnotations;

namespace ArsenalTechnicalAssignment.Data.Data.Models
{
    public class Result
    {
        [Key]
        public Guid ResultId { get; set; }
        public virtual required Fixture Fixture { get; set; }
        public Guid FixtureId { get; set; }
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
    }
}
