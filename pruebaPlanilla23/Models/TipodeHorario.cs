using System;
using System.Collections.Generic;

namespace pruebaPlanilla23.Models;

public partial class TipodeHorario
{
    public int Id { get; set; }

    public string NombreHorario { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<Horario> Horarios { get; set; } = new List<Horario>();
}
