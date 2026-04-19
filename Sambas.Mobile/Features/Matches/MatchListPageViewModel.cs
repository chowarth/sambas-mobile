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

    public ObservableCollection<Match> Matches
    {
        get;
        set => SetProperty(ref field, value);
    } = new ObservableCollection<Match>();

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
            .OrderByDescending(x => x.KickOff.Date!)
            .ToList();

        Matches.Clear();

        foreach (Match match in matches)
            Matches.Add(match);
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
            Matches.Remove(match);
    }
}
