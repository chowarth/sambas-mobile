using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Sambas.Mobile.Models;
using Sambas.Mobile.Mvvm;
using Shiny.DocumentDb;
using UXDivers.Popups.Services;

namespace Sambas.Mobile.Features.Tournaments;

internal class EditTournamentDetailsPopupViewModel : BasePopupViewModel
{
    private readonly IDocumentStore _store;
    private readonly IPopupService _popupService;

    public Tournament? Tournament
    {
        get;
        set => SetProperty(ref field, value);
    }

    public string? TournamentName
    {
        get;
        set => SetProperty(ref field, value, () => RaisePropertyChanged(nameof(IsValid)));
    }

    public DateTime? StartDate
    {
        get;
        set => SetProperty(ref field, value, () => RaisePropertyChanged(nameof(IsValid)));
    }

    public DateTime? EndDate
    {
        get;
        set => SetProperty(ref field, value, () => RaisePropertyChanged(nameof(IsValid)));
    }

    public bool IsValid
        => IsValidTournamentDetails();

    public ICommand SaveTournamentCommand { get; init; }

    public EditTournamentDetailsPopupViewModel(
        IDocumentStore store,
        IPopupService popupService,
        ILogger<BaseViewModel> logger)
        : base(logger)
    {
        _store = store;
        _popupService = popupService;

        SaveTournamentCommand = new Command(async () => await SaveTournamentAsync());
    }

    private async Task SaveTournamentAsync()
    {
        if (!IsValid)
            return;

        var tournament = new Tournament(
            TournamentName!,
            DateOnly.FromDateTime(StartDate!.Value),
            DateOnly.FromDateTime(EndDate!.Value)
        );
        await _store.Insert(tournament);

        Tournament = tournament;
        await _popupService.PopAsync();
    }

    private bool IsValidTournamentDetails()
    {
        if (string.IsNullOrWhiteSpace(TournamentName) || TournamentName.Length < 3 ||
            !StartDate.HasValue || !EndDate.HasValue)
        {
            return false;
        }

        return true;
    }
}
