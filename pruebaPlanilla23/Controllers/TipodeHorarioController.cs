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
    public class TipodeHorarioController : Controller
    {
        private readonly PlanillaPrDbContext _context;

        public TipodeHorarioController(PlanillaPrDbContext context)
        {
            _context = context;
        }

        // GET: TipodeHorario
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipodeHorarios.ToListAsync());
        }

        // GET: TipodeHorario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipodeHorario = await _context.TipodeHorarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipodeHorario == null)
            {
                return NotFound();
            }

            return View(tipodeHorario);
        }

        // GET: TipodeHorario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipodeHorario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreHorario")] TipodeHorario tipodeHorario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipodeHorario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipodeHorario);
        }

        // GET: TipodeHorario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipodeHorario = await _context.TipodeHorarios.FindAsync(id);
            if (tipodeHorario == null)
            {
                return NotFound();
            }
            return View(tipodeHorario);
        }

        // POST: TipodeHorario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreHorario")] TipodeHorario tipodeHorario)
        {
            if (id != tipodeHorario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipodeHorario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipodeHorarioExists(tipodeHorario.Id))
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
            return View(tipodeHorario);
        }

        // GET: TipodeHorario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipodeHorario = await _context.TipodeHorarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipodeHorario == null)
            {
                return NotFound();
            }

            return View(tipodeHorario);
        }

        // POST: TipodeHorario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipodeHorario = await _context.TipodeHorarios.FindAsync(id);
            if (tipodeHorario != null)
            {
                _context.TipodeHorarios.Remove(tipodeHorario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipodeHorarioExists(int id)
        {
            return _context.TipodeHorarios.Any(e => e.Id == id);
        }
    }
}
