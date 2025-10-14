using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Menu item component - a single item within an RlvMenu.
/// Can render as a link, button, title, or submenu.
/// </summary>
public partial class RlvMenuItem
{
    /// <summary>
    /// Link URL. If provided, renders as an anchor tag.
    /// </summary>
    [Parameter]
    public string? Href { get; set; }

    /// <summary>
    /// Simple text content for the menu item.
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// Rich content for the menu item (icons, text, etc.).
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Whether this is a menu title (section header).
    /// </summary>
    [Parameter]
    public bool IsTitle { get; set; }

    /// <summary>
    /// Whether this menu item is active.
    /// </summary>
    [Parameter]
    public bool IsActive { get; set; }

    /// <summary>
    /// Whether this menu item is disabled.
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// Whether this menu item is focused.
    /// </summary>
    [Parameter]
    public bool IsFocused { get; set; }

    /// <summary>
    /// Whether this item has a submenu.
    /// </summary>
    [Parameter]
    public bool HasSubmenu { get; set; }

    /// <summary>
    /// Content for the submenu (nested menu items).
    /// </summary>
    [Parameter]
    public RenderFragment? SubmenuContent { get; set; }

    /// <summary>
    /// Content for the summary element (when HasSubmenu is true).
    /// </summary>
    [Parameter]
    public RenderFragment? SummaryContent { get; set; }

    /// <summary>
    /// Whether the submenu details element is open by default.
    /// </summary>
    [Parameter]
    public bool SubmenuOpen { get; set; }

    /// <summary>
    /// Click event handler.
    /// </summary>
    [Parameter]
    public EventCallback OnClick { get; set; }

    /// <summary>
    /// Additional CSS classes for the menu item.
    /// </summary>
    [Parameter]
    public string? ItemClass { get; set; }

    /// <summary>
    /// Additional attributes for the li element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Attributes for the details element (when HasSubmenu is true).
    /// </summary>
    private Dictionary<string, object>? DetailsAttributes
    {
        get
        {
            if (SubmenuOpen)
            {
                return new Dictionary<string, object> { { "open", true } };
            }
            return null;
        }
    }

    /// <summary>
    /// Handles the menu item click event.
    /// </summary>
    private async Task HandleClick()
    {
        if (!IsDisabled)
        {
            await OnClick.InvokeAsync();
        }
    }

    /// <summary>
    /// Builds the CSS class string for the menu item.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            // Active state
            if (IsActive)
            {
                classes.Add("menu-active");
            }

            // Disabled state
            if (IsDisabled)
            {
                classes.Add("menu-disabled");
            }

            // Focused state
            if (IsFocused)
            {
                classes.Add("menu-focus");
            }

            // Custom classes
            if (!string.IsNullOrEmpty(ItemClass))
            {
                classes.Add(ItemClass);
            }

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }
}
