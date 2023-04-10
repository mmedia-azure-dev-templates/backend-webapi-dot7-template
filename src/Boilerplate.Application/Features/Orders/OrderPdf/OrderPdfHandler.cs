using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Common.Pdfs;
using Boilerplate.Application.Features.Orders.OrderValid;
using Boilerplate.Application.Implementations;
using Boilerplate.Domain.Entities.Common;
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
    private readonly IPdfService _pdfService;

    public OrderPdfHandler(IContext context, IMapper mapper, IMediator mediator, IPdfService pdfService)
    {
        _context = context;
        _mapper = mapper;
        _mediator = mediator;
        _pdfService = pdfService;
    }

    public async Task<OrderPdfResponse> Handle(OrderPdfRequest request, CancellationToken cancellationToken)
    {
        var result = new OrderPdfResponse();
        var orderValid = await _mediator.Send(new OrderValidRequest { OrderId = request.OrderId }, cancellationToken);

        if (!orderValid.IsValid)
        {
            return result;
        }
        var document = new OrderDocument(orderValid);
        AmazonObject amazonObject = await _pdfService.CreateOrderPdf(document);
        result.IsValid = true;
        result.DocumentUrl = amazonObject.ObjectUrl;
        return result;
    }
}