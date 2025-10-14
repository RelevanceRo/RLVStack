using Microsoft.AspNetCore.Components.Web;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvButton
{
    [Parameter] public string? Text { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

    [Parameter] public ButtonColor Color { get; set; } = ButtonColor.None;
    [Parameter] public ButtonStyle Style { get; set; } = ButtonStyle.None;
    [Parameter] public ButtonSize Size { get; set; } = ButtonSize.Medium;

    [Parameter] public bool Wide { get; set; }
    [Parameter] public bool Block { get; set; }
    [Parameter] public bool Square { get; set; }
    [Parameter] public bool Circle { get; set; }

    [Parameter] public bool Active { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool Loading { get; set; }

    [Parameter] public string? Type { get; set; } = "button";
    [Parameter] public string? Href { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string CssClass
    {
        get
        {
            var classes = new List<string> { "btn" };

            if (Color != ButtonColor.None)
            {
                var colorClass = Color switch
                {
                    ButtonColor.Neutral => "btn-neutral",
                    ButtonColor.Primary => "btn-primary",
                    ButtonColor.Secondary => "btn-secondary",
                    ButtonColor.Accent => "btn-accent",
                    ButtonColor.Info => "btn-info",
                    ButtonColor.Success => "btn-success",
                    ButtonColor.Warning => "btn-warning",
                    ButtonColor.Error => "btn-error",
                    _ => ""
                };
                if (!string.IsNullOrEmpty(colorClass))
                    classes.Add(colorClass);
            }

            if (Style != ButtonStyle.None)
            {
                var styleClass = Style switch
                {
                    ButtonStyle.Outline => "btn-outline",
                    ButtonStyle.Dash => "btn-dash",
                    ButtonStyle.Soft => "btn-soft",
                    ButtonStyle.Ghost => "btn-ghost",
                    ButtonStyle.Link => "btn-link",
                    _ => ""
                };
                if (!string.IsNullOrEmpty(styleClass))
                    classes.Add(styleClass);
            }

            if (Size != ButtonSize.Medium)
            {
                var sizeClass = Size switch
                {
                    ButtonSize.XSmall => "btn-xs",
                    ButtonSize.Small => "btn-sm",
                    ButtonSize.Large => "btn-lg",
                    ButtonSize.XLarge => "btn-xl",
                    _ => ""
                };
                if (!string.IsNullOrEmpty(sizeClass))
                    classes.Add(sizeClass);
            }

            if (Wide) classes.Add("btn-wide");
            if (Block) classes.Add("btn-block");
            if (Square) classes.Add("btn-square");
            if (Circle) classes.Add("btn-circle");

            if (Active) classes.Add("btn-active");
            if (Disabled) classes.Add("btn-disabled");

            return string.Join(" ", classes);
        }
    }

    private async Task HandleClick(MouseEventArgs e)
    {
        if (!Disabled && OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(e);
        }
    }
}

public enum ButtonColor
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

public enum ButtonStyle
{
    None,
    Outline,
    Dash,
    Soft,
    Ghost,
    Link
}

public enum ButtonSize
{
    XSmall,
    Small,
    Medium,
    Large,
    XLarge
}
