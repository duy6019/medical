using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Bravure.Infrastructure.Emails
{
    public class DummyEmailSender : IEmailSender, ITemplateEmailSender
    {
        private readonly ILogger<DummyEmailSender> _logger;
        private readonly IEmailHtmlParser _emailHtmlParser;

        public DummyEmailSender(
            ILogger<DummyEmailSender> logger,
            IEmailHtmlParser emailHtmlParser)
        {
            _logger = logger;
            _emailHtmlParser = emailHtmlParser;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _logger.LogInformation(new
            {
                Name = "Send Email Async",
                Email = email,
                Subject = subject,
                Message = htmlMessage
            }.ToString());
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage, IDictionary<string, string> templateData)
        {
            _logger.LogInformation(new
            {
                Name = "Send Single Template Email",
                HtmlMessage = htmlMessage,
                OutputHtml = _emailHtmlParser.ReplaceTemplateData(htmlMessage, templateData),
                Recipient = email,
                TemplateData = JsonConvert.SerializeObject(templateData)
            }.ToString());
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    }
}
