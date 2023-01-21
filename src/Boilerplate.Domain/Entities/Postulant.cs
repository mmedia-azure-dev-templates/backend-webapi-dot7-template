using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// POSTULANTES AQUI SE GUARDAN LAS PERSONAS QUE SE REGISTRAN EN EL SISTEMA
/// </summary>
public partial class Postulant
{
    public int Ptlid { get; set; }

    public bool? Ptlcontacted { get; set; }

    public string Ptlnombres { get; set; }

    public string Ptlapellidos { get; set; }

    public string Ptlusername { get; set; }

    public string Ptlemail { get; set; }

    public int Ptltipodocumento { get; set; }

    public int Ptlnacionalidad { get; set; }

    public string Ptlndocumento { get; set; }

    public int Ptlgenero { get; set; }

    public int Ptlestadocivil { get; set; }

    public DateOnly Ptlfecnacimiento { get; set; }

    public int Ptlprovincia { get; set; }

    public int Ptlcanton { get; set; }

    public int Ptlparroquia { get; set; }

    public string Ptldireccion { get; set; }

    public string Ptltelefono { get; set; }

    public string Ptlcelular { get; set; }

    public int Ptlestado { get; set; }

    public string Ptlimgurl { get; set; }

    public string Ptlhojavidaurl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
