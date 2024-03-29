﻿using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Teams.Create;

public class CreateTeamHandler : IRequestHandler<CreateTeamRequest, CreateTeamResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateTeamHandler> _logger;
    private readonly IMailService _mail;
    private readonly UserManager<ApplicationUser> _userManager;


    public CreateTeamHandler(IContext context, IMapper mapper, ILogger<CreateTeamHandler> logger, IMailService mail, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
        _mail = mail;
        _userManager = userManager;
    }
    public async Task<CreateTeamResponse> Handle(CreateTeamRequest request, CancellationToken cancellationToken)
    {
        CreateTeamResponse userResponse = new CreateTeamResponse();
        return await Task.Run(() => userResponse); 

        //using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //{
        //    try
        //    {
                
        //        scope.Complete();
        //        userResponse.Transaction = true;
        //        return userResponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogInformation(3, ex.Message);
        //        userResponse.Message = ex.Message;
        //        return userResponse;
        //    }
        //}
    }
}