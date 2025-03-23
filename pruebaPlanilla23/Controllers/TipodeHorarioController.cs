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


    }
}
