using Boilerplate.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA HACE JOIN CON TABLA USERS AQUI SE ALMACENA LOS DATOS INFORMATIVOS DEL USUARIO
/// </summary>
[Table("Users", Schema = "web")]
public partial class Identification : Entity<IdentificationId>
{
    [Required]
    public override IdentificationId Id { get; set; }

    public UserId UserId { get; set; }

    public int Cattipodocumento { get; set; }

    public int Catnacionalidad { get; set; }

    public string Idtndocumento { get; set; }

    public int? Catgenero { get; set; }

    public int? Catestadocivil { get; set; }

    public DateOnly? Idtfecnacimiento { get; set; }

    public DateOnly? Idtfecingreso { get; set; }

    public DateOnly? Idtfecsalida { get; set; }

    public short Idtcontratado { get; set; }

    public string Idtimgurl { get; set; }

    public string Idthojavidaurl { get; set; }

    public string Idtcelular { get; set; }

    public string Idttelefono { get; set; }

    public string Idtdireccion { get; set; }

    public int? Ubcprovincia { get; set; }

    public int? Ubccanton { get; set; }

    public int? Ubcparroquia { get; set; }

    public string Idtnotas { get; set; }
}
