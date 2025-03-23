using System;
using System.Collections.Generic;

namespace pruebaPlanilla23.Models;

public partial class AsignacionBono
{
    public int Id { get; set; }

    public int EmpleadosId { get; set; }

    public int BonosId { get; set; }

    public byte Estado { get; set; }

    public virtual Bono Bonos { get; set; } = null!;

    public virtual ICollection<DevengoPlanilla> DevengoPlanillas { get; set; } = new List<DevengoPlanilla>();

    public virtual Empleado Empleados { get; set; } = null!;
}
