﻿using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Boilerplate.Application.Common.Pdfs.Components;
public class AddressComponent : IComponent
{
    private string Title { get; }
    private Address2 Address { get; }

    public AddressComponent(string title, Address2 address)
    {
        Title = title;
        Address = address;
    }

    public void Compose(IContainer container)
    {
        container.Column(column =>
        {
            column.Spacing(2);

            column.Item().BorderBottom(1).PaddingBottom(5).Text(Title).SemiBold();

            column.Item().Text(Address.CompanyName);
            column.Item().Text(Address.Street);
            column.Item().Text($"{Address.City}, {Address.State}");
            column.Item().Text(Address.Email);
            column.Item().Text(Address.Phone);
        });
    }
}