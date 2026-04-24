using Microsoft.Extensions.Logging;
using Sambas.Mobile.Mvvm;
using UXDivers.Popups.Services;

namespace Sambas.Mobile.Features.Tournaments;

internal class EditTournamentDetailsPopupViewModel : BasePopupViewModel
{
    private readonly IPopupService _popupService;

    public EditTournamentDetailsPopupViewModel(
        IPopupService popupService,
        ILogger<BaseViewModel> logger)
        : base(logger)
    {
        _popupService = popupService;
    }
}
