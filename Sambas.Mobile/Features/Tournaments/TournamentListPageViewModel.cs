using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Sambas.Mobile.Models;
using Sambas.Mobile.Mvvm;
using UXDivers.Popups.Services;

namespace Sambas.Mobile.Features.Tournaments;

internal class TournamentListPageViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;

    public ICommand AddTournamentCommand { get; init; }

    public TournamentListPageViewModel(
        IPopupService popupService,
        ILogger<TournamentListPageViewModel> logger)
        : base(logger)
    {
        _popupService = popupService;

        AddTournamentCommand = new Command(async () => await AddTournamentAsync());
    }

    private async Task AddTournamentAsync()
    {
        await _popupService.PushAsync<EditTournamentDetailsPopup, Tournament>();
    }
}
