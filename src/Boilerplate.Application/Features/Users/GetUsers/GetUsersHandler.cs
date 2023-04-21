using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Extensions;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Users.GetUsers;

public class GetUsersHandler : IRequestHandler<GetUsersRequest, PaginatedList<GetUsersResponse>>
{
    private readonly IContext _context;

    private readonly IMapper _mapper;

    public GetUsersHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PaginatedList<GetUsersResponse>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        string filter = Enum.Parse(typeof(OwnerFilterType), request.Filter.ToString()).ToString();

        var users = _context.ApplicationUsers.AsNoTracking()
                    .Join(_context.UserInformations.AsNoTracking(),
                        applicationUser => applicationUser.Id,
                        userInformation => (Guid)userInformation.UserId,
                        (applicationUser, userInformation) => new GetUsersResponse
                        {
                            Id = applicationUser.Id,
                            UserId = userInformation.UserId,
                            FirstName = applicationUser.FirstName,
                            LastName = applicationUser.LastName,
                            LastLogin = applicationUser.LastLogin,
                            Email = applicationUser.Email,
                            EmailConfirmed = applicationUser.EmailConfirmed,
                            DocumentType = userInformation.DocumentType,
                            Nacionality = userInformation.Nacionality,
                            Ndocument = userInformation.Ndocument,
                            Gender = userInformation.Gender,
                            CivilStatus = userInformation.CivilStatus,
                            BirthDate = userInformation.BirthDate,
                            EntryDate = userInformation.EntryDate,
                            DepartureDate = userInformation.DepartureDate,
                            Hired = userInformation.Hired,
                            ImgUrl = userInformation.ImgUrl,
                            CurriculumUrl = userInformation.CurriculumUrl,
                            Mobile = userInformation.Mobile,
                            Phone = userInformation.Phone,
                            PrimaryStreet = userInformation.PrimaryStreet,
                            SecondaryStreet = userInformation.SecondaryStreet,
                            Numeration = userInformation.Numeration,
                            Reference = userInformation.Reference,
                            Provincia = userInformation.Provincia,
                            Canton = userInformation.Canton,
                            Parroquia = userInformation.Parroquia,
                            Notes = userInformation.Notes,
                            DateCreated = userInformation.DateCreated,
                            DateUpdated = userInformation.DateUpdated
                        })
                    .WhereIf(!string.IsNullOrEmpty(request.Search) && !string.IsNullOrEmpty(filter) && filter == "FirstName", x => EF.Functions.Like(x.FirstName, $"%{request.Search}%"))
                    .WhereIf(!string.IsNullOrEmpty(request.Search) && !string.IsNullOrEmpty(filter) && filter == "LastName", x => EF.Functions.Like(x.LastName, $"%{request.Search}%"))
                    .WhereIf(!string.IsNullOrEmpty(request.Search) && !string.IsNullOrEmpty(filter) && filter == "Ndocument", x => EF.Functions.Like(x.Ndocument, $"%{request.Search}%"));
        
        return await users
            //.OrderBy(x => x.Sku)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);

    }
}