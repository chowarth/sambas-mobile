using Microsoft.Extensions.Logging;
using Sambas.Mobile.Models;
using Sambas.Mobile.Mvvm;
using Shiny;
using Shiny.DocumentDb;

namespace Sambas.Mobile.Features.Startup;

internal class StartupPageViewModel : BaseViewModel
{
    private readonly IDocumentStore _store;
    private readonly INavigator _navigator;

    public StartupPageViewModel(
        IDocumentStore store,
        INavigator navigator,
        ILogger<StartupPageViewModel> logger)
        : base(logger)
    {
        _store = store;
        _navigator = navigator;
    }

    public override async void OnAppearing()
    {
        base.OnAppearing();

        await InitialiseAsync();

        await _navigator.SwitchShell<AppShell>();
    }

    private async Task InitialiseAsync()
    {
        var sambas = await _store.Get<Team>(Team.Sambas.Id);
        if (sambas is null)
        {
            await _store.Insert(Team.Sambas);
        }

        await Task.Delay(1_000);
    }
}
