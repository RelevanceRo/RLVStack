using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Stack component for visually putting elements on top of each other.
/// Stacks children elements with configurable alignment.
/// </summary>
public partial class RlvStack
{
    /// <summary>
    /// Child content to stack.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Vertical alignment of stacked elements.
    /// </summary>
    [Parameter]
    public StackVerticalAlignment VerticalAlignment { get; set; } = StackVerticalAlignment.Bottom;

    /// <summary>
    /// Horizontal alignment of stacked elements.
    /// </summary>
    [Parameter]
    public StackHorizontalAlignment HorizontalAlignment { get; set; } = StackHorizontalAlignment.Center;

    /// <summary>
    /// Additional attributes for the stack element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Builds the CSS class string for the stack.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string> { "stack" };

            // Vertical alignment - Bottom is default (no class needed)
            if (VerticalAlignment == StackVerticalAlignment.Top)
            {
                classes.Add("stack-top");
            }

            // Horizontal alignment - Center is default (no class needed)
            classes.Add(HorizontalAlignment switch
            {
                StackHorizontalAlignment.Start => "stack-start",
                StackHorizontalAlignment.End => "stack-end",
                _ => ""
            });

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }
}

/// <summary>
/// Vertical alignment options for stack elements.
/// </summary>
public enum StackVerticalAlignment
{
    Top,
    Bottom
}

/// <summary>
/// Horizontal alignment options for stack elements.
/// </summary>
public enum StackHorizontalAlignment
{
    Start,
    Center,
    End
}
