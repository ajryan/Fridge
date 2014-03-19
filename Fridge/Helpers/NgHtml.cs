using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Fridge.Helpers
{
    public static class NgHtml
    {
        public static NgContext<TScope> NgRepeat<TModel, TScope>(
            this HtmlHelper<TModel> html,
            string tagName,
            Expression<Func<TModel, ICollection<TScope>>> expression,
            object htmlAttributes = null)
        {
            var memberBody = expression.Body as MemberExpression;
            if (memberBody == null)
                throw new InvalidOperationException("Expression must be a property accessor");

            var context = new NgContext<TScope>(html, tagName, typeof(TScope).Name);
            
            // add ng-repeat attribute to start tag
            var propertyName = memberBody.Member.Name;
            var ngRepeatExpr = String.Format("{0} in {1}", typeof (TScope).Name, propertyName);
            context.WriteStartTag(new { ng_repeat = ngRepeatExpr }, htmlAttributes);
            
            return context;
        }

        public static NgContext<TScope> NgForm<TModel, TScope>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TScope>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var context = new NgContext<TScope>(html, "form", metadata.PropertyName);

            context.WriteStartTag(new {novalidate = true, role="form", name=metadata.PropertyName + "Form"});
            return context;
        }

        public static MvcHtmlString NgLabelFor<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            var labelBuilder = new TagBuilder("label");
            labelBuilder.MergeAttribute("for", NgLiteralString(html, expression));
            labelBuilder.InnerHtml = html.DisplayNameFor(expression).ToString();
            return new MvcHtmlString(labelBuilder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString NgInputFor<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            object htmlAttributes = null,
            string type = null,
            string placeHolder = null)
        {
            var inputBuilder = new TagBuilder("input");
            
            if (htmlAttributes != null)
                inputBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            var id = NgLiteralString(html, expression);
            var attributes = new
            {
                id = id,
                name = id,
                type = type?? "text",
            };
            inputBuilder.MergeAttributes(new RouteValueDictionary(attributes));
            inputBuilder.MergeAttribute("ng-model", id);
            if (!String.IsNullOrEmpty(placeHolder))
            {
                inputBuilder.MergeAttribute("placeholder", placeHolder);
            }

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            if (metadata.IsRequired)
            {
                inputBuilder.MergeAttribute("required", "required");
            }
            if (metadata.IsReadOnly)
            {
                inputBuilder.MergeAttribute("readonly", "readonly");
            }

            var rangeValidators = metadata.GetValidators(html.ViewContext.Controller.ControllerContext).OfType<RangeAttributeAdapter>();
            foreach (var rangeValidator in rangeValidators)
            {
                var rangeRules = rangeValidator.GetClientValidationRules().OfType<ModelClientValidationRangeRule>();

            }
            
            
            return new MvcHtmlString(inputBuilder.ToString(TagRenderMode.StartTag));
        }

        // TODO: accept paramter
        public static MvcHtmlString NgClick<TModel>(
            this HtmlHelper<TModel> html,
            Expression<Action<TModel>> expression)
        {
            var methodBody = expression.Body as MethodCallExpression;
            if (methodBody == null)
                throw new InvalidOperationException("Expression must be a method call.");

            string clickMethod = methodBody.Method.Name;
            return new MvcHtmlString(String.Format("ng-click='{0}()'", clickMethod));
        }

        public static MvcHtmlString NgShow<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            return new MvcHtmlString(String.Format("ng-show='{0}'", NgLiteralString(html, expression)));
        }
        
        public static MvcHtmlString NgBind<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            return new MvcHtmlString(String.Format("ng-bind='{0}'", NgLiteralString(html, expression)));
        }

        public static MvcHtmlString NgExpr<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            return new MvcHtmlString("{{" + NgLiteralString(html, expression) + "}}");
        }

        public static MvcHtmlString NgLiteral<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            return new MvcHtmlString(NgLiteralString(html, expression));
        }

        public static void SetScopeName(this HtmlHelper html, string scopeName)
        {
            if (scopeName == null)
                html.ViewContext.TempData.Remove("scopeName");
            else
                html.ViewContext.TempData["scopeName"] = scopeName;
        }

        public static string GetScopeName(this HtmlHelper html)
        {
            return html.ViewContext.TempData["scopeName"] as String;
        }

        private static string NgLiteralString<TModel, TValue>(HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            string literalExpression = metaData.PropertyName;
            var scopeName = html.GetScopeName();
            if (scopeName != null)
                literalExpression = scopeName + "." + literalExpression;

            return literalExpression;
        }
    }
}