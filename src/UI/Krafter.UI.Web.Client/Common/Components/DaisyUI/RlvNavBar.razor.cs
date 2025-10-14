using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Navbar component for showing a navigation bar on the top of the page.
/// Contains a flexible layout container that can hold navbar-start, navbar-center, and navbar-end sections.
/// </summary>
public partial class RlvNavBar
{
    /// <summary>
    /// Child content - typically contains navbar-start, navbar-center, and navbar-end divs.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional CSS classes for the navbar container.
    /// </summary>
    [Parameter]
    public string? NavBarClass { get; set; }

    /// <summary>
    /// Additional attributes for the navbar div element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Builds the CSS class string for the navbar.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            // Custom classes
            if (!string.IsNullOrEmpty(NavBarClass))
            {
                classes.Add(NavBarClass);
            }

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }
}
