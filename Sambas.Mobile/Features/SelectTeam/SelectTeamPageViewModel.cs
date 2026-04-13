using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Sambas.Mobile.Features.EditTeam;
using Sambas.Mobile.Models;
using Sambas.Mobile.Mvvm;
using Shiny;

namespace Sambas.Mobile.Features.SelectTeam;

internal sealed class SelectTeamPageViewModel : BaseViewModel
{
    private readonly INavigator _navigator;

    private IList<Team> _teams = [
        new Team(
            Guid.NewGuid(),
            "Sambas",
            "https://www.foresttownrangers.co.uk/wp-content/uploads/Forest-Town-Rangers-FC-Logo.png"
        )
    ];

    public IList<Team> Teams
    {
        get => _teams;
        set => SetProperty(ref _teams, value);
    }


    public SelectTeamPageViewModel(
        INavigator navigator,
        ILogger<SelectTeamPageViewModel> logger)
        : base(logger)
    {
        AddTeamCommand = new Command(async () => await AddTeamAsync());
        _navigator = navigator;
    }
}
