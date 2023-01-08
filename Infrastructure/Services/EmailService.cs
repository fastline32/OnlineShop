using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private IOptions<SMTPConfigModel> _smtpConfig;
        
        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Test email",userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("TestEmail"),userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }
        
        public async Task SendEmailConfirmationEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Hello {{UserName}}, confirm your email",userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("EmailConfirm"),userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }

        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig;
        }
        private const string templatePath= @"EmailTemplate/{0}.html";
        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage 
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.Value.SenderAddress,_smtpConfig.Value.SenderDisplayName),
                IsBodyHtml = _smtpConfig.Value.IsBodyHTML,

            };

            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.Value.Username,_smtpConfig.Value.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Value.Host,
                Port = _smtpConfig.Value.Port,
                EnableSsl = _smtpConfig.Value.EnableSSL,
                UseDefaultCredentials = _smtpConfig.Value.UseDefaultCredentials,
                Credentials = networkCredential,
            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);
        }

        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath,templateName));
            return body;
        }

        private string UpdatePlaceHolders(string text, List<KeyValuePair<string,string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeHolder in keyValuePairs)
                {
                    if (text.Contains(placeHolder.Key))
                    {
                        text = text.Replace(placeHolder.Key, placeHolder.Value);
                    }
                }
            }
            return text;
        }
    }
}