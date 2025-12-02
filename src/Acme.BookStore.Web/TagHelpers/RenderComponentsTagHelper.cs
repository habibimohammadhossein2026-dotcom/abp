using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[HtmlTargetElement("render-components")]
public class RenderComponentsTagHelper : TagHelper
{
    private readonly IViewComponentHelper _vcHelper;

    [ViewContext]
    public ViewContext ViewContext { get; set; }

    public RenderComponentsTagHelper(IViewComponentHelper vcHelper)
    {
        _vcHelper = vcHelper;
    }

    public ModelExpression For { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (_vcHelper is IViewContextAware aware)
            aware.Contextualize(ViewContext);

        var pageModel = For?.ModelExplorer?.Container?.Model;

        if (pageModel == null)
        {
            output.Content.SetHtmlContent("<div style='color:red'>PageModel is null</div>");
            return;
        }

        var attributes = pageModel.GetType()
            .GetCustomAttributes(typeof(RenderComponentAttribute), true)
            .Cast<RenderComponentAttribute>()
            .ToList();

        output.TagName = null;

        foreach (var attr in attributes)
        {
            var parameters = new Dictionary<string, object>();

            for (int i = 0; i < attr.ParamNames?.Length; i++)
            {
                var prop = pageModel.GetType().GetProperty(attr.ModelProperties[i]);
                var value = prop?.GetValue(pageModel);

                parameters.Add(attr.ParamNames[i], value);
            }
            var result = await _vcHelper.InvokeAsync(attr.ComponentName, parameters);
            output.Content.AppendHtml(result);
        }
    }
}
