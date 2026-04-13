using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Sambas.Mobile.Mvvm;

namespace Sambas.Mobile.Features.Games;

internal class GamesPageViewModel : BaseViewModel
{
    public ICommand AddGameCommand { get; init; }

    public GamesPageViewModel(ILogger<GamesPageViewModel> logger)
        : base(logger)
    {
        AddGameCommand = new Command(async () => await AddGameAsync());
    }

    private async Task AddGameAsync()
    {
        // TODO: Implement the logic to add a new game, such as navigating to a new page or showing a dialog.
    }
}
