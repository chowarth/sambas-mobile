using Microsoft.Extensions.Logging;
using Sambas.Mobile.Features.Matches;
using Sambas.Mobile.Models;
using Sambas.Mobile.Mvvm;
using Shiny.DocumentDb;
using UXDivers.Popups.Services;

namespace Sambas.Mobile.Features.Tournaments;

internal sealed class EditTournamentMatchDetailsPopupViewModel : EditMatchDetailsPopupViewModel
{
    public EditTournamentMatchDetailsPopupViewModel(
        IDocumentStore store,
        IPopupService popupService,
        ILogger<BaseViewModel> logger)
        : base(store, popupService, logger)
    {
    }

    protected override async Task SaveMatchAsync()
    {
        // Custom save match logic for tournaments so that they
        // aren't saved to the database as their own document.
        // Return to the caller to add to the tournament matches collection.

        if (!IsValid)
            return;

        // Return values for popups should be bound to the page 'Result' property,
        // see: https://github.com/UXDivers/uxd-popups/issues/32
        Match = new Match(
            HomeTeam,
            new Team(Guid.Empty, AwayTeamName!, []),
            new KickOff(KickOffDate!.Value, KickOffTime!.Value),
            new Score(HomeScore!.Value, AwayScore!.Value),
            Array.Empty<Goal>()
        );

        await _popupService.PopAsync();
    }
}
