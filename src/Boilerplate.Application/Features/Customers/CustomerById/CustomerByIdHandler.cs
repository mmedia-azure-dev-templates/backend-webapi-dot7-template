using Amazon.Runtime.Documents;
using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Address.AddresById;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using org.apache.zookeeper.data;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
namespace Boilerplate.Application.Features.Customers.CustomerById;

public class CustomerByIdHandler : IRequestHandler<CustomerByIdRequest, CustomerByIdResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;

    public CustomerByIdHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<CustomerByIdResponse> Handle(CustomerByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await (from customer in _context.Customers.AsNoTracking().DefaultIfEmpty()
                            join address in _context.Addresses on (Guid?)customer.Id equals (Guid)address.PersonId into j1
                            from address in j1.DefaultIfEmpty()
                            where customer.Id == request.CustomerId
                            select new CustomerByIdResponse
                            {
                                Id = customer.Id,
                                DocumentType = customer.DocumentType,
                                Ndocument = customer.Ndocument,
                                BirthDate = customer.BirthDate,
                                GenderType = customer.GenderType,
                                CivilStatusType = customer.CivilStatusType,
                                FirstName = customer.FirstName,
                                LastName = customer.LastName,
                                Email = customer.Email,
                                Mobile = customer.Mobile,
                                Phone = customer.Phone,
                                Notes = customer.Notes,
                                DateCreated = customer.DateCreated,
                                DateUpdated = customer.DateUpdated,
                                AddressByIdResponse = new AddressByIdResponse
                                {
                                    PersonId = address.PersonId,
                                    PrimaryStreet = address.PrimaryStreet,
                                    SecondaryStreet = address.SecondaryStreet,
                                    Numeration = address.Numeration,
                                    Reference = address.Reference,
                                    Provincia = address.Provincia,
                                    Canton = address.Canton,
                                    Parroquia = address.Parroquia,
                                    Notes = address.Notes,
                                    DateCreated = address.DateCreated,
                                    DateUpdated = address.DateUpdated,
                                }
                            }).FirstOrDefaultAsync(cancellationToken);
        return result;
    }
}