using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AppMuseo.Helpers
{
    public static class HtmlAttributesHelper
    {
        public static IHtmlContent HtmlAttributes(this IHtmlHelper helper, object htmlAttributes)
        {
            var attributes = Microsoft.AspNetCore.Mvc.ViewFeatures.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            var result = new System.Text.StringBuilder();
            
            foreach (var attr in attributes)
            {
                result.Append($"{attr.Key}=\"{attr.Value}\" ");
            }
            
            return new HtmlString(result.ToString().TrimEnd());
        }
        
        public static IHtmlContent HtmlAttributes(this IHtmlHelper helper, IDictionary<string, object> htmlAttributes)
        {
            var result = new System.Text.StringBuilder();
            
            foreach (var attr in htmlAttributes)
            {
                result.Append($"{attr.Key}=\"{attr.Value}\" ");
            }
            
            return new HtmlString(result.ToString().TrimEnd());
        }
    }
}
