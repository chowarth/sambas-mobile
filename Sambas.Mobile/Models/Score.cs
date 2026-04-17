namespace Sambas.Mobile.Models;

public sealed record Score(int? HomeTeamScore, int? AwayTeamScore)
{
    public static Score Default
        => new Score(null, null);
};
