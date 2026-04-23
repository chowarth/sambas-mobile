using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Sambas.Mobile.Models;
using Sambas.Mobile.Mvvm;
using Shiny.DocumentDb;
using UXDivers.Popups.Services;

namespace Sambas.Mobile.Features.Matches;

internal class EditMatchDetailsPopupViewModel : BasePopupViewModel
{
    private readonly IDocumentStore _store;
    private readonly IPopupService _popupService;

    public Match? Match
    {
        get;
        set => SetProperty(ref field, value);
    }

    public Team HomeTeam
    {
        get;
        set => SetProperty(ref field, value);
    } = Team.Sambas;

    public string? AwayTeamName
    {
        get;
        set => SetProperty(ref field, value, () => RaisePropertyChanged(nameof(IsValid)));
    }

    public int? HomeScore
    {
        get;
        set => SetProperty(ref field, value, () => RaisePropertyChanged(nameof(IsValid)));
    }

    public int? AwayScore
    {
        get;
        set => SetProperty(ref field, value, () => RaisePropertyChanged(nameof(IsValid)));
    }

    public DateTime? KickOffDate
    {
        get;
        set => SetProperty(ref field, value, () => RaisePropertyChanged(nameof(IsValid)));
    }

    public TimeSpan? KickOffTime
    {
        get;
        set => SetProperty(ref field, value, () => RaisePropertyChanged(nameof(IsValid)));
    }

    public bool IsValid
        => IsValidMatchDetails();

    public ICommand SaveMatchCommand { get; init; }

    public EditMatchDetailsPopupViewModel(
        IDocumentStore store,
        IPopupService popupService,
        ILogger<BaseViewModel> logger)
        : base(logger)
    {
        _store = store;
        _popupService = popupService;

        SaveMatchCommand = new Command(async () => await SaveMatchAsync());
    }

    private async Task SaveMatchAsync()
    {
        if (!IsValid)
            return;

        // TODO: Handle match edit
        // Pass an existing match to the popup when editing
        // If Match.Id is empty, then it's a new match and we should insert it
        // If Match.Id is not empty, then it's an existing match and we should update it
        var match = new Match(
            HomeTeam,
            new Team(Guid.Empty, AwayTeamName!, []),
            new KickOff(KickOffDate!.Value, KickOffTime!.Value),
            new Score(HomeScore!.Value, AwayScore!.Value),
            Array.Empty<Goal>()
        );
        await _store.Insert(match);

        // Return values for popups should be bound to the page 'Result' property,
        // see: https://github.com/UXDivers/uxd-popups/issues/32
        Match = match;
        await _popupService.PopAsync();
    }

    private bool IsValidMatchDetails()
    {
        if (string.IsNullOrWhiteSpace(AwayTeamName) || AwayTeamName.Length < 3 ||
            !HomeScore.HasValue || !AwayScore.HasValue ||
            !KickOffDate.HasValue || !KickOffTime.HasValue)
        {
            return false;
        }

        return true;
    }
}
