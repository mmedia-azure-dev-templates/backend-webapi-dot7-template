using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

public partial class Order
{
    public int Ordid { get; set; }

    public string Ordempresa { get; set; }

    public DateOnly Ordregistrofecha1 { get; set; }

    public DateOnly? Ordregistrofecha2 { get; set; }

    public TimeOnly Ordregistrohora1 { get; set; }

    public int Ordnumero { get; set; }

    public decimal? Ordcredito { get; set; }

    public int Usuid { get; set; }

    public int? Ctoid { get; set; }

    public int? Ordassigned { get; set; }

    public char? Persontype { get; set; }

    public decimal Ordabono { get; set; }

    public decimal Ordsubtotal { get; set; }

    public decimal Ordiva { get; set; }

    public decimal Ordtotal { get; set; }

    public decimal Ordsaldo { get; set; }

    public int? Ordconvenio { get; set; }

    public int? Ordplazo { get; set; }

    public int? Ordestado { get; set; }

    public string Ordnotasobs { get; set; }

    public string Ordnotas { get; set; }

    public string Ordimgurl { get; set; }

    public string Orddocumentacion { get; set; }

    public DateTime? Orddatesalepayment { get; set; }

    public int? Ordsaleuserpayment { get; set; }

    public char? Ordsaleusertypepayment { get; set; }

    public bool? Ordsalestatepayment { get; set; }

    public string Orddespacho { get; set; }

    public string Ordextras { get; set; }
}
