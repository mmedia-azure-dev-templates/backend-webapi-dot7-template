using Boilerplate.Domain.Entities.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Domain.Implementations;
public interface IMailService
{
    Task<bool> CreateEmailMessage<TModel>(MailStruct mailData,TModel mode, CancellationToken ct);
}
