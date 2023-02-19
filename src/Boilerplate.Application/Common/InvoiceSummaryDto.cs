// Copyright (c) 2021 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using Boilerplate.Domain.Entities;
using System;
using System.Linq;

namespace Boilerplate.Application.Common;

public class InvoiceSummaryDto
{
    public int InvoiceId { get; set; }

    public string InvoiceName { get; set; }

    public DateTime DateCreated { get; set; }

    public int NumItems { get; set; }

    public double? TotalCost { get; set; }

    public static IQueryable<InvoiceSummaryDto> SelectInvoices(IQueryable<Invoice> invoices)
    {
        return invoices.Select(x => new InvoiceSummaryDto
        {
            InvoiceId = x.InvoiceId,
            InvoiceName = x.InvoiceName,
            DateCreated = x.DateCreated,
            NumItems = x.LineItems.Count,
            TotalCost = x.LineItems.Select(y => (double?)y.TotalPrice).Sum()
        });
    }
}