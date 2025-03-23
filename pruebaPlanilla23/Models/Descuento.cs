using System;
using System.Collections.Generic;

namespace pruebaPlanilla23.Models;

public partial class Descuento
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Valor { get; set; }

    public byte? Estado { get; set; }

    public byte Operacion { get; set; }

    public byte Planilla { get; set; }

    public virtual ICollection<AsignacionDescuento> AsignacionDescuentos { get; set; } = new List<AsignacionDescuento>();
}
