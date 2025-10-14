using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvDrawer
{
    /// <summary>
    /// Unique ID for the drawer toggle checkbox.
    /// Auto-generated if not provided.
    /// </summary>
    [Parameter]
    public string Id { get; set; } = $"drawer-{Guid.NewGuid():N}";

    /// <summary>
    /// Controls whether the drawer is open.
    /// </summary>
    [Parameter]
    public bool IsOpen { get; set; }

    /// <summary>
    /// Event callback when drawer open state changes.
    /// </summary>
    [Parameter]
    public EventCallback<bool> IsOpenChanged { get; set; }

    /// <summary>
    /// Position the drawer on the right side (default is left).
    /// </summary>
    [Parameter]
    public bool End { get; set; }

    /// <summary>
    /// Force the drawer to always be open (removes toggle functionality).
    /// Useful for responsive layouts (e.g., lg:drawer-open).
    /// </summary>
    [Parameter]
    public bool ForceOpen { get; set; }

    /// <summary>
    /// Main page content (required).
    /// </summary>
    [Parameter]
    public RenderFragment? Content { get; set; }

    /// <summary>
    /// Sidebar content (required).
    /// </summary>
    [Parameter]
    public RenderFragment? SideContent { get; set; }

    /// <summary>
    /// Additional CSS classes for the drawer container.
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// Additional CSS classes for the drawer-content element.
    /// </summary>
    [Parameter]
    public string? ContentClass { get; set; }

    /// <summary>
    /// Additional CSS classes for the drawer-side element.
    /// </summary>
    [Parameter]
    public string? SideClass { get; set; }

    /// <summary>
    /// Captures additional HTML attributes.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string CssClass => $"drawer {(End ? "drawer-end" : "")} {(ForceOpen ? "drawer-open" : "")} {Class}".Trim();

    private string ContentCssClass => $"drawer-content {ContentClass}".Trim();

    private string SideCssClass => $"drawer-side {SideClass}".Trim();

    private async Task HandleToggleChange(ChangeEventArgs e)
    {
        if (e.Value is bool value)
        {
            IsOpen = value;
            await IsOpenChanged.InvokeAsync(IsOpen);
        }
    }
}
