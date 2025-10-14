namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvProgress
{
    [Parameter] public int? Value { get; set; }
    [Parameter] public int Max { get; set; } = 100;
    [Parameter] public ProgressColor Color { get; set; } = ProgressColor.None;

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string CssClass
    {
        get
        {
            var classes = new List<string> { "progress" };

            if (Color != ProgressColor.None)
            {
                var colorClass = Color switch
                {
                    ProgressColor.Neutral => "progress-neutral",
                    ProgressColor.Primary => "progress-primary",
                    ProgressColor.Secondary => "progress-secondary",
                    ProgressColor.Accent => "progress-accent",
                    ProgressColor.Info => "progress-info",
                    ProgressColor.Success => "progress-success",
                    ProgressColor.Warning => "progress-warning",
                    ProgressColor.Error => "progress-error",
                    _ => ""
                };
                if (!string.IsNullOrEmpty(colorClass))
                    classes.Add(colorClass);
            }

            return string.Join(" ", classes);
        }
    }
}

public enum ProgressColor
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
