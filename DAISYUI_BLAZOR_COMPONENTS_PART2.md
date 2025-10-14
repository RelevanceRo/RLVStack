# DaisyUI Blazor Component Wrappers - Part 2
## Detailed Implementation Examples

**Main Document:** [DAISYUI_BLAZOR_COMPONENTS.md](./DAISYUI_BLAZOR_COMPONENTS.md)
**Project:** Krafter UI - RLVStack
**Component Prefix:** `Rlv`
**Location:** `src/UI/Krafter.UI.Web.Client/Common/Components/DaisyUI/`

---

This document contains detailed implementation examples and usage patterns for all completed DaisyUI Blazor wrapper components. For component status, progress tracking, and implementation guidelines, see the main document.

---

## Table of Contents
1. [RlvButton](#rlvbutton-completed-2025-10-14)
2. [RlvAlert](#rlvalert-completed-2025-10-14)
3. [RlvLoading](#rlvloading-completed-2025-10-14)
4. [RlvProgress](#rlvprogress-completed-2025-10-14)
5. [RlvRadialProgress](#rlvradialprogress-completed-2025-10-14)
6. [RlvSkeleton](#rlvskeleton-completed-2025-10-14)
7. [RlvToast](#rlvtoast-completed-2025-10-14)
8. [RlvTooltip](#rlvtooltip-completed-2025-10-14)
9. [RlvCalendar](#rlvcalendar-completed-2025-10-14)
10. [RlvCheckbox](#rlvcheckbox-completed-2025-10-14)
11. [RlvFieldset](#rlvfieldset-completed-2025-10-14)
12. [RlvFileInput](#rlvfileinput-completed-2025-10-14)
13. [RlvFilter](#rlvfilter-completed-2025-10-14)
14. [RlvInputField](#rlvinputfield-completed-2025-10-14)
15. [RlvLabel](#rlvlabel-completed-2025-10-14)
16. [RlvRadio](#rlvradio-completed-2025-10-14)
17. [RlvRange](#rlvrange-completed-2025-10-14)
18. [RlvRating](#rlvrating-completed-2025-10-14)
19. [RlvSelect](#rlvselect-completed-2025-10-14)
20. [RlvTextArea](#rlvtextarea-completed-2025-10-14)
21. [RlvToggle](#rlvtoggle-completed-2025-10-14)
22. [RlvValidator](#rlvvalidator-completed-2025-10-14)
23. [RlvDivider](#rlvdivider-completed-2025-10-14)
24. [RlvDropdown](#rlvdropdown-completed-2025-10-14)

---

### Implementation Details

#### RlvButton (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvButton.razor`
- `Common/Components/DaisyUI/RlvButton.razor.cs`

**Features Implemented:**
- **8 Color variants:** None, Neutral, Primary, Secondary, Accent, Info, Success, Warning, Error
- **5 Style variants:** None, Outline, Dash, Soft, Ghost, Link
- **5 Size variants:** XSmall (xs), Small (sm), Medium (md - default), Large (lg), XLarge (xl)
- **4 Modifiers:** Wide, Block, Square, Circle
- **States:** Active, Disabled, Loading (with spinner)
- **Rendering modes:** Button element (default) or Anchor element (when Href provided)
- **Content support:** Text parameter or ChildContent for icons/complex content
- **Event handling:** OnClick with disabled state check
- **Enums:** ButtonColor, ButtonStyle, ButtonSize for type-safe configuration
- **Accessibility:** Proper ARIA attributes, disabled state, role attributes

**Usage Examples:**
```razor
<!-- Basic button -->
<RlvButton Text="Click me" OnClick="HandleClick" />

<!-- Primary button with icon -->
<RlvButton Color="ButtonColor.Primary" Size="ButtonSize.Large">
    <svg>...</svg> Save
</RlvButton>

<!-- Loading button -->
<RlvButton Loading="true" Text="Processing..." />

<!-- Link button -->
<RlvButton Href="/dashboard" Color="ButtonColor.Info" Text="Go to Dashboard" />
```

#### RlvAlert (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvAlert.razor`
- `Common/Components/DaisyUI/RlvAlert.razor.cs`

**Features Implemented:**
- **4 Color variants:** Info, Success, Warning, Error
- **3 Style variants:** Outline, Dash, Soft
- **2 Direction variants:** Vertical, Horizontal (for responsive layouts)
- **Default icons:** Automatic icon rendering based on color (info, success, warning, error)
- **Custom icon support:** Icon RenderFragment parameter
- **Content modes:** Simple message string or complex ChildContent
- **Actions support:** Actions RenderFragment for buttons/links
- **Dismissible:** Optional close button with OnDismiss callback
- **Visibility control:** Internal state management for dismiss functionality
- **Enums:** AlertColor, AlertStyle, AlertDirection for type-safe configuration
- **Accessibility:** role="alert" attribute, aria-label for close button

**Usage Examples:**
```razor
<!-- Simple info alert -->
<RlvAlert Color="AlertColor.Info" Message="New software update available." />

<!-- Success alert with default icon -->
<RlvAlert Color="AlertColor.Success" Message="Your purchase has been confirmed!" />

<!-- Dismissible error alert -->
<RlvAlert Color="AlertColor.Error" Message="Error! Task failed." Dismissible="true" OnDismiss="HandleDismiss" />

<!-- Alert with actions -->
<RlvAlert Color="AlertColor.Warning" Direction="AlertDirection.Horizontal">
    <span>We use cookies for no reason.</span>
    <Actions>
        <div>
            <RlvButton Size="ButtonSize.Small" Text="Deny" />
            <RlvButton Size="ButtonSize.Small" Color="ButtonColor.Primary" Text="Accept" />
        </div>
    </Actions>
</RlvAlert>

<!-- Alert with title and description -->
<RlvAlert Color="AlertColor.Info" Style="AlertStyle.Soft">
    <div>
        <h3 class="font-bold">New message!</h3>
        <div class="text-xs">You have 1 unread message</div>
    </div>
    <Actions>
        <RlvButton Size="ButtonSize.Small" Text="See" />
    </Actions>
</RlvAlert>
```

#### RlvLoading (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvLoading.razor`
- `Common/Components/DaisyUI/RlvLoading.razor.cs`

**Features Implemented:**
- **6 Animation types:** Spinner, Dots, Ring, Ball, Bars, Infinity
- **5 Size variants:** XSmall (xs), Small (sm), Medium (md - default), Large (lg), XLarge (xl)
- **Color support:** Optional color parameter applies Tailwind text color classes (text-primary, text-secondary, etc.)
- **Simple span element:** Renders as `<span>` with composed CSS classes
- **Enums:** LoadingAnimation, LoadingSize for type-safe configuration
- **Minimal markup:** Lightweight component for loading indicators

**Usage Examples:**
```razor
<!-- Default spinner (medium size) -->
<RlvLoading />

<!-- Large dots animation -->
<RlvLoading Animation="LoadingAnimation.Dots" Size="LoadingSize.Large" />

<!-- Spinner with primary color -->
<RlvLoading Animation="LoadingAnimation.Spinner" Color="primary" />

<!-- Ring animation, extra large, success color -->
<RlvLoading Animation="LoadingAnimation.Ring" Size="LoadingSize.XLarge" Color="success" />

<!-- Infinity animation, small size -->
<RlvLoading Animation="LoadingAnimation.Infinity" Size="LoadingSize.Small" />

<!-- Bars animation with custom color -->
<RlvLoading Animation="LoadingAnimation.Bars" Color="accent" />
```

#### RlvProgress (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvProgress.razor`
- `Common/Components/DaisyUI/RlvProgress.razor.cs`

**Features Implemented:**
- **8 Color variants:** Neutral, Primary, Secondary, Accent, Info, Success, Warning, Error
- **Value/Max support:** Standard HTML progress element with value and max attributes
- **Indeterminate mode:** When Value is null, shows indeterminate/loading animation
- **Native element:** Uses HTML `<progress>` element for accessibility
- **Enum:** ProgressColor for type-safe color configuration

**Usage Examples:**
```razor
<!-- Default progress at 50% -->
<RlvProgress Value="50" Max="100" />

<!-- Primary color progress at 70% -->
<RlvProgress Value="70" Color="ProgressColor.Primary" />

<!-- Success color progress at 100% -->
<RlvProgress Value="100" Color="ProgressColor.Success" />

<!-- Indeterminate progress (loading) -->
<RlvProgress Color="ProgressColor.Info" />

<!-- Custom max value -->
<RlvProgress Value="3" Max="10" Color="ProgressColor.Accent" />
```

#### RlvRadialProgress (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvRadialProgress.razor`
- `Common/Components/DaisyUI/RlvRadialProgress.razor.cs`

**Features Implemented:**
- **Value parameter:** 0-100 value for progress percentage
- **Color support:** Optional color parameter applies Tailwind text color classes (text-primary, etc.)
- **Custom size:** Optional Size parameter (CSS value like "12rem", default "5rem")
- **Custom thickness:** Optional Thickness parameter (CSS value like "2px" or "2rem", default 10% of size)
- **Content support:** ChildContent or default percentage display
- **CSS variables:** Uses --value, --size, --thickness for styling
- **Accessibility:** role="progressbar" and aria-valuenow attributes
- **Div element:** Uses `<div>` instead of `<progress>` for text content support

**Usage Examples:**
```razor
<!-- Default radial progress at 70% -->
<RlvRadialProgress Value="70" />

<!-- Primary color radial progress -->
<RlvRadialProgress Value="85" Color="primary" />

<!-- Custom size (12rem) with thin border -->
<RlvRadialProgress Value="60" Size="12rem" Thickness="2px" />

<!-- Custom size with thick border -->
<RlvRadialProgress Value="45" Size="12rem" Thickness="2rem" Color="success" />

<!-- With custom content -->
<RlvRadialProgress Value="90">
    <strong>90</strong>
</RlvRadialProgress>

<!-- With background and border (via AdditionalAttributes) -->
<RlvRadialProgress Value="75"
                   Color="primary"
                   class="bg-primary text-primary-content border-4 border-primary" />
```

#### RlvSkeleton (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvSkeleton.razor`
- `Common/Components/DaisyUI/RlvSkeleton.razor.cs`

**Features Implemented:**
- **Simple placeholder div:** Basic skeleton loading animation
- **Width/Height parameters:** Accept Tailwind utility classes (e.g., "w-32", "h-16")
- **Circle parameter:** Convenience boolean for rounded-full circles
- **Flexible styling:** Uses Tailwind utility classes for full customization
- **ChildContent support:** Optional content inside skeleton
- **Minimal design:** Leverages Tailwind for all sizing and shaping

**Usage Examples:**
```razor
<!-- Basic square skeleton -->
<RlvSkeleton Width="w-32" Height="h-32" />

<!-- Circle skeleton (avatar placeholder) -->
<RlvSkeleton Width="w-16" Height="h-16" Circle="true" />

<!-- Rectangle skeleton -->
<RlvSkeleton Width="w-full" Height="h-32" />

<!-- Text line skeletons -->
<RlvSkeleton Width="w-20" Height="h-4" />
<RlvSkeleton Width="w-28" Height="h-4" />

<!-- Using AdditionalAttributes for custom classes -->
<RlvSkeleton class="w-52 h-4" />

<!-- Complex skeleton layout -->
<div class="flex flex-col gap-4 w-52">
    <div class="flex gap-4 items-center">
        <RlvSkeleton Width="w-16" Height="h-16" Circle="true" class="shrink-0" />
        <div class="flex flex-col gap-4">
            <RlvSkeleton Width="w-20" Height="h-4" />
            <RlvSkeleton Width="w-28" Height="h-4" />
        </div>
    </div>
    <RlvSkeleton Width="w-full" Height="h-32" />
</div>
```

#### RlvToast (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvToast.razor`
- `Common/Components/DaisyUI/RlvToast.razor.cs`

**Features Implemented:**
- **Horizontal positioning:** Start, Center, End (default)
- **Vertical positioning:** Top, Middle, Bottom (default)
- **Stacking container:** Holds multiple alerts or notifications
- **Fixed positioning:** Sticks to corner of page/viewport
- **Enums:** ToastHorizontalPosition, ToastVerticalPosition for type-safe configuration
- **ChildContent:** Typically contains RlvAlert components

**Usage Examples:**
```razor
<!-- Default position: bottom-right -->
<RlvToast>
    <RlvAlert Color="AlertColor.Info" Message="New mail arrived." />
    <RlvAlert Color="AlertColor.Success" Message="Message sent successfully." />
</RlvToast>

<!-- Top-left position -->
<RlvToast HorizontalPosition="ToastHorizontalPosition.Start"
          VerticalPosition="ToastVerticalPosition.Top">
    <RlvAlert Color="AlertColor.Info" Message="New notification" />
</RlvToast>

<!-- Top-center position -->
<RlvToast HorizontalPosition="ToastHorizontalPosition.Center"
          VerticalPosition="ToastVerticalPosition.Top">
    <RlvAlert Color="AlertColor.Warning" Message="Warning!" />
</RlvToast>

<!-- Middle-left position -->
<RlvToast HorizontalPosition="ToastHorizontalPosition.Start"
          VerticalPosition="ToastVerticalPosition.Middle">
    <RlvAlert Color="AlertColor.Success" Message="Saved!" />
</RlvToast>

<!-- Center of screen -->
<RlvToast HorizontalPosition="ToastHorizontalPosition.Center"
          VerticalPosition="ToastVerticalPosition.Middle">
    <RlvAlert Color="AlertColor.Error" Message="Critical error!" />
</RlvToast>
```

#### RlvTooltip (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvTooltip.razor`
- `Common/Components/DaisyUI/RlvTooltip.razor.cs`

**Features Implemented:**
- **Simple text tooltip:** Via Tip parameter (data-tip attribute)
- **Rich content tooltip:** Via TipContent RenderFragment (tooltip-content div)
- **4 Placements:** Top (default), Bottom, Left, Right
- **8 Color variants:** Neutral, Primary, Secondary, Accent, Info, Success, Warning, Error
- **Force open:** Open parameter for always-visible tooltips
- **Hover activation:** Shows on hover by default
- **Enums:** TooltipPlacement, TooltipColor for type-safe configuration
- **Wraps child content:** ChildContent is the element that triggers the tooltip

**Usage Examples:**
```razor
<!-- Basic tooltip -->
<RlvTooltip Tip="Hello world">
    <RlvButton Text="Hover me" />
</RlvTooltip>

<!-- Bottom placement -->
<RlvTooltip Tip="This is a tooltip" Placement="TooltipPlacement.Bottom">
    <RlvButton Text="Bottom tooltip" />
</RlvTooltip>

<!-- Primary color -->
<RlvTooltip Tip="Primary tooltip" Color="TooltipColor.Primary">
    <RlvButton Color="ButtonColor.Primary" Text="Primary" />
</RlvTooltip>

<!-- Force open (always visible) -->
<RlvTooltip Tip="I'm always visible" Open="true">
    <RlvButton Text="Always showing tooltip" />
</RlvTooltip>

<!-- Rich content tooltip -->
<RlvTooltip Placement="TooltipPlacement.Right">
    <TipContent>
        <div class="animate-bounce text-orange-400 -rotate-10 text-2xl font-black">Wow!</div>
    </TipContent>
    <ChildContent>
        <RlvButton Text="Rich tooltip" />
    </ChildContent>
</RlvTooltip>

<!-- Multiple colors -->
<RlvTooltip Tip="Success" Color="TooltipColor.Success">
    <RlvButton Color="ButtonColor.Success" Text="Success" />
</RlvTooltip>

<RlvTooltip Tip="Error" Color="TooltipColor.Error">
    <RlvButton Color="ButtonColor.Error" Text="Error" />
</RlvTooltip>
```

#### RlvCalendar (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvCalendar.razor`
- `Common/Components/DaisyUI/RlvCalendar.razor.cs`

**Features Implemented:**
- **Styling wrapper:** Applies DaisyUI styling classes for calendar libraries
- **3 Library support:** Cally (default), Pikaday, ReactDayPicker
- **Container element:** Wraps calendar content with appropriate CSS class
- **Enum:** CalendarLibrary for selecting which library's styles to apply
- **Note:** This is a styling wrapper - actual calendar functionality requires external libraries or native HTML `<input type="date">`

**Important:**
DaisyUI Calendar is not a standalone calendar component. It provides styling for third-party calendar libraries:
- **Cally** - Web component calendar (works everywhere)
- **Pikaday** - JavaScript datepicker library
- **React Day Picker** - React calendar component
- **Native HTML** - Can also use `<input type="date">` for simple date picking

**Usage Examples:**
```razor
<!-- With native HTML date input -->
<RlvCalendar Library="CalendarLibrary.Pikaday" class="input">
    <input type="date" />
</RlvCalendar>

<!-- Cally web component (requires Cally library) -->
<RlvCalendar Library="CalendarLibrary.Cally" class="bg-base-100 border border-base-300 shadow-lg rounded-box">
    <!-- Cally calendar markup here -->
    <calendar-date>
        <calendar-month></calendar-month>
    </calendar-date>
</RlvCalendar>

<!-- Pikaday styling (requires Pikaday library) -->
<RlvCalendar Library="CalendarLibrary.Pikaday" class="input">
    <!-- Pikaday will style this input automatically -->
    <input type="text" id="datepicker" />
</RlvCalendar>

<!-- Note: For production use, integrate with:
     1. Cally via JS interop + web component
     2. Pikaday via JS interop
     3. Native HTML input type="date" for simple cases
-->
```

#### RlvCheckbox (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvCheckbox.razor`
- `Common/Components/DaisyUI/RlvCheckbox.razor.cs`

**Features Implemented:**
- **InputBase<bool> inheritance:** Full Blazor form integration and validation support
- **8 Color variants:** Primary, Secondary, Accent, Neutral, Info, Success, Warning, Error
- **5 Size variants:** XSmall (xs), Small (sm), Medium (md - default), Large (lg), XLarge (xl)
- **Label support:** Optional Label parameter wraps checkbox with label element
- **Disabled state:** Supports disabled attribute via AdditionalAttributes
- **Two-way binding:** @bind support via InputBase
- **FluentValidation compatible:** Works with EditForm and validation
- **Enums:** CheckboxColor, CheckboxSize for type-safe configuration

**Usage Examples:**
```razor
<!-- Basic checkbox -->
<RlvCheckbox @bind-Value="model.IsChecked" />

<!-- With label -->
<RlvCheckbox @bind-Value="model.RememberMe" Label="Remember me" />

<!-- Primary color, large size -->
<RlvCheckbox @bind-Value="model.Agreed"
             Label="I agree to terms"
             Color="CheckboxColor.Primary"
             Size="CheckboxSize.Large" />

<!-- In EditForm with validation -->
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />

    <RlvCheckbox @bind-Value="model.AcceptTerms"
                 Label="Accept terms and conditions"
                 Color="CheckboxColor.Success" />
    <ValidationMessage For="@(() => model.AcceptTerms)" />

    <RlvButton Type="submit" Color="ButtonColor.Primary" Text="Submit" />
</EditForm>

<!-- Disabled -->
<RlvCheckbox @bind-Value="model.IsDisabled" Label="Disabled option" disabled />

<!-- Multiple colors -->
<RlvCheckbox @bind-Value="model.Option1" Color="CheckboxColor.Success" />
<RlvCheckbox @bind-Value="model.Option2" Color="CheckboxColor.Warning" />
<RlvCheckbox @bind-Value="model.Option3" Color="CheckboxColor.Error" />

<!-- Different sizes -->
<RlvCheckbox @bind-Value="model.Tiny" Size="CheckboxSize.XSmall" />
<RlvCheckbox @bind-Value="model.Small" Size="CheckboxSize.Small" />
<RlvCheckbox @bind-Value="model.Medium" Size="CheckboxSize.Medium" />
<RlvCheckbox @bind-Value="model.Large" Size="CheckboxSize.Large" />
<RlvCheckbox @bind-Value="model.Huge" Size="CheckboxSize.XLarge" />
```

#### RlvFieldset (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvFieldset.razor`
- `Common/Components/DaisyUI/RlvFieldset.razor.cs`

**Features Implemented:**
- **Native fieldset element:** Wraps HTML `<fieldset>` for semantic form grouping
- **Legend support:** Optional Legend parameter renders `<legend class="fieldset-legend">`
- **ChildContent:** Contains form elements, labels, and other content
- **Flexible styling:** Use AdditionalAttributes for background, borders, padding (Tailwind utilities)
- **Semantic HTML:** Proper accessibility for grouped form controls

**Usage Examples:**
```razor
<!-- Basic fieldset with legend -->
<RlvFieldset Legend="Page title">
    <input type="text" class="input" placeholder="My awesome page" />
    <p class="label">You can edit page title later on from settings</p>
</RlvFieldset>

<!-- With background, border, and padding -->
<RlvFieldset Legend="Page title" class="bg-base-200 border border-base-300 p-4 rounded-box w-xs">
    <input type="text" class="input" placeholder="My awesome page" />
    <p class="label">You can edit page title later on from settings</p>
</RlvFieldset>

<!-- Multiple inputs -->
<RlvFieldset Legend="Page details" class="bg-base-200 border border-base-300 p-4 rounded-box w-xs">
    <label class="label">Title</label>
    <input type="text" class="input" placeholder="My awesome page" />

    <label class="label">Slug</label>
    <input type="text" class="input" placeholder="my-awesome-page" />

    <label class="label">Author</label>
    <input type="text" class="input" placeholder="Name" />
</RlvFieldset>

<!-- Login form -->
<RlvFieldset Legend="Login" class="bg-base-200 border border-base-300 p-4 rounded-box w-xs">
    <label class="label">Email</label>
    <input type="email" class="input" placeholder="Email" />

    <label class="label">Password</label>
    <input type="password" class="input" placeholder="Password" />

    <RlvButton Color="ButtonColor.Neutral" Text="Login" class="mt-4" />
</RlvFieldset>

<!-- With join items -->
<RlvFieldset Legend="Settings" class="bg-base-200 border border-base-300 p-4 rounded-box w-xs">
    <div class="join">
        <input type="text" class="input join-item" placeholder="Product name" />
        <RlvButton Text="save" class="join-item" />
    </div>
</RlvFieldset>
```

#### RlvFileInput (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvFileInput.razor`
- `Common/Components/DaisyUI/RlvFileInput.razor.cs`

**Features Implemented:**
- **Blazor InputFile:** Uses native Blazor InputFile component for proper file handling
- **8 Color variants:** Neutral, Primary, Secondary, Accent, Info, Success, Warning, Error
- **5 Size variants:** XSmall (xs), Small (sm), Medium (md - default), Large (lg), XLarge (xl)
- **Ghost style:** Minimal styling option
- **Disabled state:** Supports disabled attribute
- **Multiple files:** Multiple parameter for selecting multiple files
- **File type filtering:** Accept parameter for MIME type filtering
- **Event handling:** OnChange callback with InputFileChangeEventArgs
- **Enums:** FileInputColor, FileInputSize for type-safe configuration

**Usage Examples:**
```razor
<!-- Basic file input -->
<RlvFileInput OnChange="HandleFileChange" />

<!-- Primary color, large size -->
<RlvFileInput OnChange="HandleFileChange"
              Color="FileInputColor.Primary"
              Size="FileInputSize.Large" />

<!-- Ghost style -->
<RlvFileInput OnChange="HandleFileChange" Ghost="true" />

<!-- Multiple files -->
<RlvFileInput OnChange="HandleFileChange" Multiple="true" />

<!-- Accept only images -->
<RlvFileInput OnChange="HandleFileChange" Accept="image/*" />

<!-- Accept specific file types -->
<RlvFileInput OnChange="HandleFileChange" Accept=".pdf,.doc,.docx" />

<!-- Disabled -->
<RlvFileInput Disabled="true" />

<!-- With fieldset -->
<RlvFieldset Legend="Pick a file">
    <RlvFileInput OnChange="HandleFileChange" Color="FileInputColor.Success" />
    <label class="label">Max size 2MB</label>
</RlvFieldset>

<!-- Different colors -->
<RlvFileInput OnChange="HandleFileChange" Color="FileInputColor.Primary" />
<RlvFileInput OnChange="HandleFileChange" Color="FileInputColor.Secondary" />
<RlvFileInput OnChange="HandleFileChange" Color="FileInputColor.Success" />
<RlvFileInput OnChange="HandleFileChange" Color="FileInputColor.Error" />

@code {
    private async Task HandleFileChange(InputFileChangeEventArgs e)
    {
        var file = e.File;
        // Or for multiple files:
        // var files = e.GetMultipleFiles();

        // Process file(s)...
    }
}
```

#### RlvFilter (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvFilter.razor`
- `Common/Components/DaisyUI/RlvFilter.razor.cs`

**Features Implemented:**
- **Form or Div modes:** UseForm parameter (default true) renders as `<form>` or `<div>`
- **Radio button grouping:** Container for radio buttons styled as buttons
- **Auto-hide behavior:** DaisyUI automatically hides unselected options when one is chosen
- **Reset button support:** Use HTML reset button (form mode) or filter-reset radio (div mode)
- **Checkbox support:** Can also use checkboxes for multiple selections
- **Simple container:** Focuses on providing the filter structure, filtering logic managed by parent

**Important:**
Filter is a specialized component that groups radio buttons (or checkboxes) with automatic show/hide behavior. When a radio button is selected, DaisyUI CSS automatically hides other options and shows only the selected one with a reset button.

**Usage Examples:**
```razor
<!-- Form mode with radio buttons and reset -->
<RlvFilter UseForm="true">
    <input class="btn btn-square" type="reset" value="×"/>
    <input class="btn" type="radio" name="frameworks" aria-label="Svelte"/>
    <input class="btn" type="radio" name="frameworks" aria-label="Vue"/>
    <input class="btn" type="radio" name="frameworks" aria-label="React"/>
</RlvFilter>

<!-- Div mode with filter-reset radio -->
<RlvFilter UseForm="false">
    <input class="btn filter-reset" type="radio" name="metaframeworks" aria-label="All"/>
    <input class="btn" type="radio" name="metaframeworks" aria-label="SvelteKit"/>
    <input class="btn" type="radio" name="metaframeworks" aria-label="Nuxt"/>
    <input class="btn" type="radio" name="metaframeworks" aria-label="Next.js"/>
</RlvFilter>

<!-- With checkboxes for multiple selections (no filter class needed) -->
<form>
    <input class="btn" type="checkbox" name="features" aria-label="TypeScript"/>
    <input class="btn" type="checkbox" name="features" aria-label="SSR"/>
    <input class="btn" type="checkbox" name="features" aria-label="SPA"/>
    <input class="btn btn-square" type="reset" value="×"/>
</form>

<!-- With button components -->
<RlvFilter>
    <input class="btn btn-square" type="reset" value="×"/>
    <input class="btn btn-primary" type="radio" name="status" aria-label="Active"/>
    <input class="btn btn-secondary" type="radio" name="status" aria-label="Pending"/>
    <input class="btn btn-error" type="radio" name="status" aria-label="Inactive"/>
</RlvFilter>
```

#### RlvInputField (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvInputField.razor`
- `Common/Components/DaisyUI/RlvInputField.razor.cs`

**Features Implemented:**
- **InputBase<string> inheritance:** Full Blazor form integration and validation support
- **8 Color variants:** Neutral, Primary, Secondary, Accent, Info, Success, Warning, Error
- **5 Size variants:** XSmall (xs), Small (sm), Medium (md - default), Large (lg), XLarge (xl)
- **Ghost style:** Minimal styling option
- **Multiple input types:** text, password, email, number, date, datetime-local, week, month, tel, url, search, time
- **Disabled state:** Supports disabled attribute via AdditionalAttributes
- **Two-way binding:** @bind support via InputBase
- **FluentValidation compatible:** Works with EditForm and validation
- **Placeholder support:** Optional placeholder parameter
- **Enums:** InputFieldColor, InputFieldSize for type-safe configuration

**Usage Examples:**
```razor
<!-- Basic text input -->
<RlvInputField @bind-Value="model.Name" Placeholder="Enter your name" />

<!-- Email input with validation -->
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />

    <RlvInputField @bind-Value="model.Email"
                   Type="email"
                   Placeholder="mail@site.com"
                   Color="InputFieldColor.Primary" />
    <ValidationMessage For="@(() => model.Email)" />
</EditForm>

<!-- Password input -->
<RlvInputField @bind-Value="model.Password"
               Type="password"
               Placeholder="Enter password"
               Color="InputFieldColor.Info" />

<!-- Number input -->
<RlvInputField @bind-Value="model.Age"
               Type="number"
               Placeholder="Your age"
               Size="InputFieldSize.Small" />

<!-- Date input -->
<RlvInputField @bind-Value="model.BirthDate"
               Type="date"
               Color="InputFieldColor.Success" />

<!-- Search input -->
<RlvInputField @bind-Value="model.SearchTerm"
               Type="search"
               Placeholder="Search..."
               Ghost="true" />

<!-- Telephone input -->
<RlvInputField @bind-Value="model.Phone"
               Type="tel"
               Placeholder="Phone number"
               pattern="[0-9]*" />

<!-- URL input -->
<RlvInputField @bind-Value="model.Website"
               Type="url"
               Placeholder="https://"
               Color="InputFieldColor.Accent" />

<!-- Different colors -->
<RlvInputField @bind-Value="model.Field1" Color="InputFieldColor.Primary" Placeholder="Primary" />
<RlvInputField @bind-Value="model.Field2" Color="InputFieldColor.Success" Placeholder="Success" />
<RlvInputField @bind-Value="model.Field3" Color="InputFieldColor.Warning" Placeholder="Warning" />
<RlvInputField @bind-Value="model.Field4" Color="InputFieldColor.Error" Placeholder="Error" />

<!-- Different sizes -->
<RlvInputField @bind-Value="model.Small" Size="InputFieldSize.Small" Placeholder="Small" />
<RlvInputField @bind-Value="model.Medium" Size="InputFieldSize.Medium" Placeholder="Medium" />
<RlvInputField @bind-Value="model.Large" Size="InputFieldSize.Large" Placeholder="Large" />

<!-- Disabled -->
<RlvInputField @bind-Value="model.Readonly" Placeholder="Read only" disabled />
```

#### RlvLabel (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvLabel.razor`
- `Common/Components/DaisyUI/RlvLabel.razor.cs`

**Features Implemented:**
- **Two modes:** Inline label (span) or floating label (label element)
- **Inline label:** Simple span with "label" class for use inside input/select wrappers
- **Floating label:** Label container that floats above input when focused
- **Text parameter:** Simple text content
- **ChildContent:** For complex content including inputs
- **Flexible rendering:** Renders as span or label based on Floating parameter

**Usage Examples:**
```razor
<!-- Inline label at the start (inside input wrapper) -->
<label class="input">
    <RlvLabel Text="https://" />
    <input type="text" placeholder="URL" class="grow" />
</label>

<!-- Inline label at the end -->
<label class="input">
    <input type="text" placeholder="domain name" class="grow" />
    <RlvLabel Text=".com" />
</label>

<!-- Label for select -->
<label class="select">
    <RlvLabel Text="Type" />
    <select>
        <option>Personal</option>
        <option>Business</option>
    </select>
</label>

<!-- Label for date input -->
<label class="input">
    <RlvLabel Text="Publish date" />
    <input type="date" />
</label>

<!-- Floating label -->
<RlvLabel Floating="true" class="w-full max-w-xs">
    <span>Your Email</span>
    <input type="email" placeholder="mail@site.com" class="input input-md" />
</RlvLabel>

<!-- Floating label with different sizes -->
<RlvLabel Floating="true">
    <input type="text" placeholder="Extra Small" class="input input-xs" />
    <span>Extra Small</span>
</RlvLabel>

<RlvLabel Floating="true">
    <input type="text" placeholder="Small" class="input input-sm" />
    <span>Small</span>
</RlvLabel>

<RlvLabel Floating="true">
    <input type="text" placeholder="Medium" class="input input-md" />
    <span>Medium</span>
</RlvLabel>

<RlvLabel Floating="true">
    <input type="text" placeholder="Large" class="input input-lg" />
    <span>Large</span>
</RlvLabel>

<!-- Floating label with RlvInputField -->
<RlvLabel Floating="true" class="w-full max-w-xs">
    <span>Username</span>
    <RlvInputField @bind-Value="model.Username"
                   Placeholder="Enter username"
                   Size="InputFieldSize.Medium" />
</RlvLabel>
```

#### RlvRadio (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvRadio.razor`
- `Common/Components/DaisyUI/RlvRadio.razor.cs`

**Features Implemented:**
- **InputBase<string> inheritance:** Full Blazor form integration and validation support
- **8 Color variants:** Neutral, Primary, Secondary, Accent, Info, Success, Warning, Error
- **5 Size variants:** XSmall (xs), Small (sm), Medium (md - default), Large (lg), XLarge (xl)
- **Name grouping:** Required Name parameter for radio button groups (only one in group can be selected)
- **Radio value:** RadioValue parameter defines the value when this radio is selected
- **Label support:** Optional Label parameter wraps radio with label element and text
- **Two-way binding:** @bind-Value support via InputBase<string>
- **FluentValidation compatible:** Works with EditForm and validation
- **Disabled state:** Supports disabled attribute via AdditionalAttributes
- **Custom label styling:** LabelClass and LabelTextClass for styling flexibility
- **Enums:** RadioColor, RadioSize for type-safe configuration

**Important:**
Each set of radio inputs should have unique `name` attributes to avoid conflicts with other sets of radio inputs on the same page. All radio buttons with the same Name belong to the same group, and only one can be selected at a time.

**Usage Examples:**
```razor
<!-- Basic radio group -->
<RlvRadio @bind-Value="model.SelectedOption" Name="option-group" RadioValue="option1" />
<RlvRadio @bind-Value="model.SelectedOption" Name="option-group" RadioValue="option2" />

<!-- Radio group with labels -->
<RlvRadio @bind-Value="model.Gender" Name="gender" RadioValue="male" Label="Male" />
<RlvRadio @bind-Value="model.Gender" Name="gender" RadioValue="female" Label="Female" />

<!-- Primary color, large size -->
<RlvRadio @bind-Value="model.Plan"
          Name="plan"
          RadioValue="pro"
          Label="Professional Plan"
          Color="RadioColor.Primary"
          Size="RadioSize.Large" />

<!-- In EditForm with validation -->
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />

    <div class="form-control">
        <label>Select your preference:</label>
        <RlvRadio @bind-Value="model.Preference"
                  Name="preference"
                  RadioValue="email"
                  Label="Email notifications"
                  Color="RadioColor.Success" />
        <RlvRadio @bind-Value="model.Preference"
                  Name="preference"
                  RadioValue="sms"
                  Label="SMS notifications"
                  Color="RadioColor.Success" />
        <RlvRadio @bind-Value="model.Preference"
                  Name="preference"
                  RadioValue="none"
                  Label="No notifications"
                  Color="RadioColor.Success" />
    </div>
    <ValidationMessage For="@(() => model.Preference)" />

    <RlvButton Type="submit" Color="ButtonColor.Primary" Text="Submit" />
</EditForm>

<!-- Different colors -->
<RlvRadio @bind-Value="model.Status" Name="status" RadioValue="active" Color="RadioColor.Success" />
<RlvRadio @bind-Value="model.Status" Name="status" RadioValue="pending" Color="RadioColor.Warning" />
<RlvRadio @bind-Value="model.Status" Name="status" RadioValue="error" Color="RadioColor.Error" />

<!-- Different sizes -->
<RlvRadio @bind-Value="model.Size1" Name="sizes" RadioValue="xs" Size="RadioSize.XSmall" />
<RlvRadio @bind-Value="model.Size2" Name="sizes" RadioValue="sm" Size="RadioSize.Small" />
<RlvRadio @bind-Value="model.Size3" Name="sizes" RadioValue="md" Size="RadioSize.Medium" />
<RlvRadio @bind-Value="model.Size4" Name="sizes" RadioValue="lg" Size="RadioSize.Large" />
<RlvRadio @bind-Value="model.Size5" Name="sizes" RadioValue="xl" Size="RadioSize.XLarge" />

<!-- Disabled -->
<RlvRadio @bind-Value="model.Option" Name="options" RadioValue="disabled" Label="Disabled option" disabled />

<!-- Custom label styling -->
<RlvRadio @bind-Value="model.Custom"
          Name="custom"
          RadioValue="styled"
          Label="Custom styled label"
          LabelClass="flex gap-2 items-center"
          LabelTextClass="font-bold text-primary" />

<!-- With custom colors via AdditionalAttributes -->
<RlvRadio @bind-Value="model.Color"
          Name="colors"
          RadioValue="red"
          class="bg-red-100 border-red-300 checked:bg-red-200 checked:text-red-600 checked:border-red-600" />

@code {
    public class FormModel
    {
        [Required(ErrorMessage = "Please select an option")]
        public string? Preference { get; set; }

        public string? Gender { get; set; }
        public string? Status { get; set; }
    }
}
```

#### RlvRange (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvRange.razor`
- `Common/Components/DaisyUI/RlvRange.razor.cs`

**Features Implemented:**
- **InputBase<double> inheritance:** Full Blazor form integration and validation support
- **8 Color variants:** Neutral, Primary, Secondary, Accent, Info, Success, Warning, Error
- **5 Size variants:** XSmall (xs), Small (sm), Medium (md - default), Large (lg), XLarge (xl)
- **Min/Max parameters:** Define the range boundaries (default 0-100)
- **Step parameter:** Control increment/decrement step size (default 1)
- **Two-way binding:** @bind-Value support via InputBase<double>
- **Real-time updates:** Uses @oninput for immediate value changes as slider moves
- **FluentValidation compatible:** Works with EditForm and validation
- **CSS variables:** Supports custom styling via --range-bg, --range-thumb, --range-fill
- **Enums:** RangeColor, RangeSize for type-safe configuration

**Usage Examples:**
```razor
<!-- Basic range slider -->
<RlvRange @bind-Value="model.Volume" />

<!-- Range with custom min/max -->
<RlvRange @bind-Value="model.Age" Min="18" Max="100" />

<!-- Primary color, large size -->
<RlvRange @bind-Value="model.Brightness"
          Min="0"
          Max="100"
          Color="RangeColor.Primary"
          Size="RangeSize.Large" />

<!-- With steps -->
<RlvRange @bind-Value="model.Rating"
          Min="0"
          Max="100"
          Step="25"
          Color="RangeColor.Success" />

<!-- In EditForm with validation -->
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />

    <div class="form-control">
        <label class="label">
            <span class="label-text">Select your budget: $@model.Budget</span>
        </label>
        <RlvRange @bind-Value="model.Budget"
                  Min="0"
                  Max="1000"
                  Step="50"
                  Color="RangeColor.Primary" />
        <ValidationMessage For="@(() => model.Budget)" />
    </div>

    <RlvButton Type="submit" Color="ButtonColor.Primary" Text="Submit" />
</EditForm>

<!-- Different colors -->
<RlvRange @bind-Value="model.Value1" Color="RangeColor.Primary" />
<RlvRange @bind-Value="model.Value2" Color="RangeColor.Success" />
<RlvRange @bind-Value="model.Value3" Color="RangeColor.Warning" />
<RlvRange @bind-Value="model.Value4" Color="RangeColor.Error" />

<!-- Different sizes -->
<div class="flex flex-col gap-4 w-full max-w-xs">
    <RlvRange @bind-Value="model.Size1" Size="RangeSize.XSmall" />
    <RlvRange @bind-Value="model.Size2" Size="RangeSize.Small" />
    <RlvRange @bind-Value="model.Size3" Size="RangeSize.Medium" />
    <RlvRange @bind-Value="model.Size4" Size="RangeSize.Large" />
    <RlvRange @bind-Value="model.Size5" Size="RangeSize.XLarge" />
</div>

<!-- With measure marks using steps -->
<div class="w-full max-w-xs">
    <RlvRange @bind-Value="model.Level" Min="0" Max="100" Step="25" />
    <div class="flex justify-between px-2.5 mt-2 text-xs">
        <span>|</span>
        <span>|</span>
        <span>|</span>
        <span>|</span>
        <span>|</span>
    </div>
    <div class="flex justify-between px-2.5 mt-2 text-xs">
        <span>0</span>
        <span>25</span>
        <span>50</span>
        <span>75</span>
        <span>100</span>
    </div>
</div>

<!-- Custom colors via CSS variables (AdditionalAttributes) -->
<RlvRange @bind-Value="model.Custom"
          class="text-blue-300 [--range-bg:orange] [--range-thumb:blue] [--range-fill:0]" />

<!-- Disabled -->
<RlvRange @bind-Value="model.Readonly" disabled />

@code {
    public class FormModel
    {
        public double Volume { get; set; } = 50;
        public double Age { get; set; } = 25;
        public double Brightness { get; set; } = 75;

        [Range(0, 1000, ErrorMessage = "Budget must be between 0 and 1000")]
        public double Budget { get; set; } = 500;
    }
}
```

#### RlvRating (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvRating.razor`
- `Common/Components/DaisyUI/RlvRating.razor.cs`

**Features Implemented:**
- **InputBase<int> inheritance:** Full Blazor form integration and validation support
- **3 Shape variants:** Star (mask-star), Star2 (mask-star-2), Heart (mask-heart)
- **5 Size variants:** XSmall (xs), Small (sm), Medium (md - default), Large (lg), XLarge (xl)
- **Count parameter:** Configurable number of rating items (default 5)
- **Half-star support:** Enable half-star ratings with `Half` parameter (doubles inputs)
- **Allow clear:** Optional hidden radio button to clear rating (value 0)
- **Read-only mode:** Display-only ratings with `IsReadOnly` parameter
- **Color support:** Custom Tailwind color classes (e.g., "bg-orange-400")
- **Name grouping:** Required unique Name parameter for radio button group
- **Two-way binding:** @bind-Value support via InputBase<int>
- **FluentValidation compatible:** Works with EditForm and validation
- **Auto-generated inputs:** Dynamically creates radio buttons based on Count
- **Enums:** RatingShape, RatingSize for type-safe configuration

**Important:**
- Each rating group should have a unique `name` attribute
- For half-star ratings, Value represents half-stars (e.g., 3 = 1.5 stars, 10 = 5 stars)
- For full-star ratings, Value represents full stars (e.g., 3 = 3 stars, 5 = 5 stars)
- Value 0 means no rating (when AllowClear is true)

**Usage Examples:**
```razor
<!-- Basic 5-star rating -->
<RlvRating @bind-Value="model.Rating" Name="product-rating" />

<!-- Star2 shape with custom color -->
<RlvRating @bind-Value="model.Quality"
           Name="quality-rating"
           Shape="RatingShape.Star2"
           Color="bg-orange-400" />

<!-- Heart shape with multiple colors (custom approach) -->
<div class="rating gap-1">
    <input type="radio" name="hearts" value="1" class="mask mask-heart bg-red-400" aria-label="1 star" />
    <input type="radio" name="hearts" value="2" class="mask mask-heart bg-orange-400" aria-label="2 star" checked />
    <input type="radio" name="hearts" value="3" class="mask mask-heart bg-yellow-400" aria-label="3 star" />
    <input type="radio" name="hearts" value="4" class="mask mask-heart bg-lime-400" aria-label="4 star" />
    <input type="radio" name="hearts" value="5" class="mask mask-heart bg-green-400" aria-label="5 star" />
</div>

<!-- Different sizes -->
<RlvRating @bind-Value="model.Size1" Name="size-xs" Size="RatingSize.XSmall" Color="bg-orange-400" />
<RlvRating @bind-Value="model.Size2" Name="size-sm" Size="RatingSize.Small" Color="bg-orange-400" />
<RlvRating @bind-Value="model.Size3" Name="size-md" Size="RatingSize.Medium" Color="bg-orange-400" />
<RlvRating @bind-Value="model.Size4" Name="size-lg" Size="RatingSize.Large" Color="bg-orange-400" />
<RlvRating @bind-Value="model.Size5" Name="size-xl" Size="RatingSize.XLarge" Color="bg-orange-400" />

<!-- With clear button -->
<RlvRating @bind-Value="model.OptionalRating"
           Name="optional-rating"
           AllowClear="true"
           Shape="RatingShape.Star2"
           Size="RatingSize.Large" />

<!-- Half-star rating -->
<RlvRating @bind-Value="model.DetailedRating"
           Name="detailed-rating"
           Half="true"
           AllowClear="true"
           Shape="RatingShape.Star2"
           Color="bg-green-500"
           Size="RatingSize.Large" />

<!-- Read-only rating (display) -->
<RlvRating Value="@averageRating"
           Name="display-rating"
           Shape="RatingShape.Star2"
           Color="bg-orange-400"
           IsReadOnly="true" />

<!-- In EditForm with validation -->
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />

    <div class="form-control">
        <label class="label">
            <span class="label-text">Rate this product</span>
        </label>
        <RlvRating @bind-Value="model.ProductRating"
                   Name="product-rating"
                   Shape="RatingShape.Star2"
                   Color="bg-orange-400"
                   Size="RatingSize.Large"
                   AllowClear="true" />
        <ValidationMessage For="@(() => model.ProductRating)" />
    </div>

    <RlvButton Type="submit" Color="ButtonColor.Primary" Text="Submit Review" />
</EditForm>

<!-- Custom count (3 stars) -->
<RlvRating @bind-Value="model.SimpleRating"
           Name="simple-rating"
           Count="3"
           Color="bg-blue-500" />

<!-- Heart shape rating -->
<RlvRating @bind-Value="model.Favorite"
           Name="favorite"
           Shape="RatingShape.Heart"
           Color="bg-red-400"
           Count="5" />

@code {
    private int averageRating = 4;

    public class FormModel
    {
        public int Rating { get; set; } = 3;
        public int Quality { get; set; } = 4;
        public int OptionalRating { get; set; } = 0;

        // For half-star: Value 3 = 1.5 stars, Value 10 = 5 stars
        public int DetailedRating { get; set; } = 7; // 3.5 stars

        [Range(1, 5, ErrorMessage = "Please provide a rating")]
        public int ProductRating { get; set; }

        public int SimpleRating { get; set; } = 2;
        public int Favorite { get; set; } = 5;
    }
}
```

#### RlvSelect (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvSelect.razor`
- `Common/Components/DaisyUI/RlvSelect.razor.cs`

**Features Implemented:**
- **InputBase<TValue> inheritance:** Full Blazor form integration and validation support with generic type support
- **Generic type support:** Works with string, int, enum, and other value types via `<TValue>`
- **8 Color variants:** Neutral, Primary, Secondary, Accent, Info, Success, Warning, Error
- **5 Size variants:** XSmall (xs), Small (sm), Medium (md - default), Large (lg), XLarge (xl)
- **Ghost style:** Minimal background styling option
- **ChildContent:** Contains option elements for dropdown choices
- **Two-way binding:** @bind-Value support via InputBase<TValue>
- **FluentValidation compatible:** Works with EditForm and validation
- **Disabled state:** Supports disabled attribute via AdditionalAttributes
- **Enums:** SelectColor, SelectSize for type-safe configuration

**Usage Examples:**
```razor
<!-- Basic select with string binding -->
<RlvSelect @bind-Value="model.Color">
    <option disabled selected>Pick a color</option>
    <option>Crimson</option>
    <option>Amber</option>
    <option>Velvet</option>
</RlvSelect>

<!-- Primary color, large size -->
<RlvSelect @bind-Value="model.Browser"
           Color="SelectColor.Primary"
           Size="SelectSize.Large">
    <option disabled selected>Pick a browser</option>
    <option>Chrome</option>
    <option>Firefox</option>
    <option>Safari</option>
</RlvSelect>

<!-- Ghost style (minimal background) -->
<RlvSelect @bind-Value="model.Font" Ghost="true">
    <option disabled selected>Pick a font</option>
    <option>Inter</option>
    <option>Poppins</option>
    <option>Raleway</option>
</RlvSelect>

<!-- With fieldset and labels -->
<RlvFieldset Legend="Browsers" class="w-xs">
    <RlvSelect @bind-Value="model.PreferredBrowser">
        <option disabled selected>Pick a browser</option>
        <option>Chrome</option>
        <option>Firefox</option>
        <option>Safari</option>
    </RlvSelect>
    <span class="label">Optional</span>
</RlvFieldset>

<!-- Different colors -->
<RlvSelect @bind-Value="model.Editor" Color="SelectColor.Primary">
    <option disabled selected>Pick a text editor</option>
    <option>VSCode</option>
    <option>VSCode fork</option>
    <option>Another VSCode fork</option>
</RlvSelect>

<RlvSelect @bind-Value="model.Language" Color="SelectColor.Secondary">
    <option disabled selected>Pick a language</option>
    <option>Zig</option>
    <option>Go</option>
    <option>Rust</option>
</RlvSelect>

<RlvSelect @bind-Value="model.Theme" Color="SelectColor.Accent">
    <option selected disabled>Color scheme</option>
    <option>Light mode</option>
    <option>Dark mode</option>
    <option>System</option>
</RlvSelect>

<!-- Different sizes -->
<RlvSelect @bind-Value="model.Size1" Size="SelectSize.XSmall">
    <option disabled selected>Xsmall</option>
    <option>Xsmall Apple</option>
    <option>Xsmall Orange</option>
</RlvSelect>

<RlvSelect @bind-Value="model.Size2" Size="SelectSize.Small">
    <option disabled selected>Small</option>
    <option>Small Apple</option>
    <option>Small Orange</option>
</RlvSelect>

<RlvSelect @bind-Value="model.Size3" Size="SelectSize.Large">
    <option disabled selected>Large</option>
    <option>Large Apple</option>
    <option>Large Orange</option>
</RlvSelect>

<!-- Disabled -->
<RlvSelect @bind-Value="model.Disabled" disabled>
    <option>You can't touch this</option>
</RlvSelect>

<!-- With enum binding -->
<RlvSelect @bind-Value="model.Status">
    <option disabled selected>Select status</option>
    <option value="@Status.Active">Active</option>
    <option value="@Status.Pending">Pending</option>
    <option value="@Status.Inactive">Inactive</option>
</RlvSelect>

<!-- With int binding -->
<RlvSelect @bind-Value="model.Quantity" Color="SelectColor.Info">
    <option disabled selected>Select quantity</option>
    <option value="1">1 item</option>
    <option value="5">5 items</option>
    <option value="10">10 items</option>
    <option value="50">50 items</option>
</RlvSelect>

<!-- In EditForm with validation -->
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />

    <div class="form-control">
        <label class="label">
            <span class="label-text">Select your country</span>
        </label>
        <RlvSelect @bind-Value="model.Country"
                   Color="SelectColor.Primary"
                   Size="SelectSize.Large">
            <option disabled selected>Pick a country</option>
            <option>United States</option>
            <option>United Kingdom</option>
            <option>Canada</option>
            <option>Australia</option>
        </RlvSelect>
        <ValidationMessage For="@(() => model.Country)" />
    </div>

    <RlvButton Type="submit" Color="ButtonColor.Primary" Text="Submit" />
</EditForm>

<!-- Native OS style dropdown (appearance-none) -->
<RlvSelect @bind-Value="model.Native" class="appearance-none">
    <option disabled selected>Pick a color</option>
    <option>Crimson</option>
    <option>Amber</option>
    <option>Velvet</option>
</RlvSelect>

@code {
    public enum Status
    {
        Active,
        Pending,
        Inactive
    }

    public class FormModel
    {
        public string? Color { get; set; }
        public string? Browser { get; set; }
        public string? Font { get; set; }
        public string? PreferredBrowser { get; set; }
        public string? Editor { get; set; }
        public string? Language { get; set; }
        public string? Theme { get; set; }

        public Status Status { get; set; } = Status.Active;
        public int Quantity { get; set; } = 1;

        [Required(ErrorMessage = "Please select a country")]
        public string? Country { get; set; }
    }
}
```

#### RlvTextArea (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvTextArea.razor`
- `Common/Components/DaisyUI/RlvTextArea.razor.cs`

**Features Implemented:**
- **InputBase<string?> inheritance:** Full Blazor form integration and validation support
- **8 Color variants:** Neutral, Primary, Secondary, Accent, Info, Success, Warning, Error
- **5 Size variants:** XSmall (xs), Small (sm), Medium (md - default), Large (lg), XLarge (xl)
- **Ghost style:** Minimal background styling option
- **Rows parameter:** Control visible text lines (default 4)
- **Placeholder support:** Optional placeholder text
- **Two-way binding:** @bind-Value support via InputBase<string?>
- **FluentValidation compatible:** Works with EditForm and validation
- **Real-time updates:** Uses @oninput for immediate value changes
- **Disabled state:** Supports disabled attribute via AdditionalAttributes
- **Enums:** TextAreaColor, TextAreaSize for type-safe configuration

**Usage Examples:**
```razor
<!-- Basic textarea -->
<RlvTextArea @bind-Value="model.Bio" Placeholder="Bio" />

<!-- Primary color with custom rows -->
<RlvTextArea @bind-Value="model.Description"
             Placeholder="Enter description"
             Color="TextAreaColor.Primary"
             Rows="6" />

<!-- Ghost style (minimal background) -->
<RlvTextArea @bind-Value="model.Notes"
             Placeholder="Notes"
             Ghost="true" />

<!-- With fieldset and labels -->
<RlvFieldset Legend="Your bio" class="w-xs">
    <RlvTextArea @bind-Value="model.Biography"
                 Placeholder="Tell us about yourself"
                 class="h-24" />
    <div class="label">Optional</div>
</RlvFieldset>

<!-- Different colors -->
<div class="grid gap-4 w-xs">
    <RlvTextArea @bind-Value="model.Field1" Placeholder="Primary" Color="TextAreaColor.Primary" />
    <RlvTextArea @bind-Value="model.Field2" Placeholder="Secondary" Color="TextAreaColor.Secondary" />
    <RlvTextArea @bind-Value="model.Field3" Placeholder="Accent" Color="TextAreaColor.Accent" />
    <RlvTextArea @bind-Value="model.Field4" Placeholder="Neutral" Color="TextAreaColor.Neutral" />
    <RlvTextArea @bind-Value="model.Field5" Placeholder="Info" Color="TextAreaColor.Info" />
    <RlvTextArea @bind-Value="model.Field6" Placeholder="Success" Color="TextAreaColor.Success" />
    <RlvTextArea @bind-Value="model.Field7" Placeholder="Warning" Color="TextAreaColor.Warning" />
    <RlvTextArea @bind-Value="model.Field8" Placeholder="Error" Color="TextAreaColor.Error" />
</div>

<!-- Different sizes -->
<div class="flex flex-col gap-4 w-full items-center">
    <RlvTextArea @bind-Value="model.Size1" Placeholder="Xsmall" Size="TextAreaSize.XSmall" />
    <RlvTextArea @bind-Value="model.Size2" Placeholder="Small" Size="TextAreaSize.Small" />
    <RlvTextArea @bind-Value="model.Size3" Placeholder="Medium" Size="TextAreaSize.Medium" />
    <RlvTextArea @bind-Value="model.Size4" Placeholder="Large" Size="TextAreaSize.Large" />
    <RlvTextArea @bind-Value="model.Size5" Placeholder="Xlarge" Size="TextAreaSize.XLarge" />
</div>

<!-- Disabled -->
<RlvTextArea @bind-Value="model.Readonly" Placeholder="Bio" disabled />

<!-- In EditForm with validation -->
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />

    <div class="form-control">
        <label class="label">
            <span class="label-text">Tell us about your project</span>
        </label>
        <RlvTextArea @bind-Value="model.ProjectDescription"
                     Placeholder="Describe your project..."
                     Color="TextAreaColor.Primary"
                     Rows="8"
                     class="w-full" />
        <ValidationMessage For="@(() => model.ProjectDescription)" />
        <label class="label">
            <span class="label-text-alt">Min 50 characters</span>
        </label>
    </div>

    <RlvButton Type="submit" Color="ButtonColor.Primary" Text="Submit" />
</EditForm>

<!-- Long text with custom height -->
<RlvTextArea @bind-Value="model.Article"
             Placeholder="Write your article..."
             Rows="15"
             Color="TextAreaColor.Success"
             class="w-full max-w-2xl" />

<!-- Comment box example -->
<div class="form-control w-full max-w-xs">
    <label class="label">
        <span class="label-text">Your comment</span>
        <span class="label-text-alt">Max 500 characters</span>
    </label>
    <RlvTextArea @bind-Value="model.Comment"
                 Placeholder="Leave a comment..."
                 Rows="4"
                 Color="TextAreaColor.Info"
                 class="w-full" />
    <label class="label">
        <span class="label-text-alt">@(model.Comment?.Length ?? 0) / 500</span>
    </label>
</div>

@code {
    public class FormModel
    {
        public string? Bio { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public string? Biography { get; set; }

        [Required(ErrorMessage = "Project description is required")]
        [StringLength(1000, MinimumLength = 50, ErrorMessage = "Description must be between 50 and 1000 characters")]
        public string? ProjectDescription { get; set; }

        public string? Article { get; set; }

        [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters")]
        public string? Comment { get; set; }
    }
}
```

#### RlvDropdown (Completed 2025-10-14)
**Files Created:**
- `Common/Components/DaisyUI/RlvDropdown.razor`
- `Common/Components/DaisyUI/RlvDropdown.razor.cs`

**Features Implemented:**
- **3 Implementation methods:** DetailsSummary (native details/summary), PopoverApi (new HTML popover standard), CssFocus (default - focus-based)
- **Horizontal placements:** Start (default), Center, End
- **Vertical placements:** Top, Bottom (default), Left, Right
- **Modifiers:** Hover (opens on hover), Open (force open)
- **Content flexibility:** Supports any content (menus, cards, forms, etc.)
- **ButtonContent RenderFragment:** Customizable trigger button
- **Content RenderFragment:** Dropdown content (typically menu or card)
- **Separate attribute support:** ButtonAttributes, ContentAttributes, AdditionalAttributes for fine-grained control
- **Auto-generated IDs:** Unique popover/anchor IDs for PopoverApi method
- **Enums:** DropdownMethod, DropdownHorizontalPlacement, DropdownVerticalPlacement for type-safe configuration
- **Accessibility:** Proper tabindex, role attributes, ARIA support

**Implementation Methods:**
1. **DetailsSummary** - Native HTML `<details>`/`<summary>` elements with click toggle
2. **PopoverApi** - Modern popover API with anchor positioning (top layer, no z-index issues)
3. **CssFocus** - CSS-based with tabindex and role="button" (Safari compatible)

**Important Notes:**
- PopoverApi uses CSS anchor positioning (not yet supported in Firefox/Safari - falls back to centered modal)
- CssFocus method uses div with tabindex="0" instead of button (Safari focus bug workaround)
- Can combine horizontal + vertical placements (e.g., Top + Center)

**Usage Examples:**
```razor
<!-- Basic dropdown (CSS Focus method, default) -->
<RlvDropdown>
    <ButtonContent>
        <div class="btn m-1">Click to open</div>
    </ButtonContent>
    <Content>
        <ul class="menu bg-base-100 rounded-box z-1 w-52 p-2 shadow-sm">
            <li><a>Item 1</a></li>
            <li><a>Item 2</a></li>
        </ul>
    </Content>
</RlvDropdown>

<!-- Dropdown with placement -->
<RlvDropdown HorizontalPlacement="DropdownHorizontalPlacement.End"
             VerticalPlacement="DropdownVerticalPlacement.Top">
    <ButtonContent>
        <div class="btn m-1">Click ⬆️</div>
    </ButtonContent>
    <Content>
        <ul class="menu bg-base-100 rounded-box z-1 w-52 p-2 shadow-sm">
            <li><a>Item 1</a></li>
            <li><a>Item 2</a></li>
        </ul>
    </Content>
</RlvDropdown>

<!-- Dropdown on hover -->
<RlvDropdown Hover="true">
    <ButtonContent>
        <div class="btn m-1">Hover me</div>
    </ButtonContent>
    <Content>
        <ul class="menu bg-base-100 rounded-box z-1 w-52 p-2 shadow-sm">
            <li><a>Item 1</a></li>
            <li><a>Item 2</a></li>
        </ul>
    </Content>
</RlvDropdown>

<!-- Details/Summary method -->
<RlvDropdown Method="DropdownMethod.DetailsSummary">
    <ButtonContent>
        <div class="btn m-1">open or close</div>
    </ButtonContent>
    <Content>
        <ul class="menu bg-base-100 rounded-box z-1 w-52 p-2 shadow-sm">
            <li><a>Item 1</a></li>
            <li><a>Item 2</a></li>
        </ul>
    </Content>
</RlvDropdown>

<!-- Popover API method (modern, top layer) -->
<RlvDropdown Method="DropdownMethod.PopoverApi">
    <ButtonContent>
        <div class="btn">Modern Popover</div>
    </ButtonContent>
    <Content>
        <ul class="menu w-52 rounded-box bg-base-100 shadow-sm">
            <li><a>Item 1</a></li>
            <li><a>Item 2</a></li>
        </ul>
    </Content>
</RlvDropdown>

<!-- Card as dropdown content -->
<RlvDropdown>
    <ButtonContent>
        <div class="btn m-1">Click</div>
    </ButtonContent>
    <Content>
        <div class="card card-sm bg-base-100 z-1 w-64 shadow-md">
            <div class="card-body">
                <p>This is a card. You can use any element as dropdown content.</p>
            </div>
        </div>
    </Content>
</RlvDropdown>

<!-- Dropdown in navbar -->
<div class="navbar bg-base-200">
    <div class="ps-4">
        <a class="text-lg font-bold">daisyUI</a>
    </div>
    <div class="flex grow justify-end px-2">
        <RlvDropdown HorizontalPlacement="DropdownHorizontalPlacement.End">
            <ButtonContent>
                <div class="btn btn-ghost rounded-field">Dropdown</div>
            </ButtonContent>
            <Content>
                <ul class="menu bg-base-200 rounded-box z-1 mt-4 w-52 p-2 shadow-sm">
                    <li><a>Item 1</a></li>
                    <li><a>Item 2</a></li>
                </ul>
            </Content>
        </RlvDropdown>
    </div>
</div>

<!-- Force open (always visible) -->
<RlvDropdown Open="true">
    <ButtonContent>
        <div class="btn m-1">Button</div>
    </ButtonContent>
    <Content>
        <ul class="menu bg-base-100 rounded-box z-1 w-52 p-2 shadow-sm">
            <li><a>Always visible</a></li>
        </ul>
    </Content>
</RlvDropdown>

<!-- All placements combined -->
<RlvDropdown HorizontalPlacement="DropdownHorizontalPlacement.Center"
             VerticalPlacement="DropdownVerticalPlacement.Top">
    <ButtonContent>
        <div class="btn m-1">Top Center ⬆️</div>
    </ButtonContent>
    <Content>
        <ul class="menu bg-base-100 rounded-box z-1 w-52 p-2 shadow-sm">
            <li><a>Item 1</a></li>
            <li><a>Item 2</a></li>
        </ul>
    </Content>
</RlvDropdown>

<!-- With RlvButton component -->
<RlvDropdown Hover="true"
             HorizontalPlacement="DropdownHorizontalPlacement.End">
    <ButtonContent>
        <RlvButton Color="ButtonColor.Primary" Text="Menu" />
    </ButtonContent>
    <Content>
        <ul class="menu bg-base-100 rounded-box z-1 w-52 p-2 shadow-sm">
            <li><a>Profile</a></li>
            <li><a>Settings</a></li>
            <li><a>Logout</a></li>
        </ul>
    </Content>
</RlvDropdown>
```

---
