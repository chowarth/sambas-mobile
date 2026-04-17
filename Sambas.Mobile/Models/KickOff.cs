namespace Sambas.Mobile.Models;

public sealed record KickOff(DateTime? Date, TimeSpan? Time)
{
    public static KickOff Default
        => new KickOff(null, null);
};
