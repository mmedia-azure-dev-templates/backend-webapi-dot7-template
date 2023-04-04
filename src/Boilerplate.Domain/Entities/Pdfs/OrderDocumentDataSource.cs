using Boilerplate.Domain.Entities;
using QuestPDF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

public class OrderDocumentDataSource
{
    private Order Order { get; set; }
    private List<OrderItem> OrderItems { get; set; }
    private Customer Customer { get; set; }
    public OrderDocumentDataSource(Order order, List<OrderItem> orderItems, Customer customer)
    {
        Order = order;
        OrderItems = orderItems;
        Customer = customer;
    }
}