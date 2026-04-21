using System.Collections.ObjectModel;

namespace Sambas.Mobile.Models;

internal sealed class MatchGrouping : ObservableCollection<Match>
{
    public DateTime Timestamp { get; }

    public MatchGrouping(DateTime timestamp, IEnumerable<Match> matches)
        : base(matches)
    {
        Timestamp = timestamp;
    }
}
