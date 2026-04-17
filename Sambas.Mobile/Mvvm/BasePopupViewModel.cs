using Microsoft.Extensions.Logging;
using UXDivers.Popups;

namespace Sambas.Mobile.Mvvm;

internal abstract class BasePopupViewModel : BindableBase, IPopupViewModel
{
    protected readonly ILogger<BaseViewModel> Logger;

    protected BasePopupViewModel(ILogger<BaseViewModel> logger)
    {
        Logger = logger;
    }

    #region IPopupViewModel

    Task IPopupViewModel.OnPopupNavigatedAsync(IReadOnlyDictionary<string, object?> parameters)
    {
        Logger.LogInformation(nameof(IPopupViewModel.OnPopupNavigatedAsync));
        return OnPopupNavigatedAsync(parameters);
    }

    protected virtual Task OnPopupNavigatedAsync(IReadOnlyDictionary<string, object?> parameters)
        => Task.CompletedTask;

    #endregion IPopupViewModel
}
