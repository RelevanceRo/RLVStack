namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvFilter
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool UseForm { get; set; } = true;

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string CssClass
    {
        get
        {
            var classes = new List<string> { "filter" };
            return string.Join(" ", classes);
        }
    }
}
