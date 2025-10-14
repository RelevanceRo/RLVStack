using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Avatar group component for displaying multiple overlapping avatars.
/// Typically used to show a list of users/participants.
/// </summary>
public partial class RlvAvatarGroup
{
    /// <summary>
    /// Child content - typically contains multiple RlvAvatar components.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Negative spacing between avatars for overlap effect.
    /// Default: "-space-x-6"
    /// </summary>
    [Parameter]
    public string SpacingClass { get; set; } = "-space-x-6";

    /// <summary>
    /// Additional CSS classes for the avatar group.
    /// </summary>
    [Parameter]
    public string? GroupClass { get; set; }

    /// <summary>
    /// Additional attributes for the avatar group element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }
}
