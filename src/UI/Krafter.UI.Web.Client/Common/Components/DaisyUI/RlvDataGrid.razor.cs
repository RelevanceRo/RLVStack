using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Advanced data grid component with pagination, sorting, filtering, and loading states.
/// Built on top of RlvTable with comprehensive CRUD support.
/// </summary>
/// <typeparam name="TItem">Type of data items in the grid.</typeparam>
public partial class RlvDataGrid<TItem> : ComponentBase
{
    // ========================================
    // DATA PARAMETERS
    // ========================================

    /// <summary>
    /// Collection of data items to display in the grid.
    /// </summary>
    [Parameter]
    public IEnumerable<TItem>? Data { get; set; }

    /// <summary>
    /// Total count of items across all pages (for pagination calculation).
    /// </summary>
    [Parameter]
    public int TotalCount { get; set; }

    /// <summary>
    /// Number of items to display per page.
    /// Default: 10
    /// </summary>
    [Parameter]
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Available page size options for the page size selector.
    /// Default: [10, 25, 50, 100]
    /// </summary>
    [Parameter]
    public int[] PageSizeOptions { get; set; } = new[] { 10, 25, 50, 100 };

    // ========================================
    // COLUMNS PARAMETERS
    // ========================================

    /// <summary>
    /// Column definitions (RlvDataGridColumn components).
    /// </summary>
    [Parameter]
    public RenderFragment? Columns { get; set; }

    // ========================================
    // LOADING & EVENTS
    // ========================================

    /// <summary>
    /// Whether data is currently being loaded.
    /// Shows skeleton loader when true.
    /// </summary>
    [Parameter]
    public bool IsLoading { get; set; }

    /// <summary>
    /// Callback invoked when data needs to be loaded.
    /// Triggered by page change, sorting, filtering, or page size change.
    /// </summary>
    [Parameter]
    public EventCallback<DataGridLoadArgs> OnLoadData { get; set; }

    // ========================================
    // FEATURES
    // ========================================

    /// <summary>
    /// Enable pagination controls.
    /// Default: true
    /// </summary>
    [Parameter]
    public bool AllowPaging { get; set; } = true;

    /// <summary>
    /// Enable column sorting.
    /// Default: true
    /// </summary>
    [Parameter]
    public bool AllowSorting { get; set; } = true;

    /// <summary>
    /// Enable column filtering.
    /// Default: true
    /// </summary>
    [Parameter]
    public bool AllowFiltering { get; set; } = true;

    /// <summary>
    /// Show page size selector dropdown.
    /// Default: true
    /// </summary>
    [Parameter]
    public bool ShowPageSizeSelector { get; set; } = true;

    /// <summary>
    /// Show item count summary (e.g., "Showing 1-10 of 100 items").
    /// Default: true
    /// </summary>
    [Parameter]
    public bool ShowItemCountSummary { get; set; } = true;

    // ========================================
    // STYLING
    // ========================================

    /// <summary>
    /// Table size variant.
    /// Default: Medium
    /// </summary>
    [Parameter]
    public TableSize Size { get; set; } = TableSize.Medium;

    /// <summary>
    /// Enable zebra striping on table rows.
    /// Default: true
    /// </summary>
    [Parameter]
    public bool Zebra { get; set; } = true;

    /// <summary>
    /// Make header row sticky (pin to top during scroll).
    /// Default: true
    /// </summary>
    [Parameter]
    public bool PinHeader { get; set; } = true;

    /// <summary>
    /// Additional CSS classes for the grid container.
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// Additional attributes for the grid container.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    // ========================================
    // EMPTY STATE
    // ========================================

    /// <summary>
    /// Custom content to display when no data is available.
    /// If not provided, EmptyMessage is displayed.
    /// </summary>
    [Parameter]
    public RenderFragment? EmptyContent { get; set; }

    /// <summary>
    /// Default message to display when no data is available.
    /// Default: "No data available"
    /// </summary>
    [Parameter]
    public string EmptyMessage { get; set; } = "No data available";

    // ========================================
    // INTERNAL STATE
    // ========================================

    /// <summary>
    /// Current page number (zero-based).
    /// </summary>
    private int currentPage = 0;

    /// <summary>
    /// Current sort property name.
    /// </summary>
    private string? currentSortBy;

    /// <summary>
    /// Current sort direction.
    /// </summary>
    private SortDirection currentSortDirection = SortDirection.None;

    /// <summary>
    /// Active column filters (property name -> filter value).
    /// </summary>
    private Dictionary<string, string> activeFilters = new();

    /// <summary>
    /// Registered columns.
    /// </summary>
    internal List<RlvDataGridColumn<TItem>> RegisteredColumns { get; } = new();

    // ========================================
    // COMPUTED PROPERTIES
    // ========================================

    /// <summary>
    /// Total number of pages.
    /// </summary>
    private int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;

    /// <summary>
    /// Current page number for display (one-based).
    /// </summary>
    private int DisplayPageNumber => currentPage + 1;

    /// <summary>
    /// Index of first item on current page (one-based).
    /// </summary>
    private int FirstItemIndex => TotalCount == 0 ? 0 : (currentPage * PageSize) + 1;

    /// <summary>
    /// Index of last item on current page (one-based).
    /// </summary>
    private int LastItemIndex => Math.Min((currentPage + 1) * PageSize, TotalCount);

    /// <summary>
    /// Whether data is available.
    /// </summary>
    private bool HasData => Data != null && Data.Any();

    /// <summary>
    /// Whether to show empty state.
    /// </summary>
    private bool ShowEmptyState => !IsLoading && !HasData;

    // ========================================
    // LIFECYCLE METHODS
    // ========================================

    protected override async Task OnInitializedAsync()
    {
        await LoadDataAsync();
    }

    // ========================================
    // COLUMN REGISTRATION
    // ========================================

    /// <summary>
    /// Register a column with the data grid.
    /// Called by child RlvDataGridColumn components.
    /// </summary>
    internal void RegisterColumn(RlvDataGridColumn<TItem> column)
    {
        if (!RegisteredColumns.Contains(column))
        {
            RegisteredColumns.Add(column);
            StateHasChanged();
        }
    }

    /// <summary>
    /// Unregister a column from the data grid.
    /// Called when a column is disposed.
    /// </summary>
    internal void UnregisterColumn(RlvDataGridColumn<TItem> column)
    {
        RegisteredColumns.Remove(column);
        StateHasChanged();
    }

    // ========================================
    // DATA LOADING
    // ========================================

    /// <summary>
    /// Load data with current parameters.
    /// </summary>
    private async Task LoadDataAsync()
    {
        if (OnLoadData.HasDelegate)
        {
            var args = new DataGridLoadArgs
            {
                Skip = currentPage * PageSize,
                Take = PageSize,
                SortBy = currentSortBy,
                SortDirection = currentSortDirection,
                Filters = new Dictionary<string, string>(activeFilters)
            };

            await OnLoadData.InvokeAsync(args);
        }
    }

    // ========================================
    // PAGINATION METHODS
    // ========================================

    /// <summary>
    /// Navigate to first page.
    /// </summary>
    private async Task GoToFirstPage()
    {
        if (currentPage != 0)
        {
            currentPage = 0;
            await LoadDataAsync();
        }
    }

    /// <summary>
    /// Navigate to previous page.
    /// </summary>
    private async Task GoToPreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            await LoadDataAsync();
        }
    }

    /// <summary>
    /// Navigate to specific page.
    /// </summary>
    private async Task GoToPage(int pageIndex)
    {
        if (pageIndex >= 0 && pageIndex < TotalPages && pageIndex != currentPage)
        {
            currentPage = pageIndex;
            await LoadDataAsync();
        }
    }

    /// <summary>
    /// Navigate to next page.
    /// </summary>
    private async Task GoToNextPage()
    {
        if (currentPage < TotalPages - 1)
        {
            currentPage++;
            await LoadDataAsync();
        }
    }

    /// <summary>
    /// Navigate to last page.
    /// </summary>
    private async Task GoToLastPage()
    {
        if (currentPage != TotalPages - 1)
        {
            currentPage = TotalPages - 1;
            await LoadDataAsync();
        }
    }

    /// <summary>
    /// Change page size and reload data.
    /// </summary>
    private async Task ChangePageSize(int newPageSize)
    {
        if (newPageSize != PageSize && newPageSize > 0)
        {
            PageSize = newPageSize;
            currentPage = 0; // Reset to first page
            await LoadDataAsync();
        }
    }

    // ========================================
    // SORTING METHODS
    // ========================================

    /// <summary>
    /// Toggle sort for a column.
    /// </summary>
    internal async Task ToggleSort(string propertyName)
    {
        if (!AllowSorting || string.IsNullOrEmpty(propertyName))
            return;

        if (currentSortBy == propertyName)
        {
            // Cycle through: None -> Ascending -> Descending -> None
            currentSortDirection = currentSortDirection switch
            {
                SortDirection.None => SortDirection.Ascending,
                SortDirection.Ascending => SortDirection.Descending,
                SortDirection.Descending => SortDirection.None,
                _ => SortDirection.None
            };

            if (currentSortDirection == SortDirection.None)
            {
                currentSortBy = null;
            }
        }
        else
        {
            // Sort by new column (ascending)
            currentSortBy = propertyName;
            currentSortDirection = SortDirection.Ascending;
        }

        currentPage = 0; // Reset to first page
        await LoadDataAsync();
    }

    /// <summary>
    /// Get current sort direction for a property.
    /// </summary>
    internal SortDirection GetSortDirection(string propertyName)
    {
        return currentSortBy == propertyName ? currentSortDirection : SortDirection.None;
    }

    // ========================================
    // FILTERING METHODS
    // ========================================

    /// <summary>
    /// Apply filter for a column.
    /// </summary>
    internal async Task ApplyFilter(string propertyName, string? filterValue)
    {
        if (!AllowFiltering || string.IsNullOrEmpty(propertyName))
            return;

        if (string.IsNullOrWhiteSpace(filterValue))
        {
            activeFilters.Remove(propertyName);
        }
        else
        {
            activeFilters[propertyName] = filterValue;
        }

        currentPage = 0; // Reset to first page
        await LoadDataAsync();
    }

    /// <summary>
    /// Get current filter value for a property.
    /// </summary>
    internal string? GetFilterValue(string propertyName)
    {
        return activeFilters.TryGetValue(propertyName, out var value) ? value : null;
    }

    /// <summary>
    /// Clear all filters.
    /// </summary>
    private async Task ClearAllFilters()
    {
        if (activeFilters.Any())
        {
            activeFilters.Clear();
            currentPage = 0; // Reset to first page
            await LoadDataAsync();
        }
    }

    /// <summary>
    /// Whether any filters are active.
    /// </summary>
    private bool HasActiveFilters => activeFilters.Any();

    // ========================================
    // REFRESH METHOD
    // ========================================

    /// <summary>
    /// Refresh data with current parameters.
    /// Public method to allow parent components to trigger refresh.
    /// </summary>
    public async Task RefreshAsync()
    {
        await LoadDataAsync();
    }
}

// ========================================
// SUPPORTING CLASSES & ENUMS
// ========================================

/// <summary>
/// Arguments passed to OnLoadData callback.
/// Contains all parameters needed to load data from the server.
/// </summary>
public class DataGridLoadArgs
{
    /// <summary>
    /// Number of items to skip (for pagination).
    /// </summary>
    public int Skip { get; set; }

    /// <summary>
    /// Number of items to take (page size).
    /// </summary>
    public int Take { get; set; }

    /// <summary>
    /// Property name to sort by.
    /// Null if no sorting is applied.
    /// </summary>
    public string? SortBy { get; set; }

    /// <summary>
    /// Sort direction.
    /// </summary>
    public SortDirection SortDirection { get; set; }

    /// <summary>
    /// Column filters (property name -> filter value).
    /// </summary>
    public Dictionary<string, string> Filters { get; set; } = new();
}

/// <summary>
/// Sort direction for columns.
/// </summary>
public enum SortDirection
{
    /// <summary>
    /// No sorting applied.
    /// </summary>
    None,

    /// <summary>
    /// Ascending sort (A-Z, 0-9).
    /// </summary>
    Ascending,

    /// <summary>
    /// Descending sort (Z-A, 9-0).
    /// </summary>
    Descending
}

/// <summary>
/// Filter type for columns.
/// </summary>
public enum FilterType
{
    /// <summary>
    /// Text filter (string input).
    /// </summary>
    Text,

    /// <summary>
    /// Number filter (numeric input).
    /// </summary>
    Number,

    /// <summary>
    /// Date filter (date picker).
    /// </summary>
    Date,

    /// <summary>
    /// Select filter (dropdown with predefined options).
    /// </summary>
    Select,

    /// <summary>
    /// Boolean filter (true/false/all).
    /// </summary>
    Boolean
}

/// <summary>
/// Filter option for select-type filters.
/// </summary>
public class FilterOption
{
    /// <summary>
    /// Display label for the option.
    /// </summary>
    public string Label { get; set; } = "";

    /// <summary>
    /// Value of the option.
    /// </summary>
    public string Value { get; set; } = "";
}

/// <summary>
/// Text alignment options for columns.
/// </summary>
public enum TextAlign
{
    /// <summary>
    /// Left-aligned text.
    /// </summary>
    Left,

    /// <summary>
    /// Center-aligned text.
    /// </summary>
    Center,

    /// <summary>
    /// Right-aligned text.
    /// </summary>
    Right
}

/// <summary>
/// Frozen column position.
/// </summary>
public enum FrozenPosition
{
    /// <summary>
    /// Pin column to left side.
    /// </summary>
    Left,

    /// <summary>
    /// Pin column to right side.
    /// </summary>
    Right
}
