using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Sambas.Mobile.Mvvm;
using UXDivers.Popups.Services;

namespace Sambas.Mobile.Features.Games;

internal class GamesPageViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;

    public ICommand AddGameCommand { get; init; }

    public GamesPageViewModel(
        IPopupService popupService,
        ILogger<GamesPageViewModel> logger)
        : base(logger)
    {
        _popupService = popupService;

        AddGameCommand = new Command(async () => await AddGameAsync());
    }

    private async Task AddGameAsync()
    {
        await _popupService.PushAsync<EditGamePage>();
    }
}
