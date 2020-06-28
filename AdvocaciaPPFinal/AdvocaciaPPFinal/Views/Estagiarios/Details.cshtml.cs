using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdvocaciaPPFinal.Data;
using AdvocaciaPPFinal.Models;

namespace AdvocaciaPPFinal.Views.Estagiarios
{
    public class DetailsModel : PageModel
    {
        private readonly AdvocaciaPPFinal.Data.AdvocaciaPPFinalContext _context;

        public DetailsModel(AdvocaciaPPFinal.Data.AdvocaciaPPFinalContext context)
        {
            _context = context;
        }

        public Estagiario Estagiario { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Estagiario = await _context.Estagiario.FirstOrDefaultAsync(m => m.Id_Estagiario == id);

            if (Estagiario == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
