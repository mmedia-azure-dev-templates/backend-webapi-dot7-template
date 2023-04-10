using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Orders.OrderById;
using Boilerplate.Application.Features.Orders.OrderValid;
using Boilerplate.Domain.Entities.Pdfs;
using Boilerplate.Domain.Implementations;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Orders.OrderPdf;
public class OrderPdfHandler : IRequestHandler<OrderPdfRequest, OrderPdfResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public OrderPdfHandler(IContext context, IMapper mapper, IMediator mediator)
    {
        _context = context;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<OrderPdfResponse> Handle(OrderPdfRequest request, CancellationToken cancellationToken)
    {
        var result = new OrderPdfResponse();
        var orderValid = await _mediator.Send(new OrderValidRequest { OrderId = request.OrderId }, cancellationToken);

        if (!orderValid.IsValid)
        {
            return result;
        }
        throw new NotImplementedException();
        //var document = new OrderDocument(orderValid.OrderByIdResponse);
        //return File(document.GeneratePdf(), "application/pdf", "myReport.pdf");
    }
}