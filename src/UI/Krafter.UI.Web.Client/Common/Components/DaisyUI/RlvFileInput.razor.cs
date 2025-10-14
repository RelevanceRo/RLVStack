namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvFileInput
{
    [Parameter] public EventCallback<ChangeEventArgs> OnChange { get; set; }
    [Parameter] public FileInputColor Color { get; set; } = FileInputColor.None;
    [Parameter] public FileInputSize Size { get; set; } = FileInputSize.Medium;
    [Parameter] public bool Ghost { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool Multiple { get; set; }
    [Parameter] public string? Accept { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string CssClass
    {
        get
        {
            var classes = new List<string> { "file-input" };

            if (Ghost)
            {
                classes.Add("file-input-ghost");
            }

            if (Color != FileInputColor.None)
            {
                var colorClass = Color switch
                {
                    FileInputColor.Neutral => "file-input-neutral",
                    FileInputColor.Primary => "file-input-primary",
                    FileInputColor.Secondary => "file-input-secondary",
                    FileInputColor.Accent => "file-input-accent",
                    FileInputColor.Info => "file-input-info",
                    FileInputColor.Success => "file-input-success",
                    FileInputColor.Warning => "file-input-warning",
                    FileInputColor.Error => "file-input-error",
                    _ => ""
                };
                if (!string.IsNullOrEmpty(colorClass))
                    classes.Add(colorClass);
            }

            if (Size != FileInputSize.Medium)
            {
                var sizeClass = Size switch
                {
                    FileInputSize.XSmall => "file-input-xs",
                    FileInputSize.Small => "file-input-sm",
                    FileInputSize.Large => "file-input-lg",
                    FileInputSize.XLarge => "file-input-xl",
                    _ => ""
                };
                if (!string.IsNullOrEmpty(sizeClass))
                    classes.Add(sizeClass);
            }

            return string.Join(" ", classes);
        }
    }

    private async Task HandleChange(ChangeEventArgs e)
    {
        if (OnChange.HasDelegate)
        {
            // For advanced file handling, consumers should use Blazor's InputFile component directly
            // This component is for basic file input with DaisyUI styling
            await OnChange.InvokeAsync(e);
        }
    }
}

public enum FileInputColor
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

public enum FileInputSize
{
    XSmall,
    Small,
    Medium,
    Large,
    XLarge
}
