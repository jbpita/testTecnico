using System;
using System.Collections.Generic;

namespace api.src.Entities;

public partial class Usuarios
{
    public int Identificador { get; set; }
    public int Persona { get; set; }

    public string Usuario { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }
}
