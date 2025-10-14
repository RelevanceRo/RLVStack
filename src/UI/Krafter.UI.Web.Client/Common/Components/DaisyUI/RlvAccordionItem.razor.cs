using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Accordion item component using radio inputs for exclusive open/close behavior.
/// Part of an accordion group where only one item can be open at a time.
/// </summary>
public partial class RlvAccordionItem
{
    /// <summary>
    /// Name for the radio input group. All accordion items with the same name work together.
    /// REQUIRED - must be unique across the page for each accordion group.
    /// </summary>
    [Parameter, EditorRequired]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Value for this specific radio input item.
    /// If not provided, a unique value will be generated.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// Whether this item is open by default.
    /// Only one item per group should have this set to true.
    /// </summary>
    [Parameter]
    public bool IsOpen { get; set; }

    /// <summary>
    /// Simple text title for the accordion item.
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// Rich content for the title (for custom styling, icons, etc.).
    /// </summary>
    [Parameter]
    public RenderFragment? TitleContent { get; set; }

    /// <summary>
    /// Simple text content for the accordion item.
    /// </summary>
    [Parameter]
    public string? Content { get; set; }

    /// <summary>
    /// Rich content for the accordion item body.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Icon style for the accordion item.
    /// </summary>
    [Parameter]
    public AccordionIcon Icon { get; set; } = AccordionIcon.None;

    /// <summary>
    /// Force state of the accordion item.
    /// </summary>
    [Parameter]
    public AccordionForceState ForceState { get; set; } = AccordionForceState.None;

    /// <summary>
    /// Additional CSS classes for the title element.
    /// </summary>
    [Parameter]
    public string? TitleClass { get; set; }

    /// <summary>
    /// Additional CSS classes for the content element.
    /// </summary>
    [Parameter]
    public string? ContentClass { get; set; }

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

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        // Generate unique value if not provided
        if (string.IsNullOrEmpty(Value))
        {
            Value = Guid.NewGuid().ToString();
        }
    }

    /// <summary>
    /// Builds the CSS class string for the collapse container.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            // Icon style
            classes.Add(Icon switch
            {
                AccordionIcon.Arrow => "collapse-arrow",
                AccordionIcon.Plus => "collapse-plus",
                _ => ""
            });

            // Force state
            classes.Add(ForceState switch
            {
                AccordionForceState.Open => "collapse-open",
                AccordionForceState.Close => "collapse-close",
                _ => ""
            });

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
/// Icon style options for accordion items.
/// </summary>
public enum AccordionIcon
{
    None,
    Arrow,
    Plus
}

/// <summary>
/// Force state options for accordion items.
/// </summary>
public enum AccordionForceState
{
    None,
    Open,
    Close
}
