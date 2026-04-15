using Microsoft.Extensions.Logging;
using Sambas.Mobile.Mvvm;

namespace Sambas.Mobile.Features.Tournaments;

internal class TournamentListPageViewModel : BaseViewModel
{
    public TournamentListPageViewModel(ILogger<TournamentListPageViewModel> logger)
        : base(logger)
    {
    }
}
