using System;
using System.Collections.Generic;

namespace pruebaPlanilla23.Models;

public partial class AsignacionDescuento
{
    public int Id { get; set; }

    public int? EmpleadosId { get; set; }

    public int? DescuentosId { get; set; }

    public virtual ICollection<DescuentoPlanilla> DescuentoPlanillas { get; set; } = new List<DescuentoPlanilla>();

    public virtual Descuento? Descuentos { get; set; }

    public virtual Empleado? Empleados { get; set; }
}
