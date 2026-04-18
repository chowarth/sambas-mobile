namespace Sambas.Mobile.Controls;

public partial class SwipeItemControl : SwipeItemView
{
    public static readonly BindableProperty ActionColorProperty = BindableProperty.Create(
        nameof(ActionColor),
        typeof(Color),
        typeof(SwipeItemControl),
        Colors.Black);

    public Color ActionColor
    {
        get => (Color)GetValue(ActionColorProperty);
        set => SetValue(ActionColorProperty, value);
    }

    public static readonly BindableProperty ActionIconProperty = BindableProperty.Create(
        nameof(ActionIcon),
        typeof(ImageSource),
        typeof(SwipeItemControl),
        default(ImageSource));

    public ImageSource ActionIcon
    {
        get => (ImageSource)GetValue(ActionIconProperty);
        set => SetValue(ActionIconProperty, value);
    }

    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius),
        typeof(int),
        typeof(Border),
        -1);

    public int CornerRadius
    {
        get => (int)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public SwipeItemControl()
    {
        InitializeComponent();
    }
}