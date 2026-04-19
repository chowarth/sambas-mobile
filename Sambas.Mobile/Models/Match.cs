namespace Sambas.Mobile.Models;

public sealed record Match(Team HomeTeam, Team AwayTeam, KickOff KickOff, Score Score, IReadOnlyCollection<Goal> Goals)
{
    public Guid Id { get; set; } = Guid.Empty;
}
