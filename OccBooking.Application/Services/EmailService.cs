using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using MailKit.Net.Smtp;
using MimeKit;
using OccBooking.Common.Infrastructure;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Application.Services
{
    public class EmailService : IEmailService
    {
        private AppSettings _appSettings;

        public EmailService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void Send(string content, Client receiver)
        {
            if (_appSettings.SendEmails)
            {
                MimeMessage message = new MimeMessage();
                MailboxAddress from = new MailboxAddress(_appSettings.EmailName, _appSettings.EmailAddress);
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress(receiver.Name.FullName, receiver.Email.Value);
                message.To.Add(to);
                message.Subject = $"Rezerwacja OccBooking - {receiver.Name.FullName}";

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = content;

                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                client.CheckCertificateRevocation = false;

                client.Connect(_appSettings.SmtpHost, _appSettings.SmtpPort);
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(_appSettings.EmailAddress, _appSettings.EmailPassword);

                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}