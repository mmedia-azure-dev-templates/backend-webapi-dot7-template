﻿using Microsoft.AspNetCore.Http;
using Microsoft.Graph;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities.Common;
public class MailDataWithAttachments
{
    // Receiver
    public List<string> To { get; set; } = new();
    public List<string> Bcc { get; set; } = new();
    public List<string> Cc { get; set; } = new();

    // Sender
    public string? From { get; set; }
    public string? DisplayName { get; set; }
    public string? ReplyTo { get; set; }
    public string? ReplyToName { get; set; }

    // Content
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public IFormFileCollection? Attachments { get; set; }
}
