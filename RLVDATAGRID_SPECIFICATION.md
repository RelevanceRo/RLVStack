# RlvDataGrid Component Specification

**Purpose**: Replace RadzenDataGrid with a fully-featured DaisyUI-based data grid component
**Status**: Planning Phase
**Priority**: CRITICAL - Required for all CRUD pages
**Location**: `Common/Components/DaisyUI/RlvDataGrid.razor[.cs]`

---

## Overview

RlvDataGrid is a comprehensive data grid wrapper that provides:
- **Pagination** - Page-based navigation with configurable page sizes
- **Sorting** - Column-based sorting (ascending/descending)
- **Filtering** - Column-level filtering
- **Loading states** - Skeleton loader during data fetch
- **Responsive design** - Mobile-friendly table rendering
- **Action columns** - Dropdown menus for row actions
- **Permission-based rendering** - Show/hide columns and actions based on permissions

Built on top of **RlvTable** with additional components:
- RlvPagination for navigation
- RlvDropdown for actions
- RlvInputField for filters
- RlvSkeleton for loading
- RlvButton for controls

---

## Component Structure

### File Organization
```
Common/Components/DaisyUI/
├── RlvDataGrid.razor           # Main component markup
├── RlvDataGrid.razor.cs        # Component code-behind
├── RlvDataGridColumn.razor     # Column definition component
└── RlvDataGridColumn.razor.cs  # Column code-behind
```

---

## API Design

### RlvDataGrid Parameters

```csharp
/// <summary>
/// Main DataGrid component.
/// </summary>
public partial class RlvDataGrid<TItem> : ComponentBase
{
    // ========================================
    // DATA PARAMETERS
    // ========================================

    /// <summary>
    /// Collection of data items to display.
    /// </summary>
    [Parameter]
    public IEnumerable<TItem>? Data { get; set; }

    /// <summary>
    /// Total count of items (for pagination calculation).
    /// </summary>
    [Parameter]
    public int TotalCount { get; set; }

    /// <summary>
    /// Current page number (zero-based).
    /// </summary>
    [Parameter]
    public int CurrentPage { get; set; }

    /// <summary>
    /// Number of items per page.
    /// </summary>
    [Parameter]
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Available page size options.
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
    /// Whether data is currently loading.
    /// </summary>
    [Parameter]
    public bool IsLoading { get; set; }

    /// <summary>
    /// Callback when data needs to be loaded (page change, sort, filter).
    /// </summary>
    [Parameter]
    public EventCallback<DataGridLoadArgs> OnLoadData { get; set; }

    // ========================================
    // FEATURES
    // ========================================

    /// <summary>
    /// Enable pagination.
    /// </summary>
    [Parameter]
    public bool AllowPaging { get; set; } = true;

    /// <summary>
    /// Enable sorting.
    /// </summary>
    [Parameter]
    public bool AllowSorting { get; set; } = true;

    /// <summary>
    /// Enable filtering.
    /// </summary>
    [Parameter]
    public bool AllowFiltering { get; set; } = true;

    /// <summary>
    /// Show page size selector.
    /// </summary>
    [Parameter]
    public bool ShowPageSizeSelector { get; set; } = true;

    // ========================================
    // STYLING
    // ========================================

    /// <summary>
    /// Table size.
    /// </summary>
    [Parameter]
    public TableSize Size { get; set; } = TableSize.Medium;

    /// <summary>
    /// Enable zebra striping.
    /// </summary>
    [Parameter]
    public bool Zebra { get; set; } = true;

    /// <summary>
    /// Pin header row.
    /// </summary>
    [Parameter]
    public bool PinHeader { get; set; } = true;

    /// <summary>
    /// Additional CSS classes.
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// Additional attributes.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    // ========================================
    // EMPTY STATE
    // ========================================

    /// <summary>
    /// Content to display when no data is available.
    /// </summary>
    [Parameter]
    public RenderFragment? EmptyContent { get; set; }

    /// <summary>
    /// Default empty message text.
    /// </summary>
    [Parameter]
    public string EmptyMessage { get; set; } = "No data available";
}
```

### RlvDataGridColumn Parameters

```csharp
/// <summary>
/// Column definition for RlvDataGrid.
/// </summary>
public partial class RlvDataGridColumn<TItem> : ComponentBase
{
    // ========================================
    // BASIC PROPERTIES
    // ========================================

    /// <summary>
    /// Column title/header text.
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// Property name for sorting and filtering.
    /// </summary>
    [Parameter]
    public string? Property { get; set; }

    /// <summary>
    /// Custom template for cell content.
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? Template { get; set; }

    /// <summary>
    /// Custom template for header content.
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; }

    // ========================================
    // FEATURES
    // ========================================

    /// <summary>
    /// Whether this column is sortable.
    /// </summary>
    [Parameter]
    public bool Sortable { get; set; } = true;

    /// <summary>
    /// Whether this column is filterable.
    /// </summary>
    [Parameter]
    public bool Filterable { get; set; } = true;

    /// <summary>
    /// Type of filter (text, number, date, select).
    /// </summary>
    [Parameter]
    public FilterType FilterType { get; set; } = FilterType.Text;

    /// <summary>
    /// Filter options for select filter type.
    /// </summary>
    [Parameter]
    public IEnumerable<FilterOption>? FilterOptions { get; set; }

    // ========================================
    // DISPLAY
    // ========================================

    /// <summary>
    /// Text alignment for this column.
    /// </summary>
    [Parameter]
    public TextAlign TextAlign { get; set; } = TextAlign.Left;

    /// <summary>
    /// Column width (CSS value: "100px", "20%", "auto").
    /// </summary>
    [Parameter]
    public string? Width { get; set; }

    /// <summary>
    /// Whether this column is frozen (sticky).
    /// </summary>
    [Parameter]
    public bool Frozen { get; set; }

    /// <summary>
    /// Frozen position (left or right).
    /// </summary>
    [Parameter]
    public FrozenPosition FrozenPosition { get; set; } = FrozenPosition.Left;

    /// <summary>
    /// Whether this column is visible.
    /// </summary>
    [Parameter]
    public bool Visible { get; set; } = true;

    /// <summary>
    /// CSS classes for cells in this column.
    /// </summary>
    [Parameter]
    public string? CellClass { get; set; }

    /// <summary>
    /// CSS classes for header in this column.
    /// </summary>
    [Parameter]
    public string? HeaderClass { get; set; }
}
```

### Supporting Classes

```csharp
/// <summary>
/// Arguments passed to OnLoadData callback.
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
/// Sort direction.
/// </summary>
public enum SortDirection
{
    None,
    Ascending,
    Descending
}

/// <summary>
/// Filter type for columns.
/// </summary>
public enum FilterType
{
    Text,
    Number,
    Date,
    Select,
    Boolean
}

/// <summary>
/// Filter option for select filters.
/// </summary>
public class FilterOption
{
    public string Label { get; set; } = "";
    public string Value { get; set; } = "";
}

/// <summary>
/// Text alignment options.
/// </summary>
public enum TextAlign
{
    Left,
    Center,
    Right
}

/// <summary>
/// Frozen column position.
/// </summary>
public enum FrozenPosition
{
    Left,
    Right
}
```

---

## Usage Examples

### Basic DataGrid

```razor
<RlvDataGrid TItem="UserDto"
             Data="@users"
             TotalCount="@totalCount"
             PageSize="@pageSize"
             IsLoading="@isLoading"
             OnLoadData="@HandleLoadData">
    <Columns>
        <RlvDataGridColumn TItem="UserDto" Property="FirstName" Title="First Name" />
        <RlvDataGridColumn TItem="UserDto" Property="LastName" Title="Last Name" />
        <RlvDataGridColumn TItem="UserDto" Property="Email" Title="Email" />
        <RlvDataGridColumn TItem="UserDto"
                          Title="Actions"
                          Sortable="false"
                          Filterable="false"
                          TextAlign="TextAlign.Right"
                          Frozen="true"
                          FrozenPosition="FrozenPosition.Right">
            <Template Context="user">
                <RlvDropdown>
                    <RlvButton Size="ButtonSize.Small" Text="Actions" />
                    <DropdownContent>
                        <ul class="menu">
                            <li><a @onclick="() => EditUser(user)">Edit</a></li>
                            <li><a @onclick="() => DeleteUser(user)">Delete</a></li>
                        </ul>
                    </DropdownContent>
                </RlvDropdown>
            </Template>
        </RlvDataGridColumn>
    </Columns>
</RlvDataGrid>
```

```csharp
// Code-behind
private List<UserDto> users = new();
private int totalCount = 0;
private int pageSize = 10;
private bool isLoading = false;

private async Task HandleLoadData(DataGridLoadArgs args)
{
    isLoading = true;

    // Call API with pagination, sorting, filtering
    var response = await krafterClient.Users.Get.GetAsync(config => {
        config.QueryParameters.SkipCount = args.Skip;
        config.QueryParameters.MaxResultCount = args.Take;
        config.QueryParameters.OrderBy = args.SortBy != null
            ? $"{args.SortBy} {(args.SortDirection == SortDirection.Ascending ? "asc" : "desc")}"
            : null;
        // Apply filters from args.Filters dictionary
    });

    users = response.Data.Items.ToList();
    totalCount = response.Data.TotalCount;

    isLoading = false;
    StateHasChanged();
}
```

### Advanced DataGrid with Custom Templates

```razor
<RlvDataGrid TItem="UserDto"
             Data="@users"
             TotalCount="@totalCount"
             PageSize="@pageSize"
             IsLoading="@isLoading"
             OnLoadData="@HandleLoadData"
             Zebra="true"
             Size="TableSize.Small"
             AllowPaging="true"
             AllowSorting="true"
             AllowFiltering="true">
    <Columns>
        <RlvDataGridColumn TItem="UserDto" Property="Id" Title="ID" Width="80px" />

        <RlvDataGridColumn TItem="UserDto" Property="FirstName" Title="First Name" />

        <RlvDataGridColumn TItem="UserDto" Property="Email" Title="Email" FilterType="FilterType.Text" />

        <RlvDataGridColumn TItem="UserDto" Property="IsActive" Title="Active" FilterType="FilterType.Boolean">
            <Template Context="user">
                @if (user.IsActive)
                {
                    <RlvBadge Color="BadgeColor.Success">Active</RlvBadge>
                }
                else
                {
                    <RlvBadge Color="BadgeColor.Error">Inactive</RlvBadge>
                }
            </Template>
        </RlvDataGridColumn>

        <RlvDataGridColumn TItem="UserDto" Property="CreatedAt" Title="Created" FilterType="FilterType.Date">
            <Template Context="user">
                @user.CreatedAt?.ToString("MMM dd, yyyy")
            </Template>
        </RlvDataGridColumn>
    </Columns>
</RlvDataGrid>
```

---

## Component Layout Structure

### Main Grid Structure

```
┌─────────────────────────────────────────────────┐
│ Toolbar (optional)                              │
│ [Page Size] [Search] [Custom Actions]          │
├─────────────────────────────────────────────────┤
│ Filter Row (if AllowFiltering)                  │
│ [Filter 1] [Filter 2] [Filter 3] [Clear]       │
├─────────────────────────────────────────────────┤
│ Table Header                                    │
│ [Column 1 ↑] [Column 2] [Column 3 ↓]          │
├─────────────────────────────────────────────────┤
│ Table Body (or Skeleton)                        │
│ Row 1: Cell 1 | Cell 2 | Cell 3               │
│ Row 2: Cell 1 | Cell 2 | Cell 3               │
│ Row 3: Cell 1 | Cell 2 | Cell 3               │
├─────────────────────────────────────────────────┤
│ Pagination                                      │
│ [◀ Previous] [1] [2] [3] [Next ▶]             │
│ Showing 1-10 of 100 items                      │
└─────────────────────────────────────────────────┘
```

### Responsive Mobile Layout

```
┌───────────────────────┐
│ [Page Size ▼] [🔍]   │
├───────────────────────┤
│ Card View (Stacked)   │
│ ┌───────────────────┐ │
│ │ First Name: John  │ │
│ │ Last Name: Doe    │ │
│ │ Email: john@...   │ │
│ │ [Actions ▼]       │ │
│ └───────────────────┘ │
│ ┌───────────────────┐ │
│ │ First Name: Jane  │ │
│ │ ...               │ │
│ └───────────────────┘ │
├───────────────────────┤
│ [◀] [1] [2] [▶]     │
└───────────────────────┘
```

---

## Implementation Phases

### Phase 2.4: Create RlvDataGrid Component

**Files to create**:
1. `Common/Components/DaisyUI/RlvDataGrid.razor`
2. `Common/Components/DaisyUI/RlvDataGrid.razor.cs`
3. `Common/Components/DaisyUI/RlvDataGridColumn.razor`
4. `Common/Components/DaisyUI/RlvDataGridColumn.razor.cs`

**Step 1: Basic Structure**
- Create component files
- Define parameters
- Implement basic table rendering with RlvTable
- Test with simple data

**Step 2: Pagination**
- Implement pagination UI with RlvPagination
- Add page size selector with RlvSelect
- Implement page navigation logic
- Test pagination with large datasets

**Step 3: Sorting**
- Add sort indicators to column headers
- Implement sort state management
- Trigger OnLoadData on sort change
- Test sorting on multiple columns

**Step 4: Filtering**
- Add filter row above table
- Implement different filter types (text, number, date, select, boolean)
- Add filter state management
- Implement debounced filter trigger
- Add "Clear Filters" button

**Step 5: Loading States**
- Add skeleton loader using RlvSkeleton
- Implement loading overlay
- Test loading transitions

**Step 6: Actions Column**
- Implement frozen column support
- Add action dropdown template
- Test with permission-based rendering

**Step 7: Responsive Design**
- Add mobile card view
- Implement breakpoint detection
- Test on various screen sizes

**Step 8: Empty State**
- Implement empty data message
- Add custom empty content support
- Test with no data

---

## Technical Considerations

### State Management
- Column definitions stored in `List<ColumnDefinition>`
- Sort state: `currentSortProperty`, `currentSortDirection`
- Filter state: `Dictionary<string, string> activeFilters`
- Pagination state: `currentPage`, `pageSize`

### Performance Optimizations
- Virtualization for large datasets (future enhancement)
- Debounced filtering (500ms default)
- Memoized column rendering
- Efficient re-rendering with `@key` directive

### Accessibility
- ARIA labels for all interactive elements
- Keyboard navigation support (Tab, Arrow keys)
- Screen reader announcements for sort/filter changes
- Focus management

### Testing Checklist
- [ ] Basic rendering with data
- [ ] Empty state rendering
- [ ] Loading state display
- [ ] Pagination navigation
- [ ] Page size change
- [ ] Column sorting (ascending/descending)
- [ ] Text filtering
- [ ] Number filtering
- [ ] Date filtering
- [ ] Select filtering
- [ ] Boolean filtering
- [ ] Clear filters functionality
- [ ] Actions dropdown
- [ ] Frozen columns
- [ ] Custom templates
- [ ] Mobile responsive view
- [ ] Permission-based column visibility
- [ ] Large dataset performance (1000+ rows)
- [ ] Keyboard navigation
- [ ] Screen reader compatibility

---

## Migration Pattern: RadzenDataGrid → RlvDataGrid

### Before (RadzenDataGrid)

```razor
<RadzenDataGrid @ref="grid"
                IsLoading=@IsLoading
                Count=@(response?.Data?.TotalCount??0)
                LoadData=@LoadData
                AllowSorting=true
                Data="@response.Data.Items"
                AllowFiltering="true"
                AllowPaging="true"
                PageSize="@requestInput.MaxResultCount">
    <Columns>
        <RadzenDataGridColumn Property="FirstName" Title="First Name" />
        <RadzenDataGridColumn Property="Email" Title="Email" />
        <RadzenDataGridColumn Title="Actions" Filterable="false" Sortable="false">
            <Template Context="data">
                <RadzenSplitButton Click=@(args => ActionClicked(args, data)) Text="Actions">
                    <ChildContent>
                        <RadzenSplitButtonItem Text="Edit" Value="@KrafterAction.Update" Icon="edit" />
                        <RadzenSplitButtonItem Text="Delete" Value="@KrafterAction.Delete" Icon="delete" />
                    </ChildContent>
                </RadzenSplitButton>
            </Template>
        </RadzenDataGridColumn>
    </Columns>
</RadzenDataGrid>
```

### After (RlvDataGrid)

```razor
<RlvDataGrid TItem="UserDto"
             Data="@users"
             TotalCount="@totalCount"
             PageSize="@pageSize"
             IsLoading="@isLoading"
             OnLoadData="@HandleLoadData"
             AllowSorting="true"
             AllowFiltering="true"
             AllowPaging="true">
    <Columns>
        <RlvDataGridColumn TItem="UserDto" Property="FirstName" Title="First Name" />
        <RlvDataGridColumn TItem="UserDto" Property="Email" Title="Email" />
        <RlvDataGridColumn TItem="UserDto"
                          Title="Actions"
                          Sortable="false"
                          Filterable="false"
                          Frozen="true"
                          FrozenPosition="FrozenPosition.Right">
            <Template Context="user">
                <RlvDropdown Alignment="DropdownAlignment.End">
                    <RlvButton Size="ButtonSize.Small" Style="ButtonStyle.Ghost">
                        Actions
                    </RlvButton>
                    <DropdownContent>
                        <ul class="menu menu-sm">
                            <li>
                                <a @onclick="() => EditUser(user)" class="flex gap-2">
                                    <span class="icon-edit"></span> Edit
                                </a>
                            </li>
                            <li>
                                <a @onclick="() => DeleteUser(user)" class="flex gap-2">
                                    <span class="icon-delete"></span> Delete
                                </a>
                            </li>
                        </ul>
                    </DropdownContent>
                </RlvDropdown>
            </Template>
        </RlvDataGridColumn>
    </Columns>
</RlvDataGrid>
```

### Code-Behind Changes

```csharp
// Before (Radzen)
private RadzenDataGrid<UserDto> grid;
private GetRequestInput requestInput = new();
private UserDtoPaginationResponseResponse? response = new();

private async Task LoadData(LoadDataArgs args)
{
    requestInput.SkipCount = args.Skip ?? 0;
    requestInput.MaxResultCount = args.Top ?? 10;
    requestInput.Filter = args.Filter;
    requestInput.OrderBy = args.OrderBy;
    await GetListAsync();
}

// After (RlvDataGrid)
private List<UserDto> users = new();
private int totalCount = 0;
private int pageSize = 10;
private bool isLoading = false;

private async Task HandleLoadData(DataGridLoadArgs args)
{
    isLoading = true;

    var response = await krafterClient.Users.Get.GetAsync(config => {
        config.QueryParameters.SkipCount = args.Skip;
        config.QueryParameters.MaxResultCount = args.Take;
        config.QueryParameters.OrderBy = BuildOrderByString(args.SortBy, args.SortDirection);
        // Apply filters from args.Filters
    });

    users = response.Data.Items.ToList();
    totalCount = response.Data.TotalCount;

    isLoading = false;
}

private string? BuildOrderByString(string? sortBy, SortDirection direction)
{
    if (string.IsNullOrEmpty(sortBy)) return null;
    return $"{sortBy} {(direction == SortDirection.Ascending ? "asc" : "desc")}";
}
```

---

## Future Enhancements (Post-MVP)

- [ ] Column resizing
- [ ] Column reordering (drag-drop)
- [ ] Column visibility toggle
- [ ] Export to CSV/Excel
- [ ] Multi-column sorting
- [ ] Advanced filter builder (AND/OR conditions)
- [ ] Row selection (checkbox)
- [ ] Bulk actions
- [ ] Inline editing
- [ ] Expandable rows
- [ ] Grouping/aggregation
- [ ] Virtual scrolling for large datasets
- [ ] Customizable toolbar

---

**Document Version**: 1.0
**Created**: 2025-10-14
**Status**: Approved for Implementation
**Next Step**: Implement in Phase 2.4
