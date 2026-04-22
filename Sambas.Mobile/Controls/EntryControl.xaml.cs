using CommunityToolkit.Maui.Behaviors;

namespace Sambas.Mobile.Controls;

public partial class EntryControl : Border
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(EntryControl),
        defaultBindingMode: BindingMode.TwoWay);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly BindableProperty MinimumLengthProperty = BindableProperty.Create(
        nameof(MinimumLength),
        typeof(int),
        typeof(EntryControl),
        defaultValue: 1);

    public int MinimumLength
    {
        get => (int)GetValue(MinimumLengthProperty);
        set => SetValue(MinimumLengthProperty, value);
    }

    public static readonly BindableProperty MaximumLengthProperty = BindableProperty.Create(
        nameof(MaximumLength),
        typeof(int),
        typeof(EntryControl),
        defaultValue: int.MaxValue);

    public int MaximumLength
    {
        get => (int)GetValue(MaximumLengthProperty);
        set => SetValue(MaximumLengthProperty, value);
    }

    public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
        nameof(Keyboard),
        typeof(Keyboard),
        typeof(EntryControl),
        defaultValue: Keyboard.Default,
        propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
        {
            // Update the CharacterType of the EntryValidator based on the new Keyboard value
            // Numeric keyboard allows only digits, while the default keyboard allows letters.
            var keyboard = (Keyboard)newValue;
            if (keyboard == Keyboard.Numeric)
            {
                ((EntryControl)bindable).EntryValidator.CharacterType = CharacterType.Digit;
            }
            else
            {
                ((EntryControl)bindable).EntryValidator.CharacterType = CharacterType.Letter;
            }
        });

    public Keyboard Keyboard
    {
        get => (Keyboard)GetValue(KeyboardProperty);
        set => SetValue(KeyboardProperty, value);
    }

    public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(
        nameof(IsReadOnly),
        typeof(bool),
        typeof(EntryControl),
        defaultValue: false);

    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    public EntryControl()
    {
        InitializeComponent();
    }
}
