using System;
using System.Collections.Generic;

namespace pruebaPlanilla23.Models;

public partial class DevengoPlanilla
{
    public int Id { get; set; }

    public int? AsignacionBonosId { get; set; }

    public int? EmpleadoPlanillaId { get; set; }

    public virtual AsignacionBono? AsignacionBonos { get; set; }

    public virtual Vacacion? EmpleadoPlanilla { get; set; }
}
