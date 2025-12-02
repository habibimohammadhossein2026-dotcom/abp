using Acme.BookStore.Web.Helpers.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[HtmlTargetElement("render-partials")]
public class RenderPartialsTagHelper : TagHelper, IViewContextAware
{
    private readonly IHtmlHelper _htmlHelper;
    private readonly IViewComponentHelper _vcHelper;

    [ViewContext]
    public ViewContext ViewContext { get; set; }

    public string Page { get; set; }
    public ModelExpression For { get; set; }

    public RenderPartialsTagHelper(
        IHtmlHelper htmlHelper,
        IViewComponentHelper vcHelper)
    {
        _htmlHelper = htmlHelper;
        _vcHelper = vcHelper;
    }

    public void Contextualize(ViewContext context)
    {
        if (_htmlHelper is IViewContextAware ctxHtml)
            ctxHtml.Contextualize(context);
        if (_vcHelper is IViewContextAware ctxVc)
            ctxVc.Contextualize(context);
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        Contextualize(ViewContext);

        var pageModel = For.ModelExplorer.Container.Model;
        var assembly = pageModel.GetType().Assembly; 

        output.TagName = null;

        var partialPageModels = assembly
            .GetTypes()
            .Where(t => typeof(PageModel).IsAssignableFrom(t));

        foreach (var partialModel in partialPageModels)
        {
            var attrs = partialModel
                .GetCustomAttributes(typeof(PartialForAttribute), true)
                .Cast<PartialForAttribute>()
                .Where(a => a.PageNames.Contains(Page));

            foreach (var attr in attrs)
            {
                var vcName = attr.ViewComponentName;
                var result = await _vcHelper.InvokeAsync(vcName, null);
                output.Content.AppendHtml(result);
            }
        }
    }

}
