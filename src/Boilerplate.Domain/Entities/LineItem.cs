﻿// Copyright (c) 2021 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using AuthPermissions.BaseCode.CommonCode;
using System.ComponentModel.DataAnnotations.Schema;
namespace Boilerplate.Domain.Entities;
[Table("LineItems", Schema = "web")]
public class LineItem : IDataKeyFilterReadWrite
{
    public int LineItemId { get; set; }

    public string ItemName { get; set; }

    public int NumberItems { get; set; }

    public decimal TotalPrice { get; set; }

    public string DataKey { get; set; }
}