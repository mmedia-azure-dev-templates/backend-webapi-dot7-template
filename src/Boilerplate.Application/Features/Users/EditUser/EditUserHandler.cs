using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Users.EditUser;

public class EditUserHandler : IRequestHandler<EditUserRequest, EditUserResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<EditUserHandler> _logger;
    private readonly IMailService _mail;
    private readonly ILocalizationService _localizationService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAwsS3Service _awsS3Service;
    private UserResponse _userResponse;


    public EditUserHandler(IContext context, IMapper mapper, ILogger<EditUserHandler> logger, IMailService mail, UserManager<ApplicationUser> userManager, IUserResponse userResponse, ILocalizationService localizationService, IAwsS3Service awsS3Service)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
        _mail = mail;
        _userManager = userManager;
        _userResponse = (UserResponse)userResponse;
        _localizationService = localizationService;
        _awsS3Service = awsS3Service;
    }
    public async Task<EditUserResponse> Handle(EditUserRequest request, CancellationToken cancellationToken)
    {
        ApplicationUser applicationUser = await _userManager.FindByIdAsync(request.UserId.ToString());

        throw new NotImplementedException();
    }
}