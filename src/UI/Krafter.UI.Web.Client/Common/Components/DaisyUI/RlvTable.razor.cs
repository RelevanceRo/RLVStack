using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvTable
{
    /// <summary>
    /// Size of the table.
    /// </summary>
    [Parameter]
    public TableSize Size { get; set; } = TableSize.Medium;

    /// <summary>
    /// Enable zebra striping for table rows.
    /// </summary>
    [Parameter]
    public bool Zebra { get; set; }

    /// <summary>
    /// Make thead and tfoot rows sticky.
    /// </summary>
    [Parameter]
    public bool PinRows { get; set; }

    /// <summary>
    /// Make th columns sticky.
    /// </summary>
    [Parameter]
    public bool PinCols { get; set; }

    /// <summary>
    /// Table content (thead, tbody, tfoot).
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional CSS classes for the table element.
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// Wrap table with overflow-x-auto div for horizontal scrolling.
    /// </summary>
    [Parameter]
    public bool OverflowWrapper { get; set; } = true;

    /// <summary>
    /// Additional CSS classes for the overflow wrapper div.
    /// </summary>
    [Parameter]
    public string? WrapperClass { get; set; }

    /// <summary>
    /// Captures additional HTML attributes.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string TableCssClass
    {
        get
        {
            var classes = new List<string> { "table" };

            // Size (Medium is default, no class needed)
            classes.Add(Size switch
            {
                TableSize.XSmall => "table-xs",
                TableSize.Small => "table-sm",
                TableSize.Medium => "", // Default size, no class
                TableSize.Large => "table-lg",
                TableSize.XLarge => "table-xl",
                _ => "" // Default
            });

            if (Zebra) classes.Add("table-zebra");
            if (PinRows) classes.Add("table-pin-rows");
            if (PinCols) classes.Add("table-pin-cols");
            if (!string.IsNullOrWhiteSpace(Class)) classes.Add(Class);

            return string.Join(" ", classes);
        }
    }

    private string WrapperCssClass => $"overflow-x-auto {WrapperClass}".Trim();
}

public enum TableSize
{
    XSmall,
    Small,
    Medium,
    Large,
    XLarge
}
