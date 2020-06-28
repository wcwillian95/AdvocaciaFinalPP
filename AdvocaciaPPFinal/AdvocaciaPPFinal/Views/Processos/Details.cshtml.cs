using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdvocaciaPPFinal.Data;
using AdvocaciaPPFinal.Models;

namespace AdvocaciaPPFinal.Views.Processos
{
    public class DetailsModel : PageModel
    {
        private readonly AdvocaciaPPFinal.Data.AdvocaciaPPFinalContext _context;

        public DetailsModel(AdvocaciaPPFinal.Data.AdvocaciaPPFinalContext context)
        {
            _context = context;
        }

        public Processo Processo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Processo = await _context.Processo.FirstOrDefaultAsync(m => m.Id_Processo == id);

            if (Processo == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
