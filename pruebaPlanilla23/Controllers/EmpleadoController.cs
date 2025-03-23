using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pruebaPlanilla23.Models;

namespace pruebaPlanilla23.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly PlanillaPrDbContext _context;

        public EmpleadoController(PlanillaPrDbContext context)
        {
            _context = context;
        }

        // GET: Empleado
        public async Task<IActionResult> Index()
        {
            var planillaPrDbContext = _context.Empleados.Include(e => e.JefeInmediato).Include(e => e.PuestoTrabajo).Include(e => e.TipoDeHorario);
            return View(await planillaPrDbContext.ToListAsync());
        }

        // GET: Empleado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.JefeInmediato)
                .Include(e => e.PuestoTrabajo)
                .Include(e => e.TipoDeHorario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleado/Create
        public IActionResult Create()
        {
            ViewData["JefeInmediatoId"] = new SelectList(_context.Empleados, "Id", "Id");
            ViewData["PuestoTrabajoId"] = new SelectList(_context.PuestoTrabajos, "Id", "NombrePuesto");
            ViewData["TipoDeHorarioId"] = new SelectList(_context.TipodeHorarios, "Id", "Id");
            return View();
        }

        // POST: Empleado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JefeInmediatoId,TipoDeHorarioId,Dui,Nombre,Apellido,Telefono,Correo,Estado,SalarioBase,FechaContraInicial,FechaContraFinal,Usuario,Contraseña,PuestoTrabajoId")] Empleado empleado)
        {
          
            // Validar duplicados
            if (await _context.Empleados.AnyAsync(e => e.Dui == empleado.Dui))
            {
                ModelState.AddModelError("Dui", "El DUI ingresado ya está registrado.");
            }

            if (await _context.Empleados.AnyAsync(e => e.Correo == empleado.Correo))
            {
                ModelState.AddModelError("Correo", "El Correo ingresado ya está registrado.");
            }

            // Validar roles permitidos
            var rolesPermitidos = new[] { "Gerente de Recursos Humanos", "Supervisor", "Administrador de Nómina" };
            var puestoTrabajo = await _context.PuestoTrabajos.FindAsync(empleado.PuestoTrabajoId);

            // Validar Jefe Inmediato
            var rolesSinJefeInmediato = new[] { "Gerente de Recursos Humanos", "Supervisor", "Administrador de Nómina" };
            if (puestoTrabajo != null && rolesSinJefeInmediato.Contains(puestoTrabajo.NombrePuesto))
            {
                if (empleado.JefeInmediatoId != null)
                {
                    // Solo agrega error si se intenta asignar un jefe inmediato, ya que no está permitido
                    ModelState.AddModelError("JefeInmediatoId", "El campo Jefe Inmediato no se puede asignar para este puesto.");
                    empleado.JefeInmediatoId = null; // Asegurar que no tenga valor.
                }
            }

            // Si el modelo es válido, guarda los datos
            if (ModelState.IsValid)
            {
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Recargar datos para el formulario
            ViewData["JefeInmediatoId"] = new SelectList(_context.Empleados, "Id", "Id", empleado.JefeInmediatoId);
            ViewData["PuestoTrabajoId"] = new SelectList(_context.PuestoTrabajos, "Id", "NombrePuesto", empleado.PuestoTrabajoId);
            ViewData["TipoDeHorarioId"] = new SelectList(_context.TipodeHorarios, "Id", "Id", empleado.TipoDeHorarioId);
            return View(empleado);
        }

        [HttpGet]
        public async Task<IActionResult> GetPuestoNombre(int puestoTrabajoId)
        {
            var puesto = await _context.PuestoTrabajos
                .Where(p => p.Id == puestoTrabajoId)
                .Select(p => new { NombrePuesto = p.NombrePuesto })
                .FirstOrDefaultAsync();

            if (puesto == null)
            {
                return NotFound();
            }

            return Json(new { nombrePuesto = puesto.NombrePuesto });
        }

        // GET: Empleado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewData["JefeInmediatoId"] = new SelectList(_context.Empleados, "Id", "Id", empleado.JefeInmediatoId);
            ViewData["PuestoTrabajoId"] = new SelectList(_context.PuestoTrabajos, "Id", "NombrePuesto", empleado.PuestoTrabajoId);
            ViewData["TipoDeHorarioId"] = new SelectList(_context.TipodeHorarios, "Id", "Id", empleado.TipoDeHorarioId);
            return View(empleado);
        }

        // POST: Empleado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JefeInmediatoId,TipoDeHorarioId,Dui,Nombre,Apellido,Telefono,Correo,Estado,SalarioBase,FechaContraInicial,FechaContraFinal,Usuario,Contraseña,PuestoTrabajoId")] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            // Validar roles permitidos
            var rolesPermitidos = new[] { "Gerente de Recursos Humanos", "Supervisor", "Administrador de Nómina" };
            var puestoTrabajo = await _context.PuestoTrabajos.FindAsync(empleado.PuestoTrabajoId);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["JefeInmediatoId"] = new SelectList(_context.Empleados, "Id", "Id", empleado.JefeInmediatoId);
            ViewData["PuestoTrabajoId"] = new SelectList(_context.PuestoTrabajos, "Id", "NombrePuesto", empleado.PuestoTrabajoId);
            ViewData["TipoDeHorarioId"] = new SelectList(_context.TipodeHorarios, "Id", "Id", empleado.TipoDeHorarioId);
            return View(empleado);
        }

        // GET: Empleado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.JefeInmediato)
                .Include(e => e.PuestoTrabajo)
                .Include(e => e.TipoDeHorario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }

        //CONSULTA PARA OBTENER SALARIO SEGUN PUESTO
        [HttpGet]
        public async Task<IActionResult> GetSalarioBase(int puestoTrabajoId)
        {
            var puesto = await _context.PuestoTrabajos
                .Where(p => p.Id == puestoTrabajoId)
                .Select(p => new { SalarioBase = p.SalarioBase })
                .FirstOrDefaultAsync();

            if (puesto == null)
            {
                return NotFound();
            }

            return Json(new { salarioBase = puesto.SalarioBase });
        }
    }
}
