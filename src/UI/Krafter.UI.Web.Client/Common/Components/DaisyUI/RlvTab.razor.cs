using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Tab component - a single tab item within an RlvTabs container.
/// Can render as a button, link, or radio input depending on parameters.
/// </summary>
public partial class RlvTab
{
    /// <summary>
    /// Whether to render as a radio input for automatic toggle behavior.
    /// Default: false (renders as button or link)
    /// </summary>
    [Parameter]
    public bool UseRadio { get; set; }

    /// <summary>
    /// Name for the radio input group (required if UseRadio is true).
    /// All tabs with the same name work together.
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    /// <summary>
    /// Link URL. If provided (and UseRadio is false), renders as an anchor tag.
    /// </summary>
    [Parameter]
    public string? Href { get; set; }

    /// <summary>
    /// Simple text content for the tab.
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// Label text for radio input mode (wraps radio with label element).
    /// </summary>
    [Parameter]
    public string? Label { get; set; }

    /// <summary>
    /// ARIA label for radio input mode (when Label is not used).
    /// </summary>
    [Parameter]
    public string? AriaLabel { get; set; }

    /// <summary>
    /// Rich content for the tab (icons, text, etc.).
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Tab content panel (for radio mode with tab-content divs).
    /// </summary>
    [Parameter]
    public RenderFragment? Content { get; set; }

    /// <summary>
    /// Whether this tab is active.
    /// </summary>
    [Parameter]
    public bool IsActive { get; set; }

    /// <summary>
    /// Whether this tab is disabled.
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// Click event handler (for button mode).
    /// </summary>
    [Parameter]
    public EventCallback OnClick { get; set; }

    /// <summary>
    /// Additional CSS classes for the tab-content element (radio mode only).
    /// </summary>
    [Parameter]
    public string? ContentClass { get; set; }

    /// <summary>
    /// Additional CSS classes for the tab element.
    /// </summary>
    [Parameter]
    public string? TabClass { get; set; }

    /// <summary>
    /// Additional attributes for the tab element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Handles the tab click event.
    /// </summary>
    private async Task HandleClick()
    {
        if (!IsDisabled)
        {
            await OnClick.InvokeAsync();
        }
    }

    /// <summary>
    /// Builds the CSS class string for the tab.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            // Active state
            if (IsActive)
            {
                classes.Add("tab-active");
            }

            // Disabled state
            if (IsDisabled)
            {
                classes.Add("tab-disabled");
            }

            // Custom classes
            if (!string.IsNullOrEmpty(TabClass))
            {
                classes.Add(TabClass);
            }

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }
}
