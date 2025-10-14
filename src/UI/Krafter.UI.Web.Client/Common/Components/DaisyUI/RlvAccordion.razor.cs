using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Accordion container component for grouping multiple accordion items.
/// Provides a shared name for all child items and optional join styling.
/// </summary>
public partial class RlvAccordion
{
    /// <summary>
    /// Shared name for all accordion items in this group.
    /// If not provided, a unique name will be generated.
    /// All items with the same name work together (only one can be open).
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    /// <summary>
    /// Whether to use join-vertical styling to connect the items.
    /// Default: false
    /// </summary>
    [Parameter]
    public bool UseJoin { get; set; }

    /// <summary>
    /// Child content - typically contains multiple RlvAccordionItem components.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional CSS classes for the accordion container.
    /// </summary>
    [Parameter]
    public string? GroupClass { get; set; }

    /// <summary>
    /// Additional attributes for the accordion container element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Gets the name to use for accordion items in this group.
    /// </summary>
    public string AccordionName => Name ?? _generatedName;

    private readonly string _generatedName = $"accordion-{Guid.NewGuid():N}";
}
