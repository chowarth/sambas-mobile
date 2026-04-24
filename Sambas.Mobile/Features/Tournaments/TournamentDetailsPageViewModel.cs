using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Sambas.Mobile.Features.Matches;
using Sambas.Mobile.Models;
using Sambas.Mobile.Mvvm;
using Shiny.DocumentDb;
using UXDivers.Popups.Services;

namespace Sambas.Mobile.Features.Tournaments;

internal class TournamentDetailsPageViewModel : BaseViewModel
{
    private readonly IDocumentStore _store;
    private readonly IPopupService _popupService;

    public Tournament? Tournament
    {
        get;
        set => SetProperty(ref field, value);
    }

    public Guid TournamentId
    {
        get;
        set => SetProperty(ref field, value);
    }

    public ObservableCollection<Match> Matches
    {
        get;
        set => SetProperty(ref field, value);
    } = [];

    public ICommand AddMatchCommand { get; init; }
    public ICommand DeleteMatchCommand { get; init; }

    public TournamentDetailsPageViewModel(
        IDocumentStore store,
        IPopupService popupService,
        ILogger<BaseViewModel> logger)
        : base(logger)
    {
        _store = store;
        _popupService = popupService;

        AddMatchCommand = new Command(async () => await AddMatchAsync());
        DeleteMatchCommand = new Command<Match>(async (m) => await DeleteMatchAsync(m));
    }

    public override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadTournament();
    }

    private async Task LoadTournament()
    {
        var tournament = await _store.Get<Tournament>(TournamentId);
        Tournament = tournament!;

        foreach (var match in Tournament.Matches)
            Matches.Add(match);
    }

    private async Task AddMatchAsync()
    {
        var newMatch = await _popupService.PushAsync<EditMatchDetailsPopup, Match>();
        if (newMatch is null)
            return;

        Tournament!.AddMatch(newMatch);
        await _store.Update(Tournament);
        Matches.Add(newMatch);
    }

    private async Task DeleteMatchAsync(Match match)
    {
    }
}
