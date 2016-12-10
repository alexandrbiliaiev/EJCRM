using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;

namespace EuroJobsCrm.Services
{
    public class NotificationEmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            MimeMessage emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(subject, "crm@eurojobs.info.pl"));
            emailMessage.To.Add(new MailboxAddress(subject, email));
            emailMessage.Subject = subject;

            BodyBuilder bodyBuilder = new BodyBuilder
            {
                HtmlBody = message
            };

            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (SmtpClient client = new SmtpClient())
            {
                client.Connect("mail.ukraine.com.ua", 25, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("crm@eurojobs.info.pl", "azanezege22");
                await client.SendAsync(emailMessage);
                client.Dispose();
            }
 
        }
    }
}
