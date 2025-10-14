namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Represents the DaisyUI "label" class for descriptive text.
/// Use as: <p class="label">text</p> or <span class="label">text</span>
/// For floating labels, use RlvFloatingLabel component instead.
/// </summary>
public partial class RlvLabel
{
    [Parameter] public string? Text { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string CssClass => "label";
}
