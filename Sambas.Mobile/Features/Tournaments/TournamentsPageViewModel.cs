using Microsoft.Extensions.Logging;
using Sambas.Mobile.Mvvm;

namespace Sambas.Mobile.Features.Tournaments;

internal class TournamentsPageViewModel : BaseViewModel
{
    public TournamentsPageViewModel(ILogger<TournamentsPageViewModel> logger)
        : base(logger)
    {
    }
}
