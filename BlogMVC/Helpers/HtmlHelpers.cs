using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMVC.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString CustomValidationSummary(this HtmlHelper helper, string validationMessage = "")
        {
            string retVal = "";
            if (helper.ViewData.ModelState.IsValid)
                return MvcHtmlString.Create(retVal);

            retVal += "<div class='alert alert-danger shadow-sm' role='alert'>";
            foreach (var key in helper.ViewData.ModelState.Keys)
            {
                foreach (var err in helper.ViewData.ModelState[key].Errors)
                    retVal += $"{helper.Encode(err.ErrorMessage)}<br />";
            }
            retVal += "</div>";
            return MvcHtmlString.Create(retVal);
        }
    }
}