using Microsoft.AspNetCore.Components.Forms;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvCheckbox : InputBase<bool>
{
    [Parameter] public string? Label { get; set; }
    [Parameter] public CheckboxColor Color { get; set; } = CheckboxColor.None;
    [Parameter] public CheckboxSize Size { get; set; } = CheckboxSize.Medium;
    [Parameter] public bool Indeterminate { get; set; }

    private string CssClass
    {
        get
        {
            var classes = new List<string> { "checkbox" };

            if (Color != CheckboxColor.None)
            {
                var colorClass = Color switch
                {
                    CheckboxColor.Primary => "checkbox-primary",
                    CheckboxColor.Secondary => "checkbox-secondary",
                    CheckboxColor.Accent => "checkbox-accent",
                    CheckboxColor.Neutral => "checkbox-neutral",
                    CheckboxColor.Info => "checkbox-info",
                    CheckboxColor.Success => "checkbox-success",
                    CheckboxColor.Warning => "checkbox-warning",
                    CheckboxColor.Error => "checkbox-error",
                    _ => ""
                };
                if (!string.IsNullOrEmpty(colorClass))
                    classes.Add(colorClass);
            }

            if (Size != CheckboxSize.Medium)
            {
                var sizeClass = Size switch
                {
                    CheckboxSize.XSmall => "checkbox-xs",
                    CheckboxSize.Small => "checkbox-sm",
                    CheckboxSize.Large => "checkbox-lg",
                    CheckboxSize.XLarge => "checkbox-xl",
                    _ => ""
                };
                if (!string.IsNullOrEmpty(sizeClass))
                    classes.Add(sizeClass);
            }

            return string.Join(" ", classes);
        }
    }

    private void HandleChange(ChangeEventArgs e)
    {
        CurrentValue = (bool)(e.Value ?? false);
    }

    protected override bool TryParseValueFromString(string? value, out bool result, out string? validationErrorMessage)
    {
        if (bool.TryParse(value, out var parsed))
        {
            result = parsed;
            validationErrorMessage = null;
            return true;
        }

        result = default;
        validationErrorMessage = $"The {DisplayName ?? FieldIdentifier.FieldName} field must be a valid boolean.";
        return false;
    }
}

public enum CheckboxColor
{
    None,
    Primary,
    Secondary,
    Accent,
    Neutral,
    Info,
    Success,
    Warning,
    Error
}

public enum CheckboxSize
{
    XSmall,
    Small,
    Medium,
    Large,
    XLarge
}
