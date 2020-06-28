using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdvocaciaPPFinal.Data;
using AdvocaciaPPFinal.Models;

namespace AdvocaciaPPFinal.Views.Advogados
{
    public class DetailsModel : PageModel
    {
        private readonly AdvocaciaPPFinal.Data.AdvocaciaPPFinalContext _context;

        public DetailsModel(AdvocaciaPPFinal.Data.AdvocaciaPPFinalContext context)
        {
            _context = context;
        }

        public Advogado Advogado { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Advogado = await _context.Advogado.FirstOrDefaultAsync(m => m.Id_Advogado == id);

            if (Advogado == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
