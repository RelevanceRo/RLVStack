using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Rating component with DaisyUI styling and Blazor form integration.
/// Each rating group should have a unique name attribute.
/// For half-star ratings, Value represents half-stars (e.g., 3 = 1.5 stars, 10 = 5 stars).
/// For full-star ratings, Value represents full stars (e.g., 3 = 3 stars, 5 = 5 stars).
/// </summary>
public partial class RlvRating : InputBase<int>
{
    /// <summary>
    /// Unique name for the rating radio button group. Required.
    /// </summary>
    [Parameter, EditorRequired]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Number of rating items (stars/hearts). Default is 5.
    /// </summary>
    [Parameter]
    public int Count { get; set; } = 5;

    /// <summary>
    /// Mask shape for rating items. Default is Star.
    /// </summary>
    [Parameter]
    public RatingShape Shape { get; set; } = RatingShape.Star;

    /// <summary>
    /// Size of the rating component.
    /// </summary>
    [Parameter]
    public RatingSize Size { get; set; } = RatingSize.Medium;

    /// <summary>
    /// Optional color class (e.g., "bg-orange-400", "bg-green-500").
    /// Applied to all rating items via Tailwind classes.
    /// </summary>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// Enable half-star ratings. When true, doubles the number of inputs.
    /// Value will represent half-stars (e.g., 3 = 1.5 stars, 10 = 5 stars).
    /// </summary>
    [Parameter]
    public bool Half { get; set; }

    /// <summary>
    /// Add a hidden radio button at the start to allow users to clear their rating.
    /// </summary>
    [Parameter]
    public bool AllowClear { get; set; }

    /// <summary>
    /// Make the rating read-only (disabled). For display purposes only.
    /// </summary>
    [Parameter]
    public bool IsReadOnly { get; set; }

    /// <summary>
    /// Builds the CSS class string for the rating container.
    /// </summary>
    private string ContainerCssClass
    {
        get
        {
            var classes = new List<string>();

            // Size
            classes.Add(Size switch
            {
                RatingSize.XSmall => "rating-xs",
                RatingSize.Small => "rating-sm",
                RatingSize.Medium => "rating-md",
                RatingSize.Large => "rating-lg",
                RatingSize.XLarge => "rating-xl",
                _ => ""
            });

            // Half modifier
            if (Half)
            {
                classes.Add("rating-half");
            }

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }

    /// <summary>
    /// Builds the CSS class string for each rating item.
    /// </summary>
    private string ItemCssClass
    {
        get
        {
            var classes = new List<string> { "mask" };

            // Shape
            classes.Add(Shape switch
            {
                RatingShape.Star => "mask-star",
                RatingShape.Star2 => "mask-star-2",
                RatingShape.Heart => "mask-heart",
                _ => ""
            });

            // Color
            if (!string.IsNullOrEmpty(Color))
            {
                classes.Add(Color);
            }

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }

    /// <summary>
    /// Handles rating value changes.
    /// </summary>
    private void HandleChange(int value)
    {
        CurrentValue = value;
    }

    /// <inheritdoc />
    protected override bool TryParseValueFromString(string? value, out int result, out string validationErrorMessage)
    {
        if (int.TryParse(value, out result))
        {
            validationErrorMessage = string.Empty;
            return true;
        }

        validationErrorMessage = $"The value '{value}' is not a valid number.";
        return false;
    }
}

/// <summary>
/// Shape variants for rating items (mask shapes).
/// </summary>
public enum RatingShape
{
    Star,
    Star2,
    Heart
}

/// <summary>
/// Size variants for rating components.
/// </summary>
public enum RatingSize
{
    XSmall,
    Small,
    Medium,
    Large,
    XLarge
}
