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

    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
        nameof(BorderColor),
        typeof(Color),
        typeof(SwipeItemControl),
        Colors.Fuchsia);

    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(
        nameof(BorderWidth),
        typeof(double),
        typeof(SwipeItemControl),
        -1d);

    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
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