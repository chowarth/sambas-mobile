namespace Sambas.Mobile.Models;

public sealed record Goal(Team ScoringTeam, Player ScoredBy, DateTimeOffset ScoredAtUtc);
