using Microsoft.AspNetCore.Components.Forms;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvInputField : InputBase<string>
{
    [Parameter] public string? Placeholder { get; set; }
    [Parameter] public string Type { get; set; } = "text";
    [Parameter] public InputFieldColor Color { get; set; } = InputFieldColor.None;
    [Parameter] public InputFieldSize Size { get; set; } = InputFieldSize.Medium;
    [Parameter] public bool Ghost { get; set; }

    private string CssClass
    {
        get
        {
            var classes = new List<string> { "input" };

            if (Ghost)
            {
                classes.Add("input-ghost");
            }

            if (Color != InputFieldColor.None)
            {
                var colorClass = Color switch
                {
                    InputFieldColor.Neutral => "input-neutral",
                    InputFieldColor.Primary => "input-primary",
                    InputFieldColor.Secondary => "input-secondary",
                    InputFieldColor.Accent => "input-accent",
                    InputFieldColor.Info => "input-info",
                    InputFieldColor.Success => "input-success",
                    InputFieldColor.Warning => "input-warning",
                    InputFieldColor.Error => "input-error",
                    _ => ""
                };
                if (!string.IsNullOrEmpty(colorClass))
                    classes.Add(colorClass);
            }

            if (Size != InputFieldSize.Medium)
            {
                var sizeClass = Size switch
                {
                    InputFieldSize.XSmall => "input-xs",
                    InputFieldSize.Small => "input-sm",
                    InputFieldSize.Large => "input-lg",
                    InputFieldSize.XLarge => "input-xl",
                    _ => ""
                };
                if (!string.IsNullOrEmpty(sizeClass))
                    classes.Add(sizeClass);
            }

            return string.Join(" ", classes);
        }
    }

    private void HandleInput(ChangeEventArgs e)
    {
        CurrentValueAsString = e.Value?.ToString();
    }

    protected override bool TryParseValueFromString(string? value, out string result, out string? validationErrorMessage)
    {
        result = value ?? string.Empty;
        validationErrorMessage = null;
        return true;
    }
}

public enum InputFieldColor
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

public enum InputFieldSize
{
    XSmall,
    Small,
    Medium,
    Large,
    XLarge
}
