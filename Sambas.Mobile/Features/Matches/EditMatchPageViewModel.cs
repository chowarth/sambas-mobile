using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Sambas.Mobile.Models;
using Sambas.Mobile.Mvvm;
using Shiny.DocumentDb;
using UXDivers.Popups;

namespace Sambas.Mobile.Features.Matches;

internal class EditMatchPageViewModel : BaseViewModel, IPopupViewModel
{
    private readonly IDocumentStore _store;

    private Team _team = new Team(Guid.Empty, "", "", []);
    public Team Team
    {
        get => _team;
        set => SetProperty(ref _team, value);
    }

    private ObservableCollection<Player> _squad = [
        new Player("", ""),
        new Player("", "")
    ];

    public ObservableCollection<Player> Squad
    {
        get => _squad;
        set => SetProperty(ref _squad, value);
    }

    public ICommand SaveTeamCommand { get; init; }

    public EditMatchPageViewModel(
        IDocumentStore store,
        ILogger<BaseViewModel> logger)
        : base(logger)
    {
        _store = store;

        SaveTeamCommand = new Command(async () => await SaveTeamAsync());
    }

    private async Task SaveTeamAsync()
    {
        await _store.Insert(Team);
    }

    Task IPopupViewModel.OnPopupNavigatedAsync(IReadOnlyDictionary<string, object?> parameters)
    {
        return Task.CompletedTask;
    }
}
