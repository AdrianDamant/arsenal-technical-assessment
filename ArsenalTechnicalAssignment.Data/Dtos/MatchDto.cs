namespace ArsenalTechnicalAssignment.Data.Dtos
{
    public class MatchDto
    {
        public int Id { get; set; }
        public DateTime UtcDate { get; set; }
        public string? Status { get; set; }
        public string? Stage { get; set; }
        public DateTime LastUpdated { get; set; }
        public ScoreDto? Score { get; set; }
        public TeamDto? AwayTeam { get; set; }
        public TeamDto? HomeTeam { get; set; }
    }
}
