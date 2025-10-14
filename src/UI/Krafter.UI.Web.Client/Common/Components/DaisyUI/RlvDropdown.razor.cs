using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvDropdown
{
    /// <summary>
    /// Dropdown implementation method. CSS Focus is default for simplicity.
    /// </summary>
    [Parameter] public DropdownMethod Method { get; set; } = DropdownMethod.CssFocus;

    /// <summary>
    /// Horizontal alignment of dropdown content relative to button.
    /// </summary>
    [Parameter] public DropdownHorizontalPlacement HorizontalPlacement { get; set; } = DropdownHorizontalPlacement.Start;

    /// <summary>
    /// Vertical placement of dropdown content relative to button.
    /// </summary>
    [Parameter] public DropdownVerticalPlacement VerticalPlacement { get; set; } = DropdownVerticalPlacement.Bottom;

    /// <summary>
    /// Opens dropdown on hover (in addition to click/focus).
    /// </summary>
    [Parameter] public bool Hover { get; set; }

    /// <summary>
    /// Force dropdown to stay open.
    /// </summary>
    [Parameter] public bool Open { get; set; }

    /// <summary>
    /// Button/trigger content.
    /// </summary>
    [Parameter] public RenderFragment? ButtonContent { get; set; }

    /// <summary>
    /// Dropdown content (menu, card, etc.).
    /// </summary>
    [Parameter] public RenderFragment? Content { get; set; }

    /// <summary>
    /// CSS classes for the button element.
    /// </summary>
    [Parameter] public string? ButtonClass { get; set; }

    /// <summary>
    /// CSS classes for the content element (in addition to dropdown-content).
    /// </summary>
    [Parameter] public string? ContentClass { get; set; }

    /// <summary>
    /// Unique ID for popover method (required when using PopoverApi method).
    /// </summary>
    [Parameter] public string PopoverId { get; set; } = $"popover-{Guid.NewGuid():N}";

    /// <summary>
    /// Unique anchor name for popover method (required when using PopoverApi method).
    /// </summary>
    [Parameter] public string AnchorName { get; set; } = $"anchor-{Guid.NewGuid():N}";

    /// <summary>
    /// Additional attributes for button element (for CSS Focus and Popover methods).
    /// </summary>
    [Parameter] public Dictionary<string, object>? ButtonAttributes { get; set; }

    /// <summary>
    /// Additional attributes for content element (for CSS Focus and Popover methods).
    /// </summary>
    [Parameter] public Dictionary<string, object>? ContentAttributes { get; set; }

    /// <summary>
    /// Additional attributes for the container (for Details/Summary and CSS Focus methods).
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string CssClass
    {
        get
        {
            var classes = new List<string> { "dropdown" };

            // Horizontal placement
            if (HorizontalPlacement == DropdownHorizontalPlacement.Center)
                classes.Add("dropdown-center");
            else if (HorizontalPlacement == DropdownHorizontalPlacement.End)
                classes.Add("dropdown-end");
            // Start is default, no class needed

            // Vertical placement
            if (VerticalPlacement == DropdownVerticalPlacement.Top)
                classes.Add("dropdown-top");
            else if (VerticalPlacement == DropdownVerticalPlacement.Left)
                classes.Add("dropdown-left");
            else if (VerticalPlacement == DropdownVerticalPlacement.Right)
                classes.Add("dropdown-right");
            // Bottom is default, no class needed

            // Modifiers
            if (Hover)
                classes.Add("dropdown-hover");
            if (Open)
                classes.Add("dropdown-open");

            return string.Join(" ", classes);
        }
    }

    private string ContentCssClass
    {
        get
        {
            var classes = new List<string> { "dropdown-content" };
            if (!string.IsNullOrWhiteSpace(ContentClass))
                classes.Add(ContentClass);
            return string.Join(" ", classes);
        }
    }
}

/// <summary>
/// Dropdown implementation methods.
/// </summary>
public enum DropdownMethod
{
    /// <summary>
    /// Native details/summary elements. Opens/closes on click. Can be controlled via JS by adding/removing 'open' attribute.
    /// </summary>
    DetailsSummary,

    /// <summary>
    /// New HTML popover API with CSS anchor positioning. Opens on top layer (no z-index issues).
    /// Note: Anchor positioning not yet supported in Firefox/Safari (will appear centered like a modal).
    /// </summary>
    PopoverApi,

    /// <summary>
    /// CSS focus-based dropdown. Content displays when button is focused. Closes when focus is lost.
    /// Default method for simplicity.
    /// </summary>
    CssFocus
}

/// <summary>
/// Horizontal placement of dropdown content relative to button.
/// </summary>
public enum DropdownHorizontalPlacement
{
    /// <summary>
    /// Align to start of button (default).
    /// </summary>
    Start,

    /// <summary>
    /// Align to center of button.
    /// </summary>
    Center,

    /// <summary>
    /// Align to end of button.
    /// </summary>
    End
}

/// <summary>
/// Vertical placement of dropdown content relative to button.
/// </summary>
public enum DropdownVerticalPlacement
{
    /// <summary>
    /// Open from top.
    /// </summary>
    Top,

    /// <summary>
    /// Open from bottom (default).
    /// </summary>
    Bottom,

    /// <summary>
    /// Open from left.
    /// </summary>
    Left,

    /// <summary>
    /// Open from right.
    /// </summary>
    Right
}
