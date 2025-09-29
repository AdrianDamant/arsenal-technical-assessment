using System.ComponentModel.DataAnnotations;

namespace ArsenalTechnicalAssignment.Data.Data.Models
{
    public class FootballClub
    {
        [Key]
        public Guid FootballClubId { get; set; }
        public required string Name { get; set; }
        public string? CoachName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool Deleted { get; set; }
    }
}
