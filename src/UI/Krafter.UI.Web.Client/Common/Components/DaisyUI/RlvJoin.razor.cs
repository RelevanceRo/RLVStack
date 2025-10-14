using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvJoin
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool Vertical { get; set; }
    [Parameter] public string? CssClass { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string ComputedCssClass => $"{(Vertical ? "join-vertical" : "")} {CssClass}".Trim();
}
