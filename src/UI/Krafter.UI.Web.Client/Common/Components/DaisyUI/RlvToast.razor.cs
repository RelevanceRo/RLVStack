namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvToast
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public ToastHorizontalPosition HorizontalPosition { get; set; } = ToastHorizontalPosition.End;
    [Parameter] public ToastVerticalPosition VerticalPosition { get; set; } = ToastVerticalPosition.Bottom;

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string CssClass
    {
        get
        {
            var classes = new List<string> { "toast" };

            var horizontalClass = HorizontalPosition switch
            {
                ToastHorizontalPosition.Start => "toast-start",
                ToastHorizontalPosition.Center => "toast-center",
                ToastHorizontalPosition.End => "toast-end",
                _ => ""
            };
            if (!string.IsNullOrEmpty(horizontalClass))
                classes.Add(horizontalClass);

            var verticalClass = VerticalPosition switch
            {
                ToastVerticalPosition.Top => "toast-top",
                ToastVerticalPosition.Middle => "toast-middle",
                ToastVerticalPosition.Bottom => "toast-bottom",
                _ => ""
            };
            if (!string.IsNullOrEmpty(verticalClass))
                classes.Add(verticalClass);

            return string.Join(" ", classes);
        }
    }
}

public enum ToastHorizontalPosition
{
    Start,
    Center,
    End
}

public enum ToastVerticalPosition
{
    Top,
    Middle,
    Bottom
}
