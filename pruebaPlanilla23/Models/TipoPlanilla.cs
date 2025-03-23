using System;
using System.Collections.Generic;

namespace pruebaPlanilla23.Models;

public partial class TipoPlanilla
{
    public int Id { get; set; }

    public string NombreTipo { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<Planilla> Planillas { get; set; } = new List<Planilla>();
}
