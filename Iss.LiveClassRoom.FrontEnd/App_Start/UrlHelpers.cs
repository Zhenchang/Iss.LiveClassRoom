using Iss.LiveClassRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Iss.LiveClassRoom.FrontEnd.App_Start {

    public static class AuthorizationManager {
        public static void CheckAuthorization(this IEntity entity, Permissions permission) {
            if (!entity.HasAccess(HttpContext.Current.User, permission))
                throw new AuthorizationException();
        }
    }


    public class AuthorizationException : Exception {

    }

    public static class UrlHelperExtensions {


        public static IHtmlString RenderGridEnumFilter(this HtmlHelper helper, string gridName, string filterWidgetName, Type enumType) {
            var opts = new StringBuilder();
            foreach (var v in Enum.GetValues(enumType)) {
                string val = v.ToString();
                opts.AppendFormat(@"sel.append('<option ' + (""{0}"" == v ? 'selected=""selected""' : '') + ' value=""{0}"">{1}</option>');", val, val);
            }
            var script = string.Format(@"
<script type=""text/javascript"">
function {0}() {{
    this.getAssociatedTypes = function () {{ return ['{0}']; }};
    this.onShow = function () {{ }};
    this.showClearFilterButton = function () {{ return true; }};
    this.onRender = function (container, lang, typeName, values, cb, data) {{
        var _this = this;
        var _cb = cb;
        container.append('<select class=""grid-filter-type {0}List form-control""></select>');
        var sel = container.find('.{0}List');
        var v = (values.length > 0 ? values[0] : {{ filterType: 1, filterValue: '' }}).filterValue;
        {1}
        sel.change(function () {{ _cb([{{ filterValue: $(this).val(), filterType: 1 }}]); }});
    }};
}}
$(function () {{
    pageGrids.{2}.addFilterWidget(new {0}());
}});
</script>
        ", filterWidgetName, opts, gridName);
            return helper.Raw(script);
        }



        public static HtmlString RouteValueChange(this UrlHelper url, object routeValues = null) {
            //// Convert new route values to a dictionary
            //RouteValueDictionary newRoute = new RouteValueDictionary(routeValues);

            //// Get the route data of the current Url
            //var current = url.RequestContext.RouteData.Values;

            //var currentQueryString = url.RequestContext.HttpContext.Request.QueryString.Keys;
            //// Merge the new values INTO the current values, overwriting any existing values/querystrings
            //foreach (var item in newRoute)
            //    current[item.Key] = item.Value;

            //foreach (var item in currentQueryString)
            //    current[item] = item.Value;

            

            RouteValueDictionary combinedRouteValues = new RouteValueDictionary(url.RequestContext.RouteData.Values);

            NameValueCollection queryString = url.RequestContext.HttpContext.Request.QueryString;
            foreach (string key in queryString.AllKeys.Where(key => key != null))
                combinedRouteValues[key] = queryString[key];

            if (routeValues != null) {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(routeValues))
                    combinedRouteValues[descriptor.Name] = descriptor.GetValue(routeValues);
            }

            // Generate the new Url
            var newUrl = url.RouteUrl(combinedRouteValues);
            return new HtmlString(newUrl);
        }
    }
}