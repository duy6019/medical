using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Bravure.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bravure.Infrastructure.Emails
{
    public interface ITemplateEmailSender : IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage, IDictionary<string, string> templateData);
    }

    public class EmailSender : ITemplateEmailSender
    {
        private readonly BravureOptions _bravureOptions;
        private readonly ILogger<EmailSender> _logger;
        private readonly IEmailHtmlParser _emailHtmlParser;

        public EmailSender(IOptions<BravureOptions> options,
            ILogger<EmailSender> logger,
            IEmailHtmlParser emailHtmlParser)
        {
            _bravureOptions = options.Value;
            _logger = logger;
            _emailHtmlParser = emailHtmlParser;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using var client = new SmtpClient(_bravureOptions.SmtpHost, _bravureOptions.SmtpPort)
            {
                EnableSsl = _bravureOptions.SmtpSSLEnabled
            };

            //if we have username and password, use them
            if (!string.IsNullOrEmpty(_bravureOptions.SmtpUsername) && !string.IsNullOrEmpty(_bravureOptions.SmtpPassword))
            {
                client.Credentials = new NetworkCredential(_bravureOptions.SmtpUsername, _bravureOptions.SmtpPassword);
            }

            try
            {
                await client.SendMailAsync(new MailMessage(_bravureOptions.FromAddress, email, subject, htmlMessage) { IsBodyHtml = true });
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to send email to {email}: {message}", email, e.Message);
                throw new SendEmailException(email, e.Message);
            }
            return;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage, IDictionary<string, string> templateData)
        {
            var outputHtml = _emailHtmlParser.ReplaceTemplateData(htmlMessage, templateData);

            try
            {
                await SendEmailAsync(email, subject, outputHtml);
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to send email to {email}: {message}", email, e.Message);
                throw new SendEmailException(email, e.Message);
            }

        }
    }
}
