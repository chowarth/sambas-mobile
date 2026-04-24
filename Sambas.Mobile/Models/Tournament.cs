namespace Sambas.Mobile.Models;

public sealed record Tournament(string Name, DateOnly StartDate, DateOnly EndDate)
{
    public Guid Id { get; set; } = Guid.Empty;

    private IList<Match> _matches = [];
    public IReadOnlyCollection<Match> Matches
        => _matches.AsReadOnly();

    public void AddMatch(Match match)
    {
        _matches.Add(match);
    }
}
