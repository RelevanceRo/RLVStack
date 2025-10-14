using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Collapse component for showing and hiding content.
/// Supports three methods: focus-based (default), checkbox-based, or details/summary.
/// </summary>
public partial class RlvCollapse
{
    /// <summary>
    /// Whether to use details/summary HTML elements.
    /// This is the most semantic and accessible method.
    /// Note: collapse-open and collapse-close don't work with this method.
    /// Default: false
    /// </summary>
    [Parameter]
    public bool UseDetails { get; set; }

    /// <summary>
    /// Whether to use checkbox input for toggle control.
    /// Requires clicking to toggle (doesn't close on focus loss).
    /// Default: false (uses focus-based method)
    /// </summary>
    [Parameter]
    public bool UseCheckbox { get; set; }

    /// <summary>
    /// Whether the collapse is open (for checkbox and details methods).
    /// Supports two-way binding.
    /// </summary>
    [Parameter]
    public bool IsOpen { get; set; }

    /// <summary>
    /// Callback invoked when IsOpen changes.
    /// </summary>
    [Parameter]
    public EventCallback<bool> IsOpenChanged { get; set; }

    /// <summary>
    /// Simple text title for the collapse.
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// Rich content for the title.
    /// </summary>
    [Parameter]
    public RenderFragment? TitleContent { get; set; }

    /// <summary>
    /// Simple text content for the collapse body.
    /// </summary>
    [Parameter]
    public string? Content { get; set; }

    /// <summary>
    /// Rich content for the collapse body.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Icon style for the collapse.
    /// </summary>
    [Parameter]
    public CollapseIcon Icon { get; set; } = CollapseIcon.None;

    /// <summary>
    /// Force state of the collapse (only works with focus and checkbox methods, not details).
    /// </summary>
    [Parameter]
    public CollapseForceState ForceState { get; set; } = CollapseForceState.None;

    /// <summary>
    /// Additional CSS classes for the collapse-title element.
    /// </summary>
    [Parameter]
    public string? TitleClass { get; set; }

    /// <summary>
    /// Additional CSS classes for the collapse-content element.
    /// </summary>
    [Parameter]
    public string? ContentClass { get; set; }

    /// <summary>
    /// Additional CSS classes for the checkbox element (checkbox method only).
    /// Can use "peer" for peer-checked styling.
    /// </summary>
    [Parameter]
    public string? CheckboxClass { get; set; }

    /// <summary>
    /// Additional CSS classes for the collapse container.
    /// </summary>
    [Parameter]
    public string? CollapseClass { get; set; }

    /// <summary>
    /// Additional attributes for the collapse element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Attributes for the details element (when UseDetails is true).
    /// </summary>
    private Dictionary<string, object>? DetailsAttributes
    {
        get
        {
            var attrs = new Dictionary<string, object>();

            // Merge additional attributes
            if (AdditionalAttributes != null)
            {
                foreach (var attr in AdditionalAttributes)
                {
                    attrs[attr.Key] = attr.Value;
                }
            }

            // Add open attribute if IsOpen
            if (IsOpen)
            {
                attrs["open"] = true;
            }

            return attrs.Count > 0 ? attrs : null;
        }
    }

    /// <summary>
    /// Handles the checkbox change event.
    /// </summary>
    private async Task HandleCheckboxChange(ChangeEventArgs e)
    {
        if (e.Value is bool value)
        {
            IsOpen = value;
            await IsOpenChanged.InvokeAsync(IsOpen);
        }
    }

    /// <summary>
    /// Builds the CSS class string for the collapse.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            // Icon style
            classes.Add(Icon switch
            {
                CollapseIcon.Arrow => "collapse-arrow",
                CollapseIcon.Plus => "collapse-plus",
                _ => ""
            });

            // Force state (not for details method)
            if (!UseDetails)
            {
                classes.Add(ForceState switch
                {
                    CollapseForceState.Open => "collapse-open",
                    CollapseForceState.Close => "collapse-close",
                    _ => ""
                });
            }

            // Custom classes
            if (!string.IsNullOrEmpty(CollapseClass))
            {
                classes.Add(CollapseClass);
            }

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }
}

/// <summary>
/// Icon style options for collapse.
/// </summary>
public enum CollapseIcon
{
    None,
    Arrow,
    Plus
}

/// <summary>
/// Force state options for collapse.
/// </summary>
public enum CollapseForceState
{
    None,
    Open,
    Close
}
