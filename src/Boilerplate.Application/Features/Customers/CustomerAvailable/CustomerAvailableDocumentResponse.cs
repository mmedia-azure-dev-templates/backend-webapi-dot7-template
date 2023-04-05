using Boilerplate.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Customers.CustomerAvailable;
public class CustomerAvailableDocumentResponse
{
    public bool IsAvailable { get; set; }
    public CustomerId? CustomerId { get; set; } = null;
}
