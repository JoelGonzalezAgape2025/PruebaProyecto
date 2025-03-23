using System;
using System.Collections.Generic;

namespace pruebaPlanilla23.Models;

public partial class Planilla
{
    public int Id { get; set; }

    public string NombrePlanilla { get; set; } = null!;

    public int TipoPlanillaId { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public byte? Autorizacion { get; set; }

    public decimal? TotalPago { get; set; }

    public virtual ICollection<EmpleadoPlanilla> EmpleadoPlanillas { get; set; } = new List<EmpleadoPlanilla>();

    public virtual TipoPlanilla TipoPlanilla { get; set; } = null!;
}
