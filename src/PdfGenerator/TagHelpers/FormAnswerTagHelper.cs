using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bravure.TagHelpers
{
    public class FormAnswerTagHelper : AnchorTagHelper
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;

        public FormAnswerTagHelper(IHtmlGenerator generator) : base(generator) { }
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            output.Content.AppendHtml("<strong>Question: </strong>");
            output.Content.Append(Question);
            output.Content.AppendHtml("<br>");
            output.Content.AppendHtml("<strong>Answer: </strong>");
            output.Content.Append(Answer);
            output.Content.AppendHtml("<br>");
            output.Content.AppendHtml("<strong>Comment: </strong>");
            output.Content.Append(Comments);
        }
    }
}
