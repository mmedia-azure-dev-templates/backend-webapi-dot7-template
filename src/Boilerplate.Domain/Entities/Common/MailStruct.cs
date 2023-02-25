using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

//https://blog.christian-schou.dk/send-emails-with-asp-net-core-with-mailkit

namespace Boilerplate.Domain.Entities.Common;

public class MailStruct
{
    public string DisplayName { get; }
    public string Subject { get; }
    //public string Body { get; }
    public string Template { get; set; }
    public List<string> To { get; }
    public List<string>? Bcc { get; }
    public List<string>? Cc { get; }
    
    // Sender
    public string? ReplyTo { get; }
    public string? ReplyToName { get; }
    public string? From { get; }
    public IFormFileCollection? Attachments { get; set; }
    public object? Data { get; set; }

    public MailStruct(
        string from, 
        string displayName, 
        List<string> to, 
        string subject= "", 
        //string body = "",
        string template = "",
        List<string>? bcc = null,
        List<string>? cc = null,
        string? replyTo = null,
        string? replyToName = null,
        IFormFileCollection? attachments = null,
        object? data=null)
    {
        From = from;
        DisplayName = displayName;
        
        // Receiver
        To = to;
        Bcc = bcc ?? new List<string>();
        Cc = cc ?? new List<string>();
        // Sender
        ReplyTo = replyTo;
        ReplyToName = replyToName;

        // Content
        Subject = subject;
        //Body = body;
        Attachments = attachments;
        Template = template;
        Data = data ?? new object();
    }
}