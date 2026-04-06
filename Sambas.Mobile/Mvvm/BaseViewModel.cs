using Shiny;

namespace Sambas.Mobile.Mvvm;

internal abstract class BaseViewModel : BindableBase, INavigationAware, IPageLifecycleAware, IDisposable
{
    #region INavigationAware

    void INavigationAware.OnNavigatingFrom(IDictionary<string, object> parameters)
    {
        OnNavigatingFrom(parameters);
    }

    public virtual void OnNavigatingFrom(IDictionary<string, object> parameters)
    {
    }

    #endregion INavigationAware

    #region IPageLifecycleAware

    void IPageLifecycleAware.OnAppearing()
    {
        OnAppearing();
    }

    public virtual void OnAppearing()
    {
    }

    void IPageLifecycleAware.OnDisappearing()
    {
        OnDisappearing();
    }

    public virtual void OnDisappearing()
    {
    }

    #endregion IPageLifecycleAware

    #region IDisposable

    void IDisposable.Dispose()
    {
        Dispose();
    }

    public virtual void Dispose()
    {
    }

    #endregion IDisposable
}
