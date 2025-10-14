using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Avatar component for displaying user profile pictures or placeholder initials.
/// Supports presence indicators (online/offline), custom shapes, and grouping.
/// </summary>
public partial class RlvAvatar
{
    /// <summary>
    /// Image source URL for the avatar.
    /// </summary>
    [Parameter]
    public string? Src { get; set; }

    /// <summary>
    /// Alt text for the avatar image.
    /// </summary>
    [Parameter]
    public string? Alt { get; set; }

    /// <summary>
    /// Whether this is a placeholder avatar (shows initials/text instead of image).
    /// Default: false
    /// </summary>
    [Parameter]
    public bool Placeholder { get; set; }

    /// <summary>
    /// Text to display in placeholder mode (typically initials).
    /// </summary>
    [Parameter]
    public string? PlaceholderText { get; set; }

    /// <summary>
    /// Presence indicator for the avatar.
    /// </summary>
    [Parameter]
    public AvatarPresence Presence { get; set; } = AvatarPresence.None;

    /// <summary>
    /// Child content - for custom avatar content or when not using Src parameter.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional CSS classes for the avatar container.
    /// </summary>
    [Parameter]
    public string? AvatarClass { get; set; }

    /// <summary>
    /// Additional CSS classes for the inner div (size, shape, background, etc.).
    /// Typically includes width (w-24), shape (rounded-full), and background classes.
    /// </summary>
    [Parameter]
    public string? InnerClass { get; set; }

    /// <summary>
    /// Additional CSS classes for the placeholder text (font size, etc.).
    /// </summary>
    [Parameter]
    public string? PlaceholderTextClass { get; set; }

    /// <summary>
    /// Additional attributes for the avatar element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Builds the CSS class string for the avatar.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            // Presence indicator
            classes.Add(Presence switch
            {
                AvatarPresence.Online => "avatar-online",
                AvatarPresence.Offline => "avatar-offline",
                _ => ""
            });

            // Placeholder mode
            if (Placeholder)
            {
                classes.Add("avatar-placeholder");
            }

            // Custom classes
            if (!string.IsNullOrEmpty(AvatarClass))
            {
                classes.Add(AvatarClass);
            }

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }
}

/// <summary>
/// Presence indicator options for avatars.
/// </summary>
public enum AvatarPresence
{
    None,
    Online,
    Offline
}
