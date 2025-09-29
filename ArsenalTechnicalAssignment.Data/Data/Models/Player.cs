using ArsenalTechnicalAssignment.Data.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace ArsenalTechnicalAssignment.Data.Data.Models
{
    public class Player
    {
        [Key]
        public Guid PlayerId { get; set; }
        public required string PlayerName { get; set; }
        public Position Position { get; set; }
        public int JerseyNumber { get; set; }
        public int GoalsScored { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool Deleted { get; set; }
    }
}
