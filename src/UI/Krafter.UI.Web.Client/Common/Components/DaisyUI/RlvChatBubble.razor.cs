using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// DaisyUI Chat Bubble component - displays one line of conversation with all its data
/// </summary>
public partial class RlvChatBubble
{
    /// <summary>
    /// Placement of the chat bubble (Start = left, End = right) - REQUIRED
    /// </summary>
    [Parameter] public ChatPlacement Placement { get; set; } = ChatPlacement.Start;

    /// <summary>
    /// Color of the chat bubble
    /// </summary>
    [Parameter] public DaisyColor? BubbleColor { get; set; }

    /// <summary>
    /// Simple text message to display in the bubble (alternative to BubbleContent)
    /// </summary>
    [Parameter] public string? Message { get; set; }

    /// <summary>
    /// Content for the chat image (avatar)
    /// </summary>
    [Parameter] public RenderFragment? ImageContent { get; set; }

    /// <summary>
    /// Content for the chat header (author name, time, etc.)
    /// </summary>
    [Parameter] public RenderFragment? HeaderContent { get; set; }

    /// <summary>
    /// Content for the chat bubble (the message)
    /// </summary>
    [Parameter] public RenderFragment? BubbleContent { get; set; }

    /// <summary>
    /// Content for the chat footer (delivery status, etc.)
    /// </summary>
    [Parameter] public RenderFragment? FooterContent { get; set; }

    /// <summary>
    /// Additional attributes to apply to the chat container
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string GetPlacementClass() => Placement switch
    {
        ChatPlacement.Start => "chat-start",
        ChatPlacement.End => "chat-end",
        _ => "chat-start"
    };

    private string GetBubbleColorClass() => BubbleColor switch
    {
        DaisyColor.Neutral => "chat-bubble-neutral",
        DaisyColor.Primary => "chat-bubble-primary",
        DaisyColor.Secondary => "chat-bubble-secondary",
        DaisyColor.Accent => "chat-bubble-accent",
        DaisyColor.Info => "chat-bubble-info",
        DaisyColor.Success => "chat-bubble-success",
        DaisyColor.Warning => "chat-bubble-warning",
        DaisyColor.Error => "chat-bubble-error",
        _ => ""
    };
}

/// <summary>
/// Chat bubble placement options
/// </summary>
public enum ChatPlacement
{
    /// <summary>Aligns chat to start (left)</summary>
    Start,
    /// <summary>Aligns chat to end (right)</summary>
    End
}
