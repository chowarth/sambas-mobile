namespace Sambas.Mobile.Models;

public sealed record Tournament(string Name, DateOnly StartDate, DateOnly EndDate, IList<Match> Matches)
{
    public Guid Id { get; set; } = Guid.Empty;
}
