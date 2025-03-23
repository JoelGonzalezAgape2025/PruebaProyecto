using System;
using System.Collections.Generic;

namespace pruebaPlanilla23.Models;

public partial class JefeInmediato
{
    public int Id { get; set; }

    public int? EmpleadosId { get; set; }

    public virtual Empleado? Empleados { get; set; }
}
