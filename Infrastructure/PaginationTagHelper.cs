using BYU_EGYPT_INTEX.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BYU_EGYPT_INTEX.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PaginationTagHelper : TagHelper
    {
        //Dynamic creation of links
        private IUrlHelperFactory uhf;

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? vc { get; set; }

        public PageInfo? PageModel { get; set; }
        public string? PageAction;

        public bool PageClassesEnabled { get; set; } = false;
        public string? PageClass { get; set; }
        public string? PageClassNormal { get; set; }
        public string? PageClassSelected { get; set; }

        public override void Process(TagHelperContext thc, TagHelperOutput tho)
        {
            //Access DOM
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            //Create Final Tag to Output
            TagBuilder final = new TagBuilder("div");


            //Only print pagination buttons if enough pages to paginate
            if (PageModel.TotalPages > 1)
            {
                //Loop through all the needed buttons
                for (int i = 1; i <= PageModel.TotalPages; i++)
                {
                    //Generate HTML for buttons
                    TagBuilder tb = new TagBuilder("a");

                    tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });
                    if (PageClassesEnabled)
                    {
                        tb.AddCssClass(PageClass);
                        tb.AddCssClass(i == PageModel.CurrentPage
                            ? PageClassSelected : PageClassNormal);
                    }
                    tb.InnerHtml.Append(i.ToString());

                    final.InnerHtml.AppendHtml(tb);
                }
                tho.Content.AppendHtml(final.InnerHtml);
            }
        }
    }
}