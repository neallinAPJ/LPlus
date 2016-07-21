using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Model.Menu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;

namespace LPlus.HtmlHelper
{
    public static class SiteMenu
    {
        //public static HttpContext _hcontext { get; set; }
        public static HtmlString Menu(this IHtmlHelper htmlHelper, List<MenuModel> siteMenus, IHttpContextAccessor _hcontext)
        {
            if (_hcontext == null)
            {
                throw new ArgumentNullException(nameof(_hcontext));
            }
            var routeData = _hcontext.HttpContext.GetRouteData();
            string Area = routeData.Values != null && routeData.Values["Area"] != null ? routeData.Values["Area"].ToString() : string.Empty;
            string Controller = routeData.Values != null && routeData.Values["Controller"] != null ? routeData.Values["Controller"].ToString() : "home";
            string Action = routeData.Values != null && routeData.Values["Action"] != null ? routeData.Values["Action"].ToString() : "index";
            TagBuilder divTag = new TagBuilder("div");
            if (siteMenus != null)
            {
                foreach (MenuModel p in siteMenus)
                {
                    divTag.InnerHtml.AppendHtml(GetTagLI(htmlHelper, p, Area, Controller, Action, 0, p.ResourceKey));
                }
            }
            var writer = new StringWriter();
            divTag.InnerHtml.WriteTo(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }

        private static TagBuilder GetTagLI(IHtmlHelper htmlHelper, MenuModel menu, string area, string controller, string action, int menuLevel, string resourceKey)//, int companyId
        {
            TagBuilder LiTag = new TagBuilder("li");
            LiTag.AddCssClass("nav-parent");
            StringBuilder LiInnerHtml = new StringBuilder(200);

            TagBuilder aTag = new TagBuilder("a");
            if (!string.IsNullOrEmpty(menu.Controller) && !string.IsNullOrEmpty(menu.Action))
            {
                aTag.MergeAttribute("href", (string.IsNullOrEmpty(menu.Area) ? string.Empty : ("/" + menu.Area)) + "/" + menu.Controller + "/" + menu.Action);
            }
            TagBuilder iTag1 = new TagBuilder("i");
            iTag1.AddCssClass(string.Format("fa {0}", menu.MenuIcon));
            TagBuilder spanTag = new TagBuilder("span");
            spanTag.InnerHtml.Append(resourceKey);
            //TagBuilder iTag2 = new TagBuilder("i");
            //if (menu.ChildrenMenu != null && menu.ChildrenMenu.Count > 0)
            //{
            //    iTag2.AddCssClass("fa fa-angle-left pull-right");
            //}
            aTag.InnerHtml.AppendHtml(iTag1);
            aTag.InnerHtml.AppendHtml(spanTag);
            //aTag.InnerHtml.AppendHtml(iTag2);
            LiTag.InnerHtml.AppendHtml(aTag);
            if (menu.ChildrenMenu != null && menu.ChildrenMenu.Count > 0)
            {
                LiTag.InnerHtml.AppendHtml(GetTagUL(htmlHelper, menu.ChildrenMenu, area, controller, action, menuLevel, resourceKey));

            }
            string activeCSS = string.Empty;
            if ((menu.Area.Equals(area, StringComparison.CurrentCultureIgnoreCase))
                && (menu.Controller.Equals(controller, StringComparison.CurrentCultureIgnoreCase))
                && (menu.Action.Equals(action, StringComparison.CurrentCultureIgnoreCase)) ||
                (menu.ChildrenMenu != null && menu.ChildrenMenu.Count > 0 && menu.ChildrenMenu.Any(i => i.IsActive == true)))
            {
                activeCSS = "active";
                menu.IsActive = true;
            }
            LiTag.AddCssClass(activeCSS);
            return LiTag;
        }
        private static TagBuilder GetTagUL(IHtmlHelper htmlHelper, List<MenuModel> menus, string area, string controller, string action, int menuLevel, string resourceKey)
        {
            string UlHtml = string.Empty;
            if (menus != null)
            {
                TagBuilder UlTag = new TagBuilder("ul");
                StringBuilder innerHtml = new StringBuilder(200);
                UlTag.AddCssClass("children");

                foreach (MenuModel p in menus)
                {
                    UlTag.InnerHtml.AppendHtml(GetTagLI(htmlHelper, p, area, controller, action, menuLevel, p.ResourceKey));
                }
                return UlTag;
            }

            return null;
        }
    }
}
