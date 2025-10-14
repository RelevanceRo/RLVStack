using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvCode
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }
}
