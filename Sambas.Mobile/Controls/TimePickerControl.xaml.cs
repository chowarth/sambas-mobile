namespace Sambas.Mobile.Controls;

public partial class TimePickerControl : Border
{
    public static readonly BindableProperty TimeProperty = BindableProperty.Create(
        nameof(Time),
        typeof(TimeSpan?),
        typeof(TimePickerControl),
        defaultBindingMode: BindingMode.TwoWay);

    public TimeSpan? Time
    {
        get => (TimeSpan?)GetValue(TimeProperty);
        set => SetValue(TimeProperty, value);
    }

    public TimePickerControl()
    {
        InitializeComponent();
    }
}