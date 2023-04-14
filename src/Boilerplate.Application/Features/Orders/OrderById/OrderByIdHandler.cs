﻿using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Address.AddresById;
using Boilerplate.Application.Features.Articles.ArticleSearch;
using Boilerplate.Application.Features.Customers.CustomerById;
using Boilerplate.Application.Features.Users.GetUsers;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Orders.OrderById;
public class OrderByIdHandler : IRequestHandler<OrderByIdRequest, OrderByIdResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    public OrderByIdHandler(IContext context, IMapper mapper, IMediator mediator)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrderByIdResponse> Handle(OrderByIdRequest request, CancellationToken cancellationToken)
    {
        var result = (from order in _context.Orders.AsNoTracking().DefaultIfEmpty()
                      join orderItems in _context.OrderItems.AsNoTracking().DefaultIfEmpty() on order.Id equals orderItems.OrderId into j1
                      from orderItems in j1.DefaultIfEmpty()
                      join articles in _context.Articles.AsNoTracking().DefaultIfEmpty() on orderItems.ArticleId equals articles.Id into j2
                      from articles in j2.DefaultIfEmpty()
                      join userGeneratedApplicationUser in _context.ApplicationUsers.AsNoTracking().DefaultIfEmpty() on new { p1 = (Guid)order.UserGenerated } equals new { p1 = userGeneratedApplicationUser.Id } into j3
                      from userGeneratedApplicationUser in j3.DefaultIfEmpty()
                      join userGeneratedUserInformation in _context.UserInformations.AsNoTracking().DefaultIfEmpty() on new { p1 = (Guid)order.UserGenerated } equals new { p1 = (Guid)userGeneratedUserInformation.UserId } into j4
                      from userGeneratedUserInformation in j4.DefaultIfEmpty()
                      join userAssignedApplicationUser in _context.ApplicationUsers.AsNoTracking() on (Guid?)order.UserAssigned equals userAssignedApplicationUser.Id into j5
                      from userAssignedApplicationUser in j5.DefaultIfEmpty()
                      join userAssignedUserInformation in _context.UserInformations.AsNoTracking() on (Guid?)order.UserAssigned equals (Guid)userAssignedUserInformation.UserId into j6
                      from userAssignedUserInformation in j6.DefaultIfEmpty()
                      join customer in _context.Customers.AsNoTracking().DefaultIfEmpty() on order.CustomerId equals customer.Id into j7
                      from customer in j7.DefaultIfEmpty()
                      join address in _context.Addresses.AsNoTracking().DefaultIfEmpty() on (Guid?)customer.Id equals (Guid)address.PersonId into j8
                      from address in j8.DefaultIfEmpty()
                      join provincia in _context.GeographicLocations.AsNoTracking().DefaultIfEmpty() on address.Provincia equals (int)provincia.Id into j9
                      from provincia in j9.DefaultIfEmpty()
                      join canton in _context.GeographicLocations.AsNoTracking().DefaultIfEmpty() on address.Canton equals (int)canton.Id into j10
                      from canton in j10.DefaultIfEmpty()
                      join parroquia in _context.GeographicLocations.AsNoTracking().DefaultIfEmpty() on address.Parroquia equals (int)parroquia.Id into j11
                      from parroquia in j11.DefaultIfEmpty()
                      where order.Id == request.OrderId
                      select new
                      {
                          order,
                          orderItems,
                          articles,
                          userGeneratedApplicationUser,
                          userGeneratedUserInformation,
                          userAssignedApplicationUser,
                          userAssignedUserInformation,
                          customer,
                          address,
                          provincia,
                          canton,
                          parroquia
                      });

        var products = (from product in result.AsNoTracking().DefaultIfEmpty()
                        where product.orderItems != null && product.articles != null
                        group new
                        {
                            product.orderItems,
                            product.articles,
                        } by new { product.orderItems.Id } into h
                        select new ArticleSearchResponse
                        {
                            ArticleId = h.First().orderItems.ArticleId,
                            OrderId = h.First().orderItems.OrderId,
                            Provider = h.First().articles.Provider,
                            Sku = h.First().articles.Sku,
                            Abrevia = h.First().articles.Abrevia,
                            Display = h.First().articles.Display,
                            Delivered = h.First().orderItems.Delivered,
                            Quantity = h.First().orderItems.Quantity,
                            Cost = h.First().articles.Cost,
                            Total = h.First().orderItems.Total,
                            Brand = h.First().articles.Brand,
                            Notes = h.First().articles.Abrevia,
                            Meta = h.First().articles.Abrevia,
                            Discontinued = h.First().articles.Discontinued,
                            IsSelected = true,
                        });

        var finalResult = (from order in result.AsNoTracking()
                           group new
                           {
                               order.order,
                               order.customer,
                               order.address,
                               order.provincia,
                               order.canton,
                               order.parroquia,
                               order.userGeneratedApplicationUser,
                               order.userGeneratedUserInformation,
                               order.userAssignedApplicationUser,
                               order.userAssignedUserInformation
                           } by new { order.order.OrderNumber } into g
                           orderby g.Key.OrderNumber ascending
                           select new OrderByIdResponse
                           {
                               Order = g.First().order,
                               Customer = g.First().customer == null ? null : new CustomerByIdResponse
                               {
                                   Id = g.First().customer.Id,
                                   DocumentType = g.First().customer.DocumentType,
                                   Ndocument = g.First().customer.Ndocument,
                                   BirthDate = g.First().customer.BirthDate,
                                   GenderType = g.First().customer.GenderType,
                                   CivilStatusType = g.First().customer.CivilStatusType,
                                   FirstName = g.First().customer.FirstName,
                                   LastName = g.First().customer.LastName,
                                   Email = g.First().customer.Email,
                                   Mobile = g.First().customer.Mobile,
                                   Phone = g.First().customer.Phone,
                                   Notes = g.First().customer.Notes,
                                   DateCreated = g.First().customer.DateCreated,
                                   DateUpdated = g.First().customer.DateUpdated,
                                   AddressByIdResponse = g.First().address == null ? null : new AddressByIdResponse
                                   {
                                       PersonId = g.First().address.PersonId,
                                       PrimaryStreet = g.First().address.PrimaryStreet,
                                       SecondaryStreet = g.First().address.SecondaryStreet,
                                       Numeration = g.First().address.Numeration,
                                       Reference = g.First().address.Reference,
                                       ProvinciaDisplay = g.First().provincia.Name,
                                       Provincia = g.First().address.Provincia,
                                       CantonDisplay = g.First().canton.Name,
                                       Canton = g.First().address.Canton,
                                       ParroquiaDisplay = g.First().parroquia.Name,
                                       Parroquia = g.First().address.Parroquia,
                                       Notes = g.First().address.Notes,
                                       DateCreated = g.First().address.DateCreated,
                                       DateUpdated = g.First().address.DateUpdated,
                                   }
                               },
                               UserGenerated = new GetUsersResponse
                               {
                                   Id = g.First().userGeneratedApplicationUser.Id,
                                   UserId = g.First().userGeneratedUserInformation.UserId,
                                   FirstName = g.First().userGeneratedApplicationUser.FirstName,
                                   LastName = g.First().userGeneratedApplicationUser.LastName,
                                   LastLogin = g.First().userGeneratedApplicationUser.LastLogin,
                                   Email = g.First().userGeneratedApplicationUser.Email,
                                   EmailConfirmed = g.First().userGeneratedApplicationUser.EmailConfirmed,
                                   TypeDocument = g.First().userGeneratedUserInformation.TypeDocument,
                                   Nacionality = g.First().userGeneratedUserInformation.Nacionality,
                                   Ndocument = g.First().userGeneratedUserInformation.Ndocument,
                                   Gender = g.First().userGeneratedUserInformation.Gender,
                                   CivilStatus = g.First().userGeneratedUserInformation.CivilStatus,
                                   BirthDate = g.First().userGeneratedUserInformation.BirthDate,
                                   EntryDate = g.First().userGeneratedUserInformation.EntryDate,
                                   DepartureDate = g.First().userGeneratedUserInformation.DepartureDate,
                                   Hired = g.First().userGeneratedUserInformation.Hired,
                                   ImgUrl = g.First().userGeneratedUserInformation.ImgUrl,
                                   CurriculumUrl = g.First().userGeneratedUserInformation.CurriculumUrl,
                                   Mobile = g.First().userGeneratedUserInformation.Mobile,
                                   Phone = g.First().userGeneratedUserInformation.Phone,
                                   PrimaryStreet = g.First().userGeneratedUserInformation.PrimaryStreet,
                                   SecondaryStreet = g.First().userGeneratedUserInformation.SecondaryStreet,
                                   Numeration = g.First().userGeneratedUserInformation.Numeration,
                                   Reference = g.First().userGeneratedUserInformation.Reference,
                                   Provincia = g.First().userGeneratedUserInformation.Provincia,
                                   Canton = g.First().userGeneratedUserInformation.Canton,
                                   Parroquia = g.First().userGeneratedUserInformation.Parroquia,
                                   Notes = g.First().userGeneratedUserInformation.Notes,
                                   DateCreated = g.First().userGeneratedUserInformation.DateCreated,
                                   DateUpdated = g.First().userGeneratedUserInformation.DateUpdated,
                               },
                               UserAssigned = g.First().userAssignedApplicationUser == null || g.First().userAssignedUserInformation == null ? null : new GetUsersResponse
                               {
                                   Id = g.First().userAssignedApplicationUser.Id,
                                   UserId = g.First().userAssignedUserInformation.UserId,
                                   FirstName = g.First().userAssignedApplicationUser.FirstName,
                                   LastName = g.First().userAssignedApplicationUser.LastName,
                                   LastLogin = g.First().userAssignedApplicationUser.LastLogin,
                                   Email = g.First().userAssignedApplicationUser.Email,
                                   EmailConfirmed = g.First().userAssignedApplicationUser.EmailConfirmed,
                                   TypeDocument = g.First().userAssignedUserInformation.TypeDocument,
                                   Nacionality = g.First().userAssignedUserInformation.Nacionality,
                                   Ndocument = g.First().userAssignedUserInformation.Ndocument,
                                   Gender = g.First().userAssignedUserInformation.Gender,
                                   CivilStatus = g.First().userAssignedUserInformation.CivilStatus,
                                   BirthDate = g.First().userAssignedUserInformation.BirthDate,
                                   EntryDate = g.First().userAssignedUserInformation.EntryDate,
                                   DepartureDate = g.First().userAssignedUserInformation.DepartureDate,
                                   Hired = g.First().userAssignedUserInformation.Hired,
                                   ImgUrl = g.First().userAssignedUserInformation.ImgUrl,
                                   CurriculumUrl = g.First().userAssignedUserInformation.CurriculumUrl,
                                   Mobile = g.First().userAssignedUserInformation.Mobile,
                                   Phone = g.First().userAssignedUserInformation.Phone,
                                   PrimaryStreet = g.First().userAssignedUserInformation.PrimaryStreet,
                                   SecondaryStreet = g.First().userAssignedUserInformation.SecondaryStreet,
                                   Numeration = g.First().userAssignedUserInformation.Numeration,
                                   Reference = g.First().userAssignedUserInformation.Reference,
                                   Provincia = g.First().userAssignedUserInformation.Provincia,
                                   Canton = g.First().userAssignedUserInformation.Canton,
                                   Parroquia = g.First().userAssignedUserInformation.Parroquia,
                                   Notes = g.First().userAssignedUserInformation.Notes,
                                   DateCreated = g.First().userAssignedUserInformation.DateCreated,
                                   DateUpdated = g.First().userAssignedUserInformation.DateUpdated,
                               },
                               ArticleSearchResponse = (List<ArticleSearchResponse>)(
                                                        from product in products.DefaultIfEmpty()
                                                        where product.OrderId == g.First().order.Id
                                                        select product)
                           });

        return _mapper.Map<OrderByIdResponse>(await finalResult.FirstOrDefaultAsync(cancellationToken));
    }
}