using Microsoft.Extensions.Logging;
using Shiny;

namespace Sambas.Mobile.Mvvm;

internal abstract class BaseViewModel : BindableBase, INavigationAware, IPageLifecycleAware, IDisposable
{
    protected readonly ILogger<BaseViewModel> Logger;

    protected BaseViewModel(ILogger<BaseViewModel> logger)
    {
        Logger = logger;
    }

    #region INavigationAware

    void INavigationAware.OnNavigatingFrom(IDictionary<string, object> parameters)
    {
        Logger.LogInformation(nameof(INavigationAware.OnNavigatingFrom));
        OnNavigatingFrom(parameters);
    }

    public virtual void OnNavigatingFrom(IDictionary<string, object> parameters)
    {
    }

    #endregion INavigationAware

    #region IPageLifecycleAware

    void IPageLifecycleAware.OnAppearing()
    {
        Logger.LogInformation(nameof(IPageLifecycleAware.OnAppearing));
        OnAppearing();
    }

    public virtual void OnAppearing()
    {
    }

    void IPageLifecycleAware.OnDisappearing()
    {
        Logger.LogInformation(nameof(IPageLifecycleAware.OnDisappearing));
        OnDisappearing();
    }

    public virtual void OnDisappearing()
    {
    }

    #endregion IPageLifecycleAware

    #region IDisposable

    void IDisposable.Dispose()
    {
        Logger.LogInformation(nameof(IDisposable.Dispose));
        Dispose();
    }

    public virtual void Dispose()
    {
    }

    #endregion IDisposable
}
