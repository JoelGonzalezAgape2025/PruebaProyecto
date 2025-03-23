using System;
using System.Collections.Generic;

namespace pruebaPlanilla23.Models;

public partial class PuestoTrabajo
{
    public int Id { get; set; }

    public string NombrePuesto { get; set; } = null!;

    public decimal SalarioBase { get; set; }

    public decimal ValorxHora { get; set; }

    public decimal ValorExtra { get; set; }

    public byte? Estado { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
