using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Sambas.Mobile.Models;
using Sambas.Mobile.Mvvm;
using Shiny.DocumentDb;
using UXDivers.Popups.Services;

namespace Sambas.Mobile.Features.Tournaments;

internal class TournamentListPageViewModel : BaseViewModel
{
    private readonly IDocumentStore _store;
    private readonly IPopupService _popupService;

    public ObservableCollection<DateGrouping<Tournament>> TournamentGroupings
    {
        get;
        set => SetProperty(ref field, value);
    } = new ObservableCollection<DateGrouping<Tournament>>();

    public ICommand AddTournamentCommand { get; init; }
    public ICommand DeleteTournamentCommand { get; init; }

    public TournamentListPageViewModel(
        IDocumentStore store,
        IPopupService popupService,
        ILogger<TournamentListPageViewModel> logger)
        : base(logger)
    {
        _store = store;
        _popupService = popupService;

        AddTournamentCommand = new Command(async () => await AddTournamentAsync());
        DeleteTournamentCommand = new Command<Tournament>(async (t) => await DeleteTournamentAsync(t));
    }

    public override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadTournaments();
    }

    private async Task LoadTournaments()
    {
        var tournaments = await _store.Query<Tournament>()
            .ToList();

        var groups = tournaments
            .GroupBy(x => new { x.StartDate.Year, x.StartDate.Month })
            .OrderByDescending(g => g.Key.Year)
            .ThenByDescending(g => g.Key.Month)
            .Select(g => new DateGrouping<Tournament>(
                // Dummy date for group heading, we only care about year & month.
                new DateTime(g.Key.Year, g.Key.Month, 1),
                g.OrderByDescending(t => t.StartDate))
            );

        TournamentGroupings.Clear();

        foreach (var group in groups)
            TournamentGroupings.Add(group);
    }

    private async Task AddTournamentAsync()
    {
        var newTournament = await _popupService.PushAsync<EditTournamentDetailsPopup, Tournament>();
        if (newTournament is not null)
            await LoadTournaments();
    }

    private async Task DeleteTournamentAsync(Tournament tournament)
    {
        if (await _store.Remove<Tournament>(tournament.Id))
        {
            var grouping = TournamentGroupings.First(g => g.Contains(tournament));
            grouping.Remove(tournament);

            if (grouping.Count == 0)
                TournamentGroupings.Remove(grouping);
        }
    }
}
