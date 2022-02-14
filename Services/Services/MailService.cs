using DatabaseAccess.Entities;
using DatabaseAccess.UnitOfWorks;

using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Utility.Models;

namespace Services.Services
{
    public class MailService
    {
        private readonly MailSetting _mailSettings;
        
        public MailService(IOptions<MailSetting> mailSettings)
        {
            _mailSettings = mailSettings.Value;            
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            MimeMessage email = new MimeMessage();
            email.From.Add(new MailboxAddress("CarWorldSystem", _mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            email.Body = new TextPart("html")
            {
                Text = mailRequest.Body
            };
            var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.AuthenticationMechanisms.Remove("XOAUTH2");
            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, false);
            await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task SendMultiEmailAsync(MailRequests mailRequest)
        {                                  
            List <MimeMessage> emailList = new List<MimeMessage>();
            foreach (var item in mailRequest.ToEmail)
            {
                MimeMessage email = new MimeMessage();
                email.From.Add(new MailboxAddress("CarWorldSystem", _mailSettings.Mail));
                email.To.Add(MailboxAddress.Parse(item));
                email.Subject = mailRequest.Subject;
                email.Body = new TextPart("html")
                {
                    Text = mailRequest.Body
                };
                emailList.Add(email);
            }
            var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.AuthenticationMechanisms.Remove("XOAUTH2");
            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, false);
            await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
            foreach (var email in emailList)
            {
                await smtp.SendAsync(email);
            }
            await smtp.DisconnectAsync(true);
        }
    }
}
