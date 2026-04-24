using System.Collections.ObjectModel;

namespace Sambas.Mobile.Models;

internal sealed class DateGrouping<T> : ObservableCollection<T>
{
    public DateTime Timestamp { get; }

    public DateGrouping(DateTime timestamp, IEnumerable<T> items)
        : base(items)
    {
        Timestamp = timestamp;
    }
}
