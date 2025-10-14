# Claude AI Context for RLVStack Project

## Project Overview

**Project Name:** Krafter - RLVStack
**Description:** .NET 9 full-stack platform with ASP.NET Core backend and Blazor WebAssembly/Server hybrid frontend
**Repository:** C:\Users\Admin\source\repos\RelevanceRo\RLVStack
**Current Branch:** main

---

## Architecture

### Backend
- **Framework:** .NET 9 / C# 13
- **Architecture:** Vertical Slice Architecture (VSA)
- **API:** ASP.NET Core Minimal APIs
- **Database:** PostgreSQL/MySQL with EF Core
- **Features:** Multi-tenancy, JWT authentication, background jobs (TickerQ)
- **Observability:** OpenTelemetry via Aspire

### Frontend (UI)
- **Framework:** Blazor WebAssembly + Server (Hybrid)
- **Current UI Library:** Radzen Blazor Components (being migrated)
- **Target UI Library:** DaisyUI 5 + Tailwind CSS v4
- **API Client:** Microsoft Kiota (type-safe, auto-generated)
- **State Management:** LocalStorageService + LayoutService
- **Authentication:** Permission-based with JWT tokens

### Infrastructure
- **Orchestration:** .NET Aspire
- **Caching:** Redis
- **Real-time:** SignalR
- **Background Jobs:** TickerQ

---

## Coding Practices & Guidelines

### Primary References

#### 1. Copilot Instructions (Comprehensive Guide)
**Location:** [.github/copilot-instructions.md](./.github/copilot-instructions.md)

**Key Sections:**
- Solution structure and organization
- Vertical Slice Architecture (VSA) patterns
- Backend feature development playbook
- UI component development guidelines
- Naming conventions
- Permission-based authorization
- Code-behind pattern for Blazor components
- API response patterns

#### 2. Git Commit Instructions (Commit Message Standards)
**Location:** [.github/git-commit-instructions.md](./.github/git-commit-instructions.md)

**Key Standards:**
- Use all lowercase for commit messages
- Write in imperative mood (e.g., "add feature", not "added feature")
- Keep subject under 50 characters
- Use conventional commits format: `<emoji> <type>(<scope>): <description>`
- Include body explaining "what" and "why"
- Use Gitmoji for visual categorization
- Always end with `###` line for issue tracking

**Example Commit:**
```
✨ feat(ui): add daisyui button component

implement reusable button wrapper for daisyui following code-behind pattern. supports all button variants, sizes, and colors. maintains consistency with existing component architecture.

- add DButton.razor with markup
- add DButton.razor.cs with code-behind
- support primary, secondary, accent color variants
- add size options (xs, sm, md, lg, xl)

### Refs #123
```

---

## Current Migration Context

### Active Task: DaisyUI Migration
**Status:** In Progress (STEP 0)
**Tracking Document:** [FEATURE_PROGRESS.md](./FEATURE_PROGRESS.md)

**Goal:** Migrate entire UI from Radzen Blazor components to DaisyUI (Tailwind CSS v4 plugin)

**Custom DaisyUI Theme:**
```css
@plugin "daisyui/theme" {
  name: "light";
  default: true;
  prefersdark: true;
  color-scheme: "dark";
  --color-base-100: oklch(100% 0 0);
  --color-base-200: oklch(76% 0.188 70.08);
  --color-base-300: oklch(95% 0 0);
  --color-base-content: oklch(21% 0.006 285.885);
  --color-primary: oklch(55% 0.195 38.402);
  --color-primary-content: oklch(98% 0.003 247.858);
  --color-secondary: oklch(47% 0.114 61.907);
  --color-secondary-content: oklch(94% 0.028 342.258);
  --color-accent: oklch(44% 0.011 73.639);
  --color-accent-content: oklch(96% 0.007 247.896);
  --color-neutral: oklch(70% 0.022 261.325);
  --color-neutral-content: oklch(98% 0.002 247.839);
  --color-info: oklch(74% 0.16 232.661);
  --color-info-content: oklch(92% 0.013 255.508);
  --color-success: oklch(76% 0.177 163.223);
  --color-success-content: oklch(96% 0.003 264.542);
  --color-warning: oklch(82% 0.189 84.429);
  --color-warning-content: oklch(96% 0.001 286.375);
  --color-error: oklch(57% 0.245 27.325);
  --color-error-content: oklch(96% 0.007 247.896);
  --radius-selector: 0.5rem;
  --radius-field: 1rem;
  --radius-box: 2rem;
  --size-selector: 0.1875rem;
  --size-field: 0.1875rem;
  --border: 0.5px;
  --depth: 1;
  --noise: 1;
}
```

**Migration Approach:**
- Step-by-step refactoring with approval checkpoints
- Maintain all existing functionality
- Preserve permission-based authorization
- Keep code-behind pattern
- Follow VSA principles

---

## Key Conventions

### Backend Conventions

#### File Structure
```
Features/<Feature>/
  ├── <Operation>.cs           – Single file per operation
  │   ├── Request class
  │   ├── Handler class (implements IScopedHandler)
  │   ├── Validator class (FluentValidation)
  │   └── Route class (implements IRouteRegistrar)
  └── _Shared/                 – Shared feature code
      ├── <Entity>.cs          – Entity/domain model
      ├── I<Service>.cs        – Service interface
      └── <Service>.cs         – Service implementation
```

#### Response Pattern (MANDATORY)
- **ALL handlers** return `Response<T>` or `Response`
- Success: `Response<T>.Success(data)`
- Failure: `Response<T>.Failure("message", statusCode)`

### Frontend Conventions

#### File Structure
```
Features/<Feature>/
  ├── <Feature>s.razor         – List/grid page (markup)
  ├── <Feature>s.razor.cs      – List page code-behind
  ├── CreateOrUpdate<Feature>.razor – Form dialog (markup)
  ├── CreateOrUpdate<Feature>.razor.cs – Form code-behind
  └── _Shared/                 – Shared feature components
      └── <Component>.razor[.cs]
```

#### Naming Conventions
- **Components:** PascalCase (e.g., `Users.razor`, `CreateOrUpdateUser.razor`)
- **Code-Behind:** Match component name exactly (e.g., `Users.razor.cs`)
- **Routes:** Lowercase kebab-case (`/users`, `/roles`, `/tenants`)
- **Constants:** Define in `KrafterRoute.cs`

#### Blazor Component Patterns
- Always use code-behind pattern (`.razor` + `.razor.cs`)
- Inject services via primary constructors in code-behind
- Use `[Parameter]` for component parameters
- Permission checks: `@attribute [MustHavePermission(action, resource)]`
- Use `AuthorizeView` for conditional rendering

---

## Project Structure

### Solution Layout
```
C:\Users\Admin\source\repos\RelevanceRo\RLVStack\
├── .github/
│   ├── copilot-instructions.md      – Coding standards
│   └── git-commit-instructions.md   – Git standards
├── aspire/
│   ├── Krafter.Aspire.AppHost/
│   └── Krafter.Aspire.ServiceDefaults/
├── src/
│   ├── Backend/                     – ASP.NET Core API (VSA)
│   └── UI/
│       ├── Krafter.UI.Web/          – Blazor Server host
│       └── Krafter.UI.Web.Client/   – Blazor WebAssembly
│           ├── Features/            – Feature-based organization
│           ├── Common/              – Shared utilities
│           ├── Infrastructure/      – Infrastructure services
│           └── Docs/                – DaisyUI documentation
├── FEATURE_PROGRESS.md              – Migration tracking
└── claude.md                        – This file
```

### UI Project Structure (Krafter.UI.Web.Client)
```
Krafter.UI.Web.Client/
├── Features/
│   ├── Auth/
│   │   ├── Login.razor              – Login page
│   │   └── _Shared/
│   │       └── AuthenticationService.cs
│   ├── Users/
│   │   ├── Users.razor              – User list
│   │   ├── CreateOrUpdateUser.razor – User form
│   │   └── _Shared/                 – User-specific components
│   ├── Roles/
│   ├── Tenants/
│   └── ...
├── Common/
│   ├── Components/
│   │   ├── Layout/
│   │   │   ├── MainLayout.razor
│   │   │   └── ...
│   │   ├── Dialogs/
│   │   ├── Forms/
│   │   └── DaisyUI/                 – DaisyUI wrappers (NEW)
│   ├── Constants/
│   ├── Permissions/
│   └── ...
├── Infrastructure/
│   ├── Auth/
│   ├── Api/
│   ├── Services/
│   └── ...
└── Docs/
    ├── daisyUILLMs.txt              – DaisyUI component reference
    └── DaisyUITailwind.txt          – DaisyUI setup guide
```

---

## Important Files & Locations

### Configuration Files
- **Project Files:** `*.csproj`
- **App Settings:** `appsettings.json`
- **User Secrets:** `dotnet user-secrets` (dev)

### Key UI Files
- **Main Entry:** `Program.cs`
- **Root Component:** `Components/App.razor`
- **Global Imports:** `_Imports.razor`
- **Main Layout:** `Common/Components/Layout/MainLayout.razor`
- **Routes:** `Routes.razor`

### Documentation Files
- **Coding Practices:** `.github/copilot-instructions.md`
- **Git Standards:** `.github/git-commit-instructions.md`
- **Migration Progress:** `FEATURE_PROGRESS.md`
- **DaisyUI Reference:** `src/UI/Krafter.UI.Web.Client/Docs/daisyUILLMs.txt`

---

## Development Workflow

### Adding a New Feature

#### Backend (VSA)
1. Create feature folder: `Features/<Feature>/`
2. Create operation files: `Create<Feature>.cs`, `Get<Feature>s.cs`, etc.
3. Add `_Shared/` for entities and services
4. Update `KrafterContext.cs` with DbSet
5. Add EF configuration
6. Run migration: `dotnet ef migrations add Add<Feature>`
7. Update `KrafterPermissions.cs`

#### Frontend (Blazor)
1. Create feature folder: `Features/<Feature>/`
2. Create list page: `<Feature>s.razor` + `.razor.cs`
3. Create form dialog: `CreateOrUpdate<Feature>.razor` + `.razor.cs`
4. Add route constant to `KrafterRoute.cs`
5. Update `KrafterPermissions.cs` (mirror backend)
6. Update `MenuService.cs` for navigation
7. Regenerate Kiota client if needed: `kiota update`

### Commit Workflow
1. Stage changes: `git add .`
2. Commit with proper format (see git-commit-instructions.md)
3. Example: `✨ feat(ui): add product management page`
4. Include body explaining changes
5. End with `### Refs #<issue>`

---

## Resources

### Documentation
- [DaisyUI v5 Docs](https://daisyui.com)
- [Tailwind CSS v4 Docs](https://tailwindcss.com)
- [Blazor Documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [Vertical Slice Architecture](https://www.jimmybogard.com/vertical-slice-architecture/)

### Internal References
- [Copilot Instructions](./.github/copilot-instructions.md)
- [Git Commit Instructions](./.github/git-commit-instructions.md)
- [Feature Progress](./FEATURE_PROGRESS.md)
- [DaisyUI LLM Guide](./src/UI/Krafter.UI.Web.Client/Docs/daisyUILLMs.txt)

---

## AI Assistant Guidelines

### When Working on This Project

1. **Always Check First:**
   - Read `copilot-instructions.md` for coding standards
   - Review `FEATURE_PROGRESS.md` for migration status
   - Check `git-commit-instructions.md` for commit format

2. **Follow Patterns:**
   - Backend: Vertical Slice Architecture
   - Frontend: Code-behind pattern
   - Use existing features as templates (Users, Roles, Tenants)

3. **Migration Workflow:**
   - Update `FEATURE_PROGRESS.md` after each step
   - Wait for user approval before proceeding
   - Maintain all existing functionality
   - Test thoroughly after each change

4. **Commit Standards:**
   - Use lowercase
   - Imperative mood
   - Include emoji
   - Meaningful body
   - End with `###`

5. **Code Quality:**
   - No emojis in code (unless user requests)
   - Follow C# 13 and .NET 9 standards
   - Nullable enabled
   - File-scoped namespaces
   - Primary constructors for DI

---

## Quick Commands

### Build & Run
```bash
# Run with Aspire
dotnet run --project aspire/Krafter.Aspire.AppHost/Krafter.Aspire.AppHost.csproj

# Run Backend only
dotnet run --project src/Backend/Backend.csproj

# Run UI only
dotnet run --project src/UI/Krafter.UI.Web/Krafter.UI.Web.csproj
```

### Database Migrations
```bash
# Add migration
dotnet ef migrations add <MigrationName> --project src/Backend --context KrafterContext

# Update database
dotnet ef database update --project src/Backend --context KrafterContext
```

### Kiota Client Update
```bash
cd src/UI/Krafter.UI.Web.Client
kiota update
```

---

**Last Updated:** 2025-10-14
**Maintained By:** Claude AI Assistant
**Project Status:** Active Development - DaisyUI Migration Phase
