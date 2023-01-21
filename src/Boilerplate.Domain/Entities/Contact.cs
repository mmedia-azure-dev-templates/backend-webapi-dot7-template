using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DONDE SE ALMACENAN LOS CLIENTES
/// </summary>
public partial class Contact
{
    public int Ctoid { get; set; }

    public string Ctodocumento { get; set; }

    public string Ctonombres { get; set; }

    public string Ctoapellidos { get; set; }

    public string Ctoemail { get; set; }

    public string Ctocelular { get; set; }

    public string Ctotelefono { get; set; }

    public string Ctodireccion { get; set; }

    public int? Ctotipodocumento { get; set; }

    public int? Ctotiponacionalidad { get; set; }

    public int? Ctoprovincia { get; set; }

    public int? Ctocanton { get; set; }

    public int? Ctoparroquia { get; set; }

    public string Ctonotas { get; set; }

    public int? Ctosupervisor { get; set; }

    public string Ctoestadocivil { get; set; }

    public DateTime? Ctofechanacimiento { get; set; }
}
