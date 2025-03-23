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

        

    }
}
