namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvAlert
{
    [Parameter] public string? Message { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RenderFragment? Icon { get; set; }
    [Parameter] public RenderFragment? Actions { get; set; }

    [Parameter] public AlertColor Color { get; set; } = AlertColor.None;
    [Parameter] public AlertStyle Style { get; set; } = AlertStyle.None;
    [Parameter] public AlertDirection Direction { get; set; } = AlertDirection.None;

    [Parameter] public bool Dismissible { get; set; }
    [Parameter] public EventCallback OnDismiss { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private bool _isVisible = true;

    private string CssClass
    {
        get
        {
            var classes = new List<string> { "alert" };

            if (Color != AlertColor.None)
            {
                var colorClass = Color switch
                {
                    AlertColor.Info => "alert-info",
                    AlertColor.Success => "alert-success",
                    AlertColor.Warning => "alert-warning",
                    AlertColor.Error => "alert-error",
                    _ => ""
                };
                if (!string.IsNullOrEmpty(colorClass))
                    classes.Add(colorClass);
            }

            if (Style != AlertStyle.None)
            {
                var styleClass = Style switch
                {
                    AlertStyle.Outline => "alert-outline",
                    AlertStyle.Dash => "alert-dash",
                    AlertStyle.Soft => "alert-soft",
                    _ => ""
                };
                if (!string.IsNullOrEmpty(styleClass))
                    classes.Add(styleClass);
            }

            if (Direction != AlertDirection.None)
            {
                var directionClass = Direction switch
                {
                    AlertDirection.Vertical => "alert-vertical",
                    AlertDirection.Horizontal => "alert-horizontal",
                    _ => ""
                };
                if (!string.IsNullOrEmpty(directionClass))
                    classes.Add(directionClass);
            }

            return string.Join(" ", classes);
        }
    }

    private RenderFragment DefaultInfoIcon => builder =>
    {
        builder.OpenElement(0, "svg");
        builder.AddAttribute(1, "xmlns", "http://www.w3.org/2000/svg");
        builder.AddAttribute(2, "fill", "none");
        builder.AddAttribute(3, "viewBox", "0 0 24 24");
        builder.AddAttribute(4, "class", "h-6 w-6 shrink-0 stroke-current");
        builder.OpenElement(5, "path");
        builder.AddAttribute(6, "stroke-linecap", "round");
        builder.AddAttribute(7, "stroke-linejoin", "round");
        builder.AddAttribute(8, "stroke-width", "2");
        builder.AddAttribute(9, "d", "M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z");
        builder.CloseElement();
        builder.CloseElement();
    };

    private RenderFragment DefaultSuccessIcon => builder =>
    {
        builder.OpenElement(0, "svg");
        builder.AddAttribute(1, "xmlns", "http://www.w3.org/2000/svg");
        builder.AddAttribute(2, "fill", "none");
        builder.AddAttribute(3, "viewBox", "0 0 24 24");
        builder.AddAttribute(4, "class", "h-6 w-6 shrink-0 stroke-current");
        builder.OpenElement(5, "path");
        builder.AddAttribute(6, "stroke-linecap", "round");
        builder.AddAttribute(7, "stroke-linejoin", "round");
        builder.AddAttribute(8, "stroke-width", "2");
        builder.AddAttribute(9, "d", "M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z");
        builder.CloseElement();
        builder.CloseElement();
    };

    private RenderFragment DefaultWarningIcon => builder =>
    {
        builder.OpenElement(0, "svg");
        builder.AddAttribute(1, "xmlns", "http://www.w3.org/2000/svg");
        builder.AddAttribute(2, "fill", "none");
        builder.AddAttribute(3, "viewBox", "0 0 24 24");
        builder.AddAttribute(4, "class", "h-6 w-6 shrink-0 stroke-current");
        builder.OpenElement(5, "path");
        builder.AddAttribute(6, "stroke-linecap", "round");
        builder.AddAttribute(7, "stroke-linejoin", "round");
        builder.AddAttribute(8, "stroke-width", "2");
        builder.AddAttribute(9, "d", "M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z");
        builder.CloseElement();
        builder.CloseElement();
    };

    private RenderFragment DefaultErrorIcon => builder =>
    {
        builder.OpenElement(0, "svg");
        builder.AddAttribute(1, "xmlns", "http://www.w3.org/2000/svg");
        builder.AddAttribute(2, "fill", "none");
        builder.AddAttribute(3, "viewBox", "0 0 24 24");
        builder.AddAttribute(4, "class", "h-6 w-6 shrink-0 stroke-current");
        builder.OpenElement(5, "path");
        builder.AddAttribute(6, "stroke-linecap", "round");
        builder.AddAttribute(7, "stroke-linejoin", "round");
        builder.AddAttribute(8, "stroke-width", "2");
        builder.AddAttribute(9, "d", "M10 14l2-2m0 0l2-2m-2 2l-2-2m2 2l2 2m7-2a9 9 0 11-18 0 9 9 0 0118 0z");
        builder.CloseElement();
        builder.CloseElement();
    };

    private RenderFragment? GetDefaultIcon()
    {
        return Color switch
        {
            AlertColor.Info => DefaultInfoIcon,
            AlertColor.Success => DefaultSuccessIcon,
            AlertColor.Warning => DefaultWarningIcon,
            AlertColor.Error => DefaultErrorIcon,
            _ => null
        };
    }

    private async Task HandleDismiss()
    {
        _isVisible = false;
        if (OnDismiss.HasDelegate)
        {
            await OnDismiss.InvokeAsync();
        }
    }
}

public enum AlertColor
{
    None,
    Info,
    Success,
    Warning,
    Error
}

public enum AlertStyle
{
    None,
    Outline,
    Dash,
    Soft
}

public enum AlertDirection
{
    None,
    Vertical,
    Horizontal
}
