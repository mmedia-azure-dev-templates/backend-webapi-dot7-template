using AuthPermissions.AdminCode;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Users.CreateUser;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Users.AvailableUserDocument;
public class AvailableUserDocumentHandler : IRequestHandler<AvailableUserNdocumentRequest, AvailableUserNdocumentResponse>
{
    private readonly IContext _context;
    public AvailableUserDocumentHandler(IContext context)
    {
        _context = context;
    }

    public async Task<AvailableUserNdocumentResponse> Handle(AvailableUserNdocumentRequest request, CancellationToken cancellationToken)
    {
        AvailableUserNdocumentResponse availableUserResponse = new AvailableUserNdocumentResponse();
        availableUserResponse.IsAvailable = true;

        var userNdocument = await _context.UserInformations.Where(x => x.Ndocument == request.Ndocument).FirstOrDefaultAsync();
        if (userNdocument != null)
        {
            availableUserResponse.IsAvailable = false;
        }

        return availableUserResponse;
    }
}
