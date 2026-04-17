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

    public Match Match
    {
        get;
        set => SetProperty(ref field, value);
    }

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

        Match = new Match(
            Guid.Empty,
            Team.Sambas,
            Team.Empty,
            new KickOff(),
            new Score(),
            Array.Empty<Goal>()
        );
    }

    private async Task SaveMatchAsync()
    {
        // TODO: Handle match edit
        // Pass an existing match to the popup when editing
        // If Match.Id is empty, then it's a new match and we should insert it
        // If Match.Id is not empty, then it's an existing match and we should update it

        await _store.Insert(Match);

        // Return values for popups should be bound to the page 'Result' property,
        // see: https://github.com/UXDivers/uxd-popups/issues/32
        await _popupService.PopAsync();
    }
}
