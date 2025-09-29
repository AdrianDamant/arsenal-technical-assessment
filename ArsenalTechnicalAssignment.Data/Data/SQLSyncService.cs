using ArsenalTechnicalAssignment.Data.Data.Enums;
using ArsenalTechnicalAssignment.Data.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ArsenalTechnicalAssignment.Data.Data
{
    public class SQLSyncService : ISQLSyncService
    {
        private readonly SqlContext _sqlContext;

        public SQLSyncService(SqlContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public async Task<string> CreatePlayerAsync(string playerName, Position position, int jerseyNumber, int goalsScored)
        {
            try
            {
                await _sqlContext.Players.AddAsync(new()
                {
                    PlayerName = playerName,
                    Position = position,
                    JerseyNumber = jerseyNumber,
                    GoalsScored = goalsScored,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    Deleted = false
                });
                await _sqlContext.SaveChangesAsync();
                return "success";
            }
            catch (Exception ex) { return ex.ToString(); }
        }

        public async Task<List<Player>> GetPlayersAsync() => await _sqlContext.Players.Where(__ => !__.Deleted).AsNoTracking().OrderBy(__ => __.PlayerName).ToListAsync();

        public async Task<Player?> GetPlayersByJerseyNumberAsync(int jerseyNumber) => await _sqlContext.Players.Where(__ => !__.Deleted && __.JerseyNumber==jerseyNumber).AsNoTracking().OrderBy(__ => __.PlayerName).FirstOrDefaultAsync();

        public async Task<Player?> GetPlayerAsync(Guid playerId) => await _sqlContext.Players.FindAsync(playerId);

        public async Task<string> UpdatePlayerAsync(Guid playerId, string playerName, Position position, int jerseyNumber, int goalsScored)
        {
            try
            {
                var player = await _sqlContext.Players.FindAsync(playerId);
                if (player == null) return $"Exception - Player Not Found";

                player.PlayerName = playerName;
                player.Position = position;
                player.JerseyNumber = jerseyNumber;
                player.GoalsScored = goalsScored;
                player.DateModified = DateTime.UtcNow;
                _sqlContext.Players.Update(player);
                await _sqlContext.SaveChangesAsync();
                return "success";
            }
            catch (Exception ex) { return ex.ToString(); }
        }

        public async Task<string> DeletePlayerAsync(Guid playerId)
        {
            try
            {
                var player = await _sqlContext.Players.FindAsync(playerId);
                if (player == null) return $"Exception - Player Not Found";

                player.Deleted = true;
                _sqlContext.Players.Update(player);
                await _sqlContext.SaveChangesAsync();
                return "success";
            }
            catch (Exception ex) { return ex.ToString(); }
        }
    }
}
