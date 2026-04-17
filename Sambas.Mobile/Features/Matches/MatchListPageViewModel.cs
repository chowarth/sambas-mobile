using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Sambas.Mobile.Models;
using Sambas.Mobile.Mvvm;
using UXDivers.Popups.Services;

namespace Sambas.Mobile.Features.Matches;

internal class MatchListPageViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;

    public ICommand AddGameCommand { get; init; }

    public MatchListPageViewModel(
        IPopupService popupService,
        ILogger<MatchListPageViewModel> logger)
        : base(logger)
    {
        _popupService = popupService;

        AddGameCommand = new Command(async () => await AddGameAsync());
    }

    private async Task AddGameAsync()
    {
        var createdMatch = await _popupService.PushAsync<EditMatchDetailsPopup, Match>();
    }
}
