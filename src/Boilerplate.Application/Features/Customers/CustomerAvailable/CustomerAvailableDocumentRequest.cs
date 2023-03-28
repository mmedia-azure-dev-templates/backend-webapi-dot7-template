using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.Graph;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Users.AvailableUserDocument;
public record CustomerAvailableDocumentRequest(string Ndocument) : IRequest<CustomerAvailableDocumentResponse>;