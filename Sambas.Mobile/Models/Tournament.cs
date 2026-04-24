namespace Sambas.Mobile.Models;

public sealed record Tournament(string Name, DateOnly StartDate, DateOnly EndDate)
{
    public Guid Id { get; init; } = Guid.Empty;
}
