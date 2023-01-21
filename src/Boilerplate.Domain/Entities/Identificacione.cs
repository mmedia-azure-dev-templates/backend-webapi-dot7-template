using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA HACE JOIN CON TABLA USERS AQUI SE ALMACENA LOS DATOS INFORMATIVOS DEL USUARIO
/// </summary>
public partial class Identificacione
{
    public int Idtid { get; set; }

    public int Usuid { get; set; }

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
