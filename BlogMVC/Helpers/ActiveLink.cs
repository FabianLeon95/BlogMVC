using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMVC.Helpers
{
    public static class ActiveLink
    {
        public static string IsActive(this HtmlHelper html, string control, string action)
        {
            var routeData = html.ViewContext.RouteData;

            var routeAction = (string)routeData.Values["action"];
            var routeControl = (string)routeData.Values["controller"];

            // both must match
            var returnActive = control == routeControl &&
                               action == routeAction;

            return returnActive ? "active" : "";
        }

        public static string IsActive(this HtmlHelper html, string controller)
        {
            var routeData = html.ViewContext.RouteData;

            var routeControl = (string)routeData.Values["controller"];

            // both must match
            var returnActive = controller == routeControl;

            return returnActive ? "active" : "";
        }
    }
}