namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

public partial class RlvCalendar
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public CalendarLibrary Library { get; set; } = CalendarLibrary.Cally;

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            var libraryClass = Library switch
            {
                CalendarLibrary.Cally => "cally",
                CalendarLibrary.Pikaday => "pika-single",
                CalendarLibrary.ReactDayPicker => "react-day-picker",
                _ => ""
            };
            if (!string.IsNullOrEmpty(libraryClass))
                classes.Add(libraryClass);

            return string.Join(" ", classes);
        }
    }
}

public enum CalendarLibrary
{
    Cally,
    Pikaday,
    ReactDayPicker
}
