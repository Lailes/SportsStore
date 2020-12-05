using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models;

namespace SportsStore.Infrastructure {
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper: TagHelper {

        private readonly IUrlHelperFactory _urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory) {
            _urlHelperFactory = urlHelperFactory;
        }


        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PagingInfo PageModel { get; set; }

        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            var helper = _urlHelperFactory.GetUrlHelper(ViewContext);

            var result = new TagBuilder("div");

            for (int i = 0; i < PageModel.TotalPages; i++) {
                var tag = new TagBuilder("a");
                tag.Attributes["href"] = helper.Action(PageAction, new {ProductPage = i+1 });
                tag.InnerHtml.Append((i+1).ToString());
                result.InnerHtml.AppendHtml(tag);
            }


            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}