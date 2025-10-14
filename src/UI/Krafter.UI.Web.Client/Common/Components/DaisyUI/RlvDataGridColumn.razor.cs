using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Column definition component for RlvDataGrid.
/// Defines how a column should be displayed, sorted, and filtered.
/// </summary>
/// <typeparam name="TItem">Type of data items in the grid.</typeparam>
public partial class RlvDataGridColumn<TItem> : ComponentBase, IDisposable
{
    // ========================================
    // CASCADING PARAMETERS
    // ========================================

    /// <summary>
    /// Reference to the parent data grid.
    /// </summary>
    [CascadingParameter]
    public RlvDataGrid<TItem>? DataGrid { get; set; }

    // ========================================
    // BASIC PROPERTIES
    // ========================================

    /// <summary>
    /// Column title/header text displayed in the header row.
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// Property name for data binding, sorting, and filtering.
    /// Should match a property name on TItem.
    /// </summary>
    [Parameter]
    public string? Property { get; set; }

    /// <summary>
    /// Custom template for rendering cell content.
    /// If provided, this overrides the default property value display.
    /// Context parameter provides access to the current row item.
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? Template { get; set; }

    /// <summary>
    /// Custom template for rendering header content.
    /// If provided, this overrides the Title property.
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; }

    // ========================================
    // FEATURES
    // ========================================

    /// <summary>
    /// Whether this column can be sorted.
    /// Requires Property to be set.
    /// Default: true
    /// </summary>
    [Parameter]
    public bool Sortable { get; set; } = true;

    /// <summary>
    /// Whether this column can be filtered.
    /// Requires Property to be set.
    /// Default: true
    /// </summary>
    [Parameter]
    public bool Filterable { get; set; } = true;

    /// <summary>
    /// Type of filter control to display for this column.
    /// Default: Text
    /// </summary>
    [Parameter]
    public FilterType FilterType { get; set; } = FilterType.Text;

    /// <summary>
    /// Filter options for select-type filters.
    /// Only used when FilterType is Select.
    /// </summary>
    [Parameter]
    public IEnumerable<FilterOption>? FilterOptions { get; set; }

    // ========================================
    // DISPLAY
    // ========================================

    /// <summary>
    /// Text alignment for cells in this column.
    /// Default: Left
    /// </summary>
    [Parameter]
    public TextAlign TextAlign { get; set; } = TextAlign.Left;

    /// <summary>
    /// Column width (CSS value: "100px", "20%", "auto", etc.).
    /// If not set, column width is determined automatically.
    /// </summary>
    [Parameter]
    public string? Width { get; set; }

    /// <summary>
    /// Whether this column is frozen (sticky during horizontal scroll).
    /// Default: false
    /// </summary>
    [Parameter]
    public bool Frozen { get; set; }

    /// <summary>
    /// Position of the frozen column (left or right side).
    /// Only used when Frozen is true.
    /// Default: Left
    /// </summary>
    [Parameter]
    public FrozenPosition FrozenPosition { get; set; } = FrozenPosition.Left;

    /// <summary>
    /// Whether this column is visible.
    /// Can be used to conditionally show/hide columns.
    /// Default: true
    /// </summary>
    [Parameter]
    public bool Visible { get; set; } = true;

    /// <summary>
    /// Additional CSS classes to apply to cells in this column.
    /// </summary>
    [Parameter]
    public string? CellClass { get; set; }

    /// <summary>
    /// Additional CSS classes to apply to the header cell.
    /// </summary>
    [Parameter]
    public string? HeaderClass { get; set; }

    // ========================================
    // LIFECYCLE METHODS
    // ========================================

    /// <summary>
    /// Initialize and register this column with the parent grid.
    /// </summary>
    protected override void OnInitialized()
    {
        if (DataGrid == null)
        {
            throw new InvalidOperationException(
                $"{nameof(RlvDataGridColumn<TItem>)} must be used inside a {nameof(RlvDataGrid<TItem>)} component.");
        }

        DataGrid.RegisterColumn(this);
    }

    /// <summary>
    /// Unregister this column when disposed.
    /// </summary>
    public void Dispose()
    {
        DataGrid?.UnregisterColumn(this);
    }
}
