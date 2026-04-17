namespace Sambas.Mobile.Controls;

public partial class DatePickerControl : Border
{
    public static readonly BindableProperty DateProperty = BindableProperty.Create(
        nameof(Date),
        typeof(DateTime?),
        typeof(DatePickerControl),
        defaultBindingMode: BindingMode.TwoWay);

    public DateTime? Date
    {
        get => (DateTime?)GetValue(DateProperty);
        set => SetValue(DateProperty, value);
    }

    public DatePickerControl()
    {
        InitializeComponent();
    }
}