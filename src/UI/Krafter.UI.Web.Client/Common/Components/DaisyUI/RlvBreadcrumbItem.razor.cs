using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Breadcrumb item component - a single item within an RlvBreadcrumbs.
/// Can render as a link, clickable span, or plain text.
/// </summary>
public partial class RlvBreadcrumbItem
{
    /// <summary>
    /// Link URL. If provided, renders as an anchor tag.
    /// </summary>
    [Parameter]
    public string? Href { get; set; }

    /// <summary>
    /// Simple text content for the breadcrumb item.
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// Rich content for the breadcrumb item (icons, text, etc.).
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Click event handler (for non-link items).
    /// </summary>
    [Parameter]
    public EventCallback OnClick { get; set; }

    /// <summary>
    /// Additional CSS classes for the item content (a or span element).
    /// </summary>
    [Parameter]
    public string? ItemClass { get; set; }

    /// <summary>
    /// Additional attributes for the li element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Handles the breadcrumb item click event.
    /// </summary>
    private async Task HandleClick()
    {
        await OnClick.InvokeAsync();
    }
}
