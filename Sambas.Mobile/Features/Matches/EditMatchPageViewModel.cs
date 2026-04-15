using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Sambas.Mobile.Models;
using Sambas.Mobile.Mvvm;
using Shiny.DocumentDb;
using UXDivers.Popups;

namespace Sambas.Mobile.Features.Matches;

internal sealed record Match(Guid Id, Team HomeTeam, Team AwayTeam, DateTimeOffset KickOffUtc, Score score, IReadOnlyCollection<Goal> Goals);
internal sealed record Score(int HomeTeamScore, int AwayTeamScore);
internal sealed record Goal(Team ScoringTeam, Player ScoredBy, DateTimeOffset ScoredAtUtc);

internal class EditMatchPageViewModel : BaseViewModel, IPopupViewModel
{
    private readonly IDocumentStore _store;

    public Match Match
    {
        get;
        set => SetProperty(ref field, value);
    }

    public ICommand SaveMatchCommand { get; init; }

    public EditMatchPageViewModel(
        IDocumentStore store,
        ILogger<BaseViewModel> logger)
        : base(logger)
    {
        _store = store;

        SaveMatchCommand = new Command(async () => await SaveMatchAsync());

        Match = new Match(
            Guid.Empty,
            Team.Sambas,
            Team.Empty,
            DateTimeOffset.UtcNow,
            new Score(0, 0),
            Array.Empty<Goal>()
        );
    }

    private async Task SaveMatchAsync()
    {
        // TODO: Handle match edit
        // Pass an existing match to the popup when editing
        // If Match.Id is empty, then it's a new match and we should insert it
        // If Match.Id is not empty, then it's an existing match and we should update it

        //await _store.Insert(Match);

        // Return match to the caller
        // await _popupService.PopAsync(Match);
        await Task.CompletedTask;
    }

    Task IPopupViewModel.OnPopupNavigatedAsync(IReadOnlyDictionary<string, object?> parameters)
    {
        return Task.CompletedTask;
    }
}
