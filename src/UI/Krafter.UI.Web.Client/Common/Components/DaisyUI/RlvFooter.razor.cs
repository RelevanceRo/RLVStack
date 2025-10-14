using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvFooter
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool Horizontal { get; set; }
    [Parameter] public bool Center { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string CssClass => string.Join(" ", new[] {
        Horizontal ? "footer-horizontal" : "",
        Center ? "footer-center" : ""
    }.Where(c => !string.IsNullOrEmpty(c)));
}
