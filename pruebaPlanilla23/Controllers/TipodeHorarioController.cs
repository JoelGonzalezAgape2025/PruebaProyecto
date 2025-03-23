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

        public async Task<IActionResult> Index(TipodeHorario tipodeHorario)
        {
            var query = _context.TipodeHorarios.AsQueryable();
            if (!string.IsNullOrWhiteSpace(tipodeHorario.NombreHorario))
                query = query.Where(t => t.NombreHorario.Contains(tipodeHorario.NombreHorario));

            return View(await query.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }


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


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDeHorario = await _context.TipodeHorarios
                .Include(t => t.Horarios)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tipoDeHorario == null)
            {
                return NotFound();
            }

            ViewBag.TipoDeHorarioId = new SelectList(_context.TipodeHorarios, "Id", "NombreHorario", tipoDeHorario.Id);
            return View(tipoDeHorario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TipodeHorario tipodeHorario)
        {
            if (id != tipodeHorario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var tipoDeHorarioActual = await _context.TipodeHorarios
                        .Include(t => t.Horarios)
                        .FirstOrDefaultAsync(t => t.Id == id);

                    if (tipoDeHorarioActual == null)
                    {
                        return NotFound();
                    }

                    tipoDeHorarioActual.NombreHorario = tipodeHorario.NombreHorario;


                    foreach (var horario in tipodeHorario.Horarios)
                    {

                        if (horario.Id > 0)
                        {
                            var horarioExistente = tipoDeHorarioActual.Horarios.FirstOrDefault(h => h.Id == horario.Id);
                            if (horarioExistente != null)
                            {
                                horarioExistente.Dias = horario.Dias;
                                horarioExistente.HorasxDia = horario.HorasxDia;
                                horarioExistente.HorasEntrada = horario.HorasEntrada;
                                horarioExistente.HorasSalida = horario.HorasSalida;
                            }
                        }
                        else
                        {

                            tipoDeHorarioActual.Horarios.Add(horario);
                        }
                    }

                    var horariosAEliminar = tipoDeHorarioActual.Horarios
                        .Where(h => !tipodeHorario.Horarios.Any(nh => nh.Id == h.Id))
                        .ToList();

                    foreach (var horario in horariosAEliminar)
                    {
                        _context.Horarios.Remove(horario);
                    }


                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoDeHorarioExists(tipodeHorario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }


            ViewBag.TipoDeHorarioId = new SelectList(_context.TipodeHorarios, "Id", "NombreHorario", tipodeHorario.Id);
            return View(tipodeHorario);
        }

        private bool TipoDeHorarioExists(int id)
        {
            return _context.TipodeHorarios.Any(e => e.Id == id);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDeHorario = await _context.TipodeHorarios
                 .Include(h => h.Horarios)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (tipoDeHorario == null)
            {
                return NotFound();
            }

            return View(tipoDeHorario);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDeHorario = await _context.TipodeHorarios
                .Include(h => h.Horarios)
                .FirstOrDefaultAsync(h => h.Id == id);
            if (tipoDeHorario == null)
            {
                return NotFound();
            }

            return View(tipoDeHorario);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoDeHorario = await _context.TipodeHorarios.FindAsync(id);
            if (tipoDeHorario != null)
            {
                _context.TipodeHorarios.Remove(tipoDeHorario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
