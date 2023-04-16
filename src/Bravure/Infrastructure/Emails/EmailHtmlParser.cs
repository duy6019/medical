using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Bravure.Infrastructure.Emails
{
    public interface IEmailHtmlParser
    {
        string ReplaceTemplateData(string html, IDictionary<string, string> templateData);
    }

    public class EmailHtmlParser : IEmailHtmlParser
    {
        private readonly BravureOptions _bravureOptions;

        public EmailHtmlParser(IOptions<BravureOptions> bravureOptions)
        {
            _bravureOptions = bravureOptions.Value;
        }

        public string ReplaceTemplateData(string html, IDictionary<string, string> templateData)
        {
            //remove spaces inside brackets for consistency
            html = html.Replace("{{ ", "{{");
            html = html.Replace(" }}", "}}");

            templateData.Add("baseUrl", _bravureOptions.BaseUrl);

            foreach (var data in templateData)
            {
                var match = "{{" + data.Key + "}}";
                html = html.Replace(match, data.Value);
            }

            return html;
        }
    }
}
