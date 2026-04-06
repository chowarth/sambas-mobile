using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sambas.Mobile.Mvvm;

internal abstract class BindableBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual bool SetProperty<T>(ref T store, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(store, value))
            return false;

        store = value;
        RaisePropertyChanged(propertyName);

        return true;
    }

    protected virtual bool SetProperty<T>(ref T store, T value, Action? onChanged,
        [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(store, value))
            return false;

        store = value;
        onChanged?.Invoke();
        RaisePropertyChanged(propertyName);

        return true;
    }

    protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
    {
        PropertyChanged?.Invoke(this, args);
    }
}
