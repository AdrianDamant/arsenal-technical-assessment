using ArsenalTechnicalAssignment.Data.Data.Enums;
using ArsenalTechnicalAssignment.Data.Data.Models;
using ArsenalTechnicalAssignment.Portal.Models;
using System.Runtime.CompilerServices;

namespace ArsenalTechnicalAssignment.Portal.Extensions
{
    public static class PlayerExtensions
    {
        public static CreatePlayerModel ToCreatePlayerModel(this Player player) =>
            new CreatePlayerModel()
            {
                PlayerId = player.PlayerId,
                PlayerName = player.PlayerName,
                Position= player.Position,
                JerseyNumber = player.JerseyNumber,
                GoalsScored = player.GoalsScored
    };
    }
}
