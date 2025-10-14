namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvRadialProgress
{
    [Parameter] public int Value { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Color { get; set; }
    [Parameter] public string? Size { get; set; }
    [Parameter] public string? Thickness { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string CssClass
    {
        get
        {
            var classes = new List<string> { "radial-progress" };

            if (!string.IsNullOrEmpty(Color))
            {
                classes.Add($"text-{Color}");
            }

            return string.Join(" ", classes);
        }
    }

    private string StyleAttribute
    {
        get
        {
            var styles = new List<string> { $"--value:{Value}" };

            if (!string.IsNullOrEmpty(Size))
            {
                styles.Add($"--size:{Size}");
            }

            if (!string.IsNullOrEmpty(Thickness))
            {
                styles.Add($"--thickness:{Thickness}");
            }

            return string.Join("; ", styles);
        }
    }
}
