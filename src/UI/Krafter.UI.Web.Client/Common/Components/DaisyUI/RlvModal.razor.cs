using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Modal component using HTML dialog element with DaisyUI styling.
/// Provides a dialog/popup that overlays the main content.
/// Requires JS interop to open/close programmatically.
/// </summary>
public partial class RlvModal : ComponentBase, IAsyncDisposable
{
    /// <summary>
    /// Unique ID for the modal dialog element (required for JS interop).
    /// </summary>
    [Parameter, EditorRequired]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Controls whether the modal is open. Supports two-way binding.
    /// </summary>
    [Parameter]
    public bool IsOpen { get; set; }

    /// <summary>
    /// Callback invoked when IsOpen changes.
    /// </summary>
    [Parameter]
    public EventCallback<bool> IsOpenChanged { get; set; }

    /// <summary>
    /// Callback invoked when the modal is closed.
    /// </summary>
    [Parameter]
    public EventCallback OnClose { get; set; }

    /// <summary>
    /// Modal title text (simple string).
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// Modal title content (for rich content). Takes precedence over Title if both are set.
    /// </summary>
    [Parameter]
    public RenderFragment? TitleContent { get; set; }

    /// <summary>
    /// Main content of the modal body.
    /// </summary>
    [Parameter]
    public RenderFragment? Content { get; set; }

    /// <summary>
    /// Child content - provides full flexibility for custom modal structure.
    /// When ShowModalBox is false, this is the only content rendered inside the dialog.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Actions section content (buttons, links, etc.).
    /// </summary>
    [Parameter]
    public RenderFragment? Actions { get; set; }

    /// <summary>
    /// Whether to show the default modal-box wrapper.
    /// Set to false for complete custom content control.
    /// Default: true
    /// </summary>
    [Parameter]
    public bool ShowModalBox { get; set; } = true;

    /// <summary>
    /// Whether to show a close button (X) at the top-right corner.
    /// Default: false
    /// </summary>
    [Parameter]
    public bool ShowCloseButton { get; set; }

    /// <summary>
    /// Whether clicking outside the modal closes it (modal-backdrop).
    /// Default: true
    /// </summary>
    [Parameter]
    public bool CloseOnBackdropClick { get; set; } = true;

    /// <summary>
    /// Vertical placement of the modal.
    /// </summary>
    [Parameter]
    public ModalVerticalPlacement VerticalPlacement { get; set; } = ModalVerticalPlacement.Middle;

    /// <summary>
    /// Horizontal placement of the modal.
    /// </summary>
    [Parameter]
    public ModalHorizontalPlacement HorizontalPlacement { get; set; } = ModalHorizontalPlacement.None;

    /// <summary>
    /// Additional CSS classes for the modal-box element.
    /// Use this for custom widths (e.g., "w-11/12 max-w-5xl").
    /// </summary>
    [Parameter]
    public string? ModalBoxClass { get; set; }

    /// <summary>
    /// Additional attributes for the dialog element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private IJSObjectReference? _jsModule;
    private DotNetObjectReference<RlvModal>? _dotNetReference;
    private bool _previousIsOpen;

    /// <summary>
    /// Builds the CSS class string for the modal.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            // Vertical placement
            classes.Add(VerticalPlacement switch
            {
                ModalVerticalPlacement.Top => "modal-top",
                ModalVerticalPlacement.Middle => "modal-middle",
                ModalVerticalPlacement.Bottom => "modal-bottom",
                _ => ""
            });

            // Horizontal placement
            classes.Add(HorizontalPlacement switch
            {
                ModalHorizontalPlacement.Start => "modal-start",
                ModalHorizontalPlacement.End => "modal-end",
                _ => ""
            });

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }

    /// <inheritdoc />
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _dotNetReference = DotNetObjectReference.Create(this);
            _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import",
                "./Common/Components/DaisyUI/RlvModal.razor.js");

            // Initialize modal close listener
            await _jsModule.InvokeVoidAsync("initializeModal", Id, _dotNetReference);
        }

        // Handle open/close based on IsOpen parameter
        if (IsOpen != _previousIsOpen)
        {
            _previousIsOpen = IsOpen;

            if (IsOpen)
            {
                await OpenAsync();
            }
            else
            {
                await CloseAsync();
            }
        }
    }

    /// <summary>
    /// Opens the modal programmatically.
    /// </summary>
    public async Task OpenAsync()
    {
        if (_jsModule != null)
        {
            await _jsModule.InvokeVoidAsync("openModal", Id);

            if (!IsOpen)
            {
                IsOpen = true;
                await IsOpenChanged.InvokeAsync(true);
            }
        }
    }

    /// <summary>
    /// Closes the modal programmatically.
    /// </summary>
    public async Task CloseAsync()
    {
        if (_jsModule != null)
        {
            await _jsModule.InvokeVoidAsync("closeModal", Id);

            if (IsOpen)
            {
                IsOpen = false;
                await IsOpenChanged.InvokeAsync(false);
                await OnClose.InvokeAsync();
            }
        }
    }

    /// <summary>
    /// Called from JavaScript when the modal is closed (ESC key, backdrop click, etc.).
    /// </summary>
    [JSInvokable]
    public async Task HandleModalClose()
    {
        if (IsOpen)
        {
            IsOpen = false;
            await IsOpenChanged.InvokeAsync(false);
            await OnClose.InvokeAsync();
            StateHasChanged();
        }
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        if (_jsModule != null)
        {
            try
            {
                await _jsModule.InvokeVoidAsync("disposeModal", Id);
                await _jsModule.DisposeAsync();
            }
            catch
            {
                // Ignore disposal errors
            }
        }

        _dotNetReference?.Dispose();
    }
}

/// <summary>
/// Vertical placement options for modals.
/// </summary>
public enum ModalVerticalPlacement
{
    Top,
    Middle,
    Bottom
}

/// <summary>
/// Horizontal placement options for modals.
/// </summary>
public enum ModalHorizontalPlacement
{
    None,
    Start,
    End
}
