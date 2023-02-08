using Boilerplate.Domain.Entities.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Domain.Implementations;
public interface IMailService
{
    Task<bool> CreateEmailMessage<TModel>(MailData mailData,TModel mode, CancellationToken ct);
}
