using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Pagination component for navigating between a set of related content.
/// Uses the join component to group pagination buttons together.
/// </summary>
public partial class RlvPagination
{
    /// <summary>
    /// Child content - typically contains multiple buttons with join-item class.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Orientation of the pagination.
    /// </summary>
    [Parameter]
    public PaginationOrientation Orientation { get; set; } = PaginationOrientation.Horizontal;

    /// <summary>
    /// Apply grid layout with equal width columns.
    /// Useful for "Previous" and "Next" buttons with equal width.
    /// </summary>
    [Parameter]
    public int? GridColumns { get; set; }

    /// <summary>
    /// Additional CSS classes for the pagination container.
    /// </summary>
    [Parameter]
    public string? PaginationClass { get; set; }

    /// <summary>
    /// Additional attributes for the pagination div element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Builds the CSS class string for the pagination.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            // Orientation (Horizontal is default, no class needed)
            if (Orientation == PaginationOrientation.Vertical)
            {
                classes.Add("join-vertical");
            }
            // Horizontal is default, no class needed

            // Grid layout
            if (GridColumns.HasValue)
            {
                classes.Add("grid");
                classes.Add($"grid-cols-{GridColumns.Value}");
            }

            // Custom classes
            if (!string.IsNullOrEmpty(PaginationClass))
            {
                classes.Add(PaginationClass);
            }

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }
}

/// <summary>
/// Orientation options for pagination.
/// </summary>
public enum PaginationOrientation
{
    Horizontal,
    Vertical
}
