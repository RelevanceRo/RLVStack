using Krafter.UI.Web.Client.Models;

namespace Krafter.UI.Web.Client.Common.Components.Layout;

public partial class NavigationItem(NavigationManager navigationManager)
{
     [Parameter]
     public Menu Example { get; set; }

     [Parameter]
     public RenderFragment ChildContent { get; set; }
     [Parameter]
     public EventCallback<bool> ExpandedChanged { get; set; }

     [Parameter]
     public bool Expanded
     {
          get
          {
               return Example.Expanded;
          }
          set
          {
               Example.Expanded = value;
          }
     }

     string GetUrl()
     {
        //  return Example.Path == null ? Example.Path : $"{Example.Path}{new Uri(navigationManager.Uri).Query}";
        return  Example.Path;
     }

     string GetItemClass()
     {
          // Check if current URL matches this menu item's path
          if (!string.IsNullOrEmpty(Example.Path))
          {
               var currentPath = new Uri(navigationManager.Uri).LocalPath;
               if (currentPath.Equals(Example.Path, StringComparison.OrdinalIgnoreCase))
               {
                    return "menu-active bg-base-200";
               }
          }
          return string.Empty;
     }
}
