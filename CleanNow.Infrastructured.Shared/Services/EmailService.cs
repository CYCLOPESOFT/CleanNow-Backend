using CleanNow.Core.Application.Dto;
using CleanNow.Core.Application.Interfaces.Shared;
using CleanNow.Core.Domain.Settings;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Infrastructured.Shared.Service
{
    public class EmailService:IEmailService
    {
        private MailSettings mailSettings { get; }
        public async Task SendEmailAsync(EmailRequest request)
        {
            try
            {
                MimeMessage email = new();
                email.Sender = MailboxAddress.Parse(mailSettings.DisplayName + " <" + mailSettings.EmailFrom + " >"); 
                email.To.Add(MailboxAddress.Parse( request.To));
                email.Subject = request.Subject;
                BodyBuilder builder = new();
                builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();
                using SmtpClient smtpClient = new();
                smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtpClient.Connect(mailSettings.SmtpHost, mailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                smtpClient.Authenticate(mailSettings.SmtpUser, mailSettings.SmtpPass);
                await smtpClient.SendAsync(email);
                smtpClient.Disconnect(true);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
