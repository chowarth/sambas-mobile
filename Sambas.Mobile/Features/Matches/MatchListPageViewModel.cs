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

    public ObservableCollection<DateGrouping<Match>> MatchGroupings
    {
        get;
        set => SetProperty(ref field, value);
    } = new ObservableCollection<DateGrouping<Match>>();

    public ICommand AddMatchCommand { get; init; }
    public ICommand DeleteMatchCommand { get; init; }

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
            .Select(g => new DateGrouping<Match>(
                // Dummy date for group heading, we only care about year & month.
                new DateTime(g.Key.Year, g.Key.Month, 1),
                g.OrderByDescending(m => m.KickOff.Date))
            );

        MatchGroupings.Clear();

        foreach (var group in groups)
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
            var grouping = MatchGroupings.First(g => g.Contains(match));
            grouping.Remove(match);

            if (grouping.Count == 0)
                MatchGroupings.Remove(grouping);
        }
    }
}
