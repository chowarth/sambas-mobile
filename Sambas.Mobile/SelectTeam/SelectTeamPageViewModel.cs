using Sambas.Mobile.Mvvm;

namespace Sambas.Mobile.SelectTeam;

internal sealed record Team(Guid Id, string Name, string LogoUrl);

internal sealed class SelectTeamPageViewModel : BaseViewModel
{
    private IList<Team> _teams = new List<Team>();
    public IList<Team> Teams
    {
        get => _teams;
        set => SetProperty(ref _teams, value);
    }

    public override void OnAppearing()
    {
        base.OnAppearing();

        Teams.Add(
            new Team(
                Guid.NewGuid(),
                "Sambas",
                "https://www.foresttownrangers.co.uk/wp-content/uploads/Forest-Town-Rangers-FC-Logo.png"
            )
        );
    }
}
