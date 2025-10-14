namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvLoading
{
    [Parameter] public LoadingAnimation Animation { get; set; } = LoadingAnimation.Spinner;
    [Parameter] public LoadingSize Size { get; set; } = LoadingSize.Medium;
    [Parameter] public string? Color { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string CssClass
    {
        get
        {
            var classes = new List<string> { "loading" };

            var animationClass = Animation switch
            {
                LoadingAnimation.Spinner => "loading-spinner",
                LoadingAnimation.Dots => "loading-dots",
                LoadingAnimation.Ring => "loading-ring",
                LoadingAnimation.Ball => "loading-ball",
                LoadingAnimation.Bars => "loading-bars",
                LoadingAnimation.Infinity => "loading-infinity",
                _ => "loading-spinner"
            };
            classes.Add(animationClass);

            var sizeClass = Size switch
            {
                LoadingSize.XSmall => "loading-xs",
                LoadingSize.Small => "loading-sm",
                LoadingSize.Medium => "loading-md",
                LoadingSize.Large => "loading-lg",
                LoadingSize.XLarge => "loading-xl",
                _ => "loading-md"
            };
            classes.Add(sizeClass);

            if (!string.IsNullOrEmpty(Color))
            {
                classes.Add($"text-{Color}");
            }

            return string.Join(" ", classes);
        }
    }
}

public enum LoadingAnimation
{
    Spinner,
    Dots,
    Ring,
    Ball,
    Bars,
    Infinity
}

public enum LoadingSize
{
    XSmall,
    Small,
    Medium,
    Large,
    XLarge
}
