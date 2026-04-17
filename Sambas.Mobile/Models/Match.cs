namespace Sambas.Mobile.Models;

public sealed record Match(Guid Id, Team HomeTeam, Team AwayTeam, KickOff KickOff, Score Score, IReadOnlyCollection<Goal> Goals);
