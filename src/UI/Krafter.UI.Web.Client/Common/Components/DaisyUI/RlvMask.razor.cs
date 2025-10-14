using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvMask
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string MaskShape { get; set; } = "squircle";
    [Parameter] public string? CssClass { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string MaskClass => $"mask-{MaskShape}";
}
