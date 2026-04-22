using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Sambas.Mobile.Models;
using Sambas.Mobile.Mvvm;
using Shiny.DocumentDb;
using UXDivers.Popups.Services;

namespace Sambas.Mobile.Features.Matches;

internal class MatchListPageViewModel : BaseViewModel
{
    private readonly IDocumentStore _store;
    private readonly IPopupService _popupService;

    public ICommand AddMatchCommand { get; init; }
    public ICommand DeleteMatchCommand { get; init; }

    public ObservableCollection<MatchGrouping> MatchGroupings
    {
        get;
        set => SetProperty(ref field, value);
    } = new ObservableCollection<MatchGrouping>();

    public MatchListPageViewModel(
        IDocumentStore store,
        IPopupService popupService,
        ILogger<MatchListPageViewModel> logger)
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

        await LoadMatches();
    }

    private async Task LoadMatches()
    {
        var matches = await _store.Query<Match>()
            .ToList();

        var groups = matches
            .GroupBy(x => new { x.KickOff.Date.Year, x.KickOff.Date.Month })
            .OrderByDescending(g => g.Key.Year)
            .ThenByDescending(g => g.Key.Month)
            .Select(g => new MatchGrouping(
                // Dummy date for group heading, we only care about year & month.
                new DateTime(g.Key.Year, g.Key.Month, 1),
                g.OrderByDescending(m => m.KickOff.Date))
            );

        MatchGroupings.Clear();

        foreach (MatchGrouping group in groups)
            MatchGroupings.Add(group);
    }

    private async Task AddMatchAsync()
    {
        var createdMatch = await _popupService.PushAsync<EditMatchDetailsPopup, Match>();
        if (createdMatch is not null)
            await LoadMatches();
    }

    private async Task DeleteMatchAsync(Match match)
    {
        if (await _store.Remove<Match>(match.Id))
        {
            MatchGroupings.First(g => g.Contains(match))
                .Remove(match);
        }
    }
}
