namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvTooltip
{
    [Parameter] public string? Tip { get; set; }
    [Parameter] public RenderFragment? TipContent { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public TooltipPlacement Placement { get; set; } = TooltipPlacement.Top;
    [Parameter] public TooltipColor Color { get; set; } = TooltipColor.None;
    [Parameter] public bool Open { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string CssClass
    {
        get
        {
            var classes = new List<string> { "tooltip" };

            var placementClass = Placement switch
            {
                TooltipPlacement.Top => "tooltip-top",
                TooltipPlacement.Bottom => "tooltip-bottom",
                TooltipPlacement.Left => "tooltip-left",
                TooltipPlacement.Right => "tooltip-right",
                _ => ""
            };
            if (!string.IsNullOrEmpty(placementClass))
                classes.Add(placementClass);

            if (Color != TooltipColor.None)
            {
                var colorClass = Color switch
                {
                    TooltipColor.Neutral => "tooltip-neutral",
                    TooltipColor.Primary => "tooltip-primary",
                    TooltipColor.Secondary => "tooltip-secondary",
                    TooltipColor.Accent => "tooltip-accent",
                    TooltipColor.Info => "tooltip-info",
                    TooltipColor.Success => "tooltip-success",
                    TooltipColor.Warning => "tooltip-warning",
                    TooltipColor.Error => "tooltip-error",
                    _ => ""
                };
                if (!string.IsNullOrEmpty(colorClass))
                    classes.Add(colorClass);
            }

            if (Open)
            {
                classes.Add("tooltip-open");
            }

            return string.Join(" ", classes);
        }
    }
}

public enum TooltipPlacement
{
    Top,
    Bottom,
    Left,
    Right
}

public enum TooltipColor
{
    None,
    Neutral,
    Primary,
    Secondary,
    Accent,
    Info,
    Success,
    Warning,
    Error
}
