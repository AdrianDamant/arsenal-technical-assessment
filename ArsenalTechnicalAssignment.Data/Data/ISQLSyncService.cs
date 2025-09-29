using ArsenalTechnicalAssignment.Data.Data.Enums;
using ArsenalTechnicalAssignment.Data.Data.Models;

namespace ArsenalTechnicalAssignment.Data.Data
{
    public interface ISQLSyncService
    {
        Task<string> CreatePlayerAsync(string playerName, Position position, int jerseyNumber, int goalsScored);
        Task<List<Player>> GetPlayersAsync();
        Task<string> UpdatePlayerAsync(Guid playerId, string playerName, Position position, int jerseyNumber, int goalsScored);
        Task<string> DeletePlayerAsync(Guid playerId);
        Task<Player?> GetPlayerAsync(Guid playerId);
        Task<Player?> GetPlayersByJerseyNumberAsync(int jerseyNumber);
    }
}
