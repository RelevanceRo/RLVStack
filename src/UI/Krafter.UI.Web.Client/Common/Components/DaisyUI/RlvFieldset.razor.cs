namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvFieldset
{
    [Parameter] public string? Legend { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string CssClass
    {
        get
        {
            var classes = new List<string> { "fieldset" };
            return string.Join(" ", classes);
        }
    }
}
