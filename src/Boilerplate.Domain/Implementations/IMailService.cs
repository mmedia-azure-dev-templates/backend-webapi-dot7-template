using Boilerplate.Domain.Entities.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Domain.Implementations;
public interface IMailService
{
    Task<bool> SendAsync(MailData mailData, CancellationToken ct);
    Task<bool> SendWithAttachmentsAsync(MailDataWithAttachments mailData, CancellationToken ct);
    string GetEmailTemplate<T>(string emailTemplate, T emailTemplateModel);
}
