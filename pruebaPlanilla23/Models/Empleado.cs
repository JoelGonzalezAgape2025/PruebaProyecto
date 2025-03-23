using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pruebaPlanilla23.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public int? JefeInmediatoId { get; set; }

    public int? TipoDeHorarioId { get; set; }
    [Required(ErrorMessage = "El DUI es obligatorio.")]
    [RegularExpression(@"^\d{8}-\d$", ErrorMessage = "El formato del DUI es inválido.")]
    public string Dui { get; set; } = null!;
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MaxLength(50, ErrorMessage = "El nombre no debe exceder 50 caracteres.")]
    public string Nombre { get; set; } = null!;
    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [MaxLength(50, ErrorMessage = "El apellido no debe exceder 50 caracteres.")]
    public string Apellido { get; set; } = null!;
    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [RegularExpression(@"^\d{8}$", ErrorMessage = "El número de teléfono debe contener exactamente 8 dígitos.")]
    public string Telefono { get; set; } = null!;
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato del correo es inválido.")]
    public string? Correo { get; set; }
    [Required(ErrorMessage = "El estado es obligatorio.")]
    [Range(0, 1, ErrorMessage = "Estado solo puede ser 0 o 1.")]
    public byte? Estado { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "El salario base debe ser un valor positivo.")]
    public decimal? SalarioBase { get; set; }
    [Required(ErrorMessage = "La fecha inicial del contrato es obligatoria.")]
    public DateOnly FechaContraInicial { get; set; }
    [Required(ErrorMessage = "La fecha final del contrato es obligatoria.")]
    public DateOnly FechaContraFinal { get; set; }
    [MaxLength(20, ErrorMessage = "El usuario no debe exceder 20 caracteres.")]
    public string? Usuario { get; set; }
    [RegularExpression(@"^[A-Za-z\d]{8}$", ErrorMessage = "La contraseña debe tener exactamente 8 caracteres y estar compuesta solo por letras o números.")]
    public string? Contraseña { get; set; }

    public int? PuestoTrabajoId { get; set; }

    public virtual ICollection<AsignacionBono> AsignacionBonos { get; set; } = new List<AsignacionBono>();

    public virtual ICollection<AsignacionDescuento> AsignacionDescuentos { get; set; } = new List<AsignacionDescuento>();

    public virtual ICollection<ControlAsistencium> ControlAsistencia { get; set; } = new List<ControlAsistencium>();

    public virtual ICollection<EmpleadoPlanilla> EmpleadoPlanillas { get; set; } = new List<EmpleadoPlanilla>();

    public virtual ICollection<Empleado> InverseJefeInmediato { get; set; } = new List<Empleado>();

    public virtual Empleado? JefeInmediato { get; set; }

    public virtual ICollection<JefeInmediato> JefeInmediatos { get; set; } = new List<JefeInmediato>();

    public virtual PuestoTrabajo? PuestoTrabajo { get; set; }

    public virtual TipodeHorario? TipoDeHorario { get; set; }

    public virtual ICollection<Vacacion> Vacacions { get; set; } = new List<Vacacion>();
}
