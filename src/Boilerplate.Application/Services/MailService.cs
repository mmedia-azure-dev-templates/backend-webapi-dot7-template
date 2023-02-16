using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Services;

public class MailService : IMailService
{
    private readonly MailSettings _settings;
    private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

    public MailService(IOptions<MailSettings> settings, IRazorViewToStringRenderer razorViewToStringRenderer)
    {
        _settings = settings.Value;
        _razorViewToStringRenderer = razorViewToStringRenderer;
    }

    public async Task<bool> SendWithAttachmentsAsync(MailDataWithAttachments mailData, CancellationToken ct = default)
    {
        try
        {
            // Initialize a new instance of the MimeKit.MimeMessage class
            var mail = new MimeMessage();

            #region Sender / Receiver
            // Sender
            mail.From.Add(new MailboxAddress(_settings.DisplayName, mailData.From ?? _settings.From));
            mail.Sender = new MailboxAddress(mailData.DisplayName ?? _settings.DisplayName, mailData.From ?? _settings.From);

            // Receiver
            foreach (string mailAddress in mailData.To)
                mail.To.Add(MailboxAddress.Parse(mailAddress));

            // Set Reply to if specified in mail data
            if (!string.IsNullOrEmpty(mailData.ReplyTo))
                mail.ReplyTo.Add(new MailboxAddress(mailData.ReplyToName, mailData.ReplyTo));

            // BCC
            // Check if a BCC was supplied in the request
            if (mailData.Bcc != null)
            {
                // Get only addresses where value is not null or with whitespace. x = value of address
                foreach (string mailAddress in mailData.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                    mail.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));
            }

            // CC
            // Check if a CC address was supplied in the request
            if (mailData.Cc != null)
            {
                foreach (string mailAddress in mailData.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
                    mail.Cc.Add(MailboxAddress.Parse(mailAddress.Trim()));
            }
            #endregion

            #region Content

            // Add Content to Mime Message
            var body = new BodyBuilder();
            mail.Subject = mailData.Subject;

            // Check if we got any attachments and add the to the builder for our message
            if (mailData.Attachments != null)
            {
                byte[] attachmentFileByteArray;
                foreach (IFormFile attachment in mailData.Attachments)
                {
                    if (attachment.Length > 0)
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            attachment.CopyTo(memoryStream);
                            attachmentFileByteArray = memoryStream.ToArray();
                        }
                        body.Attachments.Add(attachment.FileName, attachmentFileByteArray, ContentType.Parse(attachment.ContentType));
                    }
                }
            }
            body.HtmlBody = mailData.Body;
            mail.Body = body.ToMessageBody();

            #endregion

            #region Send Mail

            using var smtp = new SmtpClient();

            //if (_settings.UseSSL)
            //{
            //    await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.SslOnConnect, ct);
            //}
            //else if (_settings.UseStartTls)
            //{
            //    await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls, ct);
            //}

            await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls, ct);
            await smtp.AuthenticateAsync(_settings.UserName, _settings.Password, ct);
            await smtp.SendAsync(mail, ct);
            await smtp.DisconnectAsync(true, ct);

            return true;
            #endregion

        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> CreateEmailMessage<TModel>(MailData mailData, TModel model, CancellationToken ct = default)
    {
        try
        {
            MimeMessage message = new MimeMessage();

            //Sender
            message.From.Add(new MailboxAddress(_settings.DisplayName, mailData.From ?? _settings.From));
            message.Sender = new MailboxAddress(mailData.DisplayName ?? _settings.DisplayName, mailData.From ?? _settings.From);
            // Receiver
            foreach (string mailAddress in mailData.To)
                message.To.Add(MailboxAddress.Parse(mailAddress));

            // Set Reply to if specified in mail data
            if (!string.IsNullOrEmpty(mailData.ReplyTo))
                message.ReplyTo.Add(new MailboxAddress(mailData.ReplyToName, mailData.ReplyTo));

            // BCC
            // Check if a BCC was supplied in the request
            // Get only addresses where value is not null or with whitespace. x = value of address
            foreach (string mailAddress in mailData.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                message.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));

            // CC
            // Check if a CC address was supplied in the request
            foreach (string mailAddress in mailData.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
                message.Cc.Add(MailboxAddress.Parse(mailAddress.Trim()));
            

            string body = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/" + mailData.Template + "View.cshtml", model);
            if (string.IsNullOrEmpty(body))
            {
                throw new InvalidOperationException("Body is empty!");
            }

            BodyBuilder bodyBuilder = new BodyBuilder { HtmlBody = string.Format("{0}", body) };
            message.Subject = mailData.Subject;
            if (mailData.Attachments != null && mailData.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in mailData.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            message.Body = bodyBuilder.ToMessageBody();
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls, ct);
            await smtp.AuthenticateAsync(_settings.UserName, _settings.Password, ct);
            await smtp.SendAsync(message, ct);
            await smtp.DisconnectAsync(true, ct);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}