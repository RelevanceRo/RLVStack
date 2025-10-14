namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvSkeleton
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Width { get; set; }
    [Parameter] public string? Height { get; set; }
    [Parameter] public bool Circle { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string CssClass
    {
        get
        {
            var classes = new List<string> { "skeleton" };

            if (!string.IsNullOrEmpty(Width))
            {
                classes.Add(Width);
            }

            if (!string.IsNullOrEmpty(Height))
            {
                classes.Add(Height);
            }

            if (Circle)
            {
                classes.Add("rounded-full");
            }

            return string.Join(" ", classes);
        }
    }
}
