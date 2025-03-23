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
    public class PuestoTrabajoController : Controller
    {
        private readonly PlanillaPrDbContext _context;

        public PuestoTrabajoController(PlanillaPrDbContext context)
        {
            _context = context;
        }

        // GET: PuestoTrabajo
        public async Task<IActionResult> Index()
        {
            return View(await _context.PuestoTrabajos.ToListAsync());
        }

        // GET: PuestoTrabajo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puestoTrabajo = await _context.PuestoTrabajos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (puestoTrabajo == null)
            {
                return NotFound();
            }

            return View(puestoTrabajo);
        }

        // GET: PuestoTrabajo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PuestoTrabajo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombrePuesto,SalarioBase,ValorxHora,ValorExtra,Estado")] PuestoTrabajo puestoTrabajo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(puestoTrabajo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(puestoTrabajo);
        }

        // GET: PuestoTrabajo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puestoTrabajo = await _context.PuestoTrabajos.FindAsync(id);
            if (puestoTrabajo == null)
            {
                return NotFound();
            }
            return View(puestoTrabajo);
        }

        // POST: PuestoTrabajo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombrePuesto,SalarioBase,ValorxHora,ValorExtra,Estado")] PuestoTrabajo puestoTrabajo)
        {
            if (id != puestoTrabajo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(puestoTrabajo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PuestoTrabajoExists(puestoTrabajo.Id))
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
            return View(puestoTrabajo);
        }

        // GET: PuestoTrabajo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puestoTrabajo = await _context.PuestoTrabajos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (puestoTrabajo == null)
            {
                return NotFound();
            }

            return View(puestoTrabajo);
        }

        // POST: PuestoTrabajo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var puestoTrabajo = await _context.PuestoTrabajos.FindAsync(id);
            if (puestoTrabajo != null)
            {
                _context.PuestoTrabajos.Remove(puestoTrabajo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PuestoTrabajoExists(int id)
        {
            return _context.PuestoTrabajos.Any(e => e.Id == id);
        }
    }
}
