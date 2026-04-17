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

    public ICommand AddGameCommand { get; init; }

    public IReadOnlyList<Match>? Matches
    {
        get;
        set => SetProperty(ref field, value);
    }

    public MatchListPageViewModel(
        IDocumentStore store,
        IPopupService popupService,
        ILogger<MatchListPageViewModel> logger)
        : base(logger)
    {
        _store = store;
        _popupService = popupService;

        AddGameCommand = new Command(async () => await AddGameAsync());
    }

    public override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadMatches();
    }

    private async Task LoadMatches()
    {
        Matches = await _store.Query<Match>().ToList();
    }

    private async Task AddGameAsync()
    {
        var createdMatch = await _popupService.PushAsync<EditMatchDetailsPopup, Match>();
        if (createdMatch is not null)
            await LoadMatches();
    }
}
