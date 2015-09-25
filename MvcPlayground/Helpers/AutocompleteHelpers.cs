using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace MvcPlayground.Helpers
{
    public static class AutocompleteHelpers
    {
        private const string AutoCompleteControllerKey = "AutoCompleteController";
        private const string AutoCompleteActionKey = "AutoCompleteAction";
        
        public static MvcHtmlString AutocompleteFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string actionName, string controllerName)
        {
            string autocompleteUrl = UrlHelper.GenerateUrl(null, actionName, controllerName,
                                                           null,
                                                           html.RouteCollection,
                                                           html.ViewContext.RequestContext,
                                                           includeImplicitMvcValues: true);

            // Generate <input type="text" id="somevalue" name="somevalue" data-autocomplete-url="@Url.Action("AutoComplete")"/>
            return html.TextBoxFor(expression, new { data_autocomplete_url = autocompleteUrl });
        }

        // Extension method to help setting the autocomplete values in the metadata
        // Referenced by AutoCompleteAttribute
        public static void SetAutoComplete(this ModelMetadata metadata, string controller, string action)
        {
            metadata.AdditionalValues[AutoCompleteControllerKey] = controller;
            metadata.AdditionalValues[AutoCompleteActionKey] = action;
        }

        // Referenced by EditorTemplates\String.cshtml
        public static string GetAutoCompleteUrl(this HtmlHelper html, ModelMetadata metadata)
        {
            string controller = metadata.AdditionalValues.GetString(AutoCompleteControllerKey);
            string action = metadata.AdditionalValues.GetString(AutoCompleteActionKey);
            if (string.IsNullOrEmpty(controller)
                || string.IsNullOrEmpty(action))
            {
                return null;
            }
            return UrlHelper.GenerateUrl(null, action, controller, null, html.RouteCollection, html.ViewContext.RequestContext, true);
        }

        private static string GetString(this IDictionary<string, object> dictionary, string key)
        {
            object value;
            dictionary.TryGetValue(key, out value);
            return (string)value;
        }
    }
}