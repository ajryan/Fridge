using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Fridge.Helpers
{
    public class NgContext<TScope> : IDisposable
    {
        private readonly string _tagName;
        public HtmlHelper<TScope> Html { get; private set; }

        public NgContext(HtmlHelper outerHtml, string tagName, string scopeName)
        {
            Html = new HtmlHelper<TScope>(outerHtml.ViewContext, outerHtml.ViewDataContainer);
            _tagName = tagName;
            outerHtml.SetScopeName(scopeName);
        }

        public void WriteStartTag(object htmlAttributes, object additionalHtmlAttributes = null)
        {
            var tagBuilder = new TagBuilder(_tagName);

            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            if (additionalHtmlAttributes != null)
                tagBuilder.MergeAttributes(new RouteValueDictionary(additionalHtmlAttributes));

            Html.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Html.ViewContext.Writer.Write("</{0}>", _tagName);
            Html.SetScopeName(null);
        }
    }
}