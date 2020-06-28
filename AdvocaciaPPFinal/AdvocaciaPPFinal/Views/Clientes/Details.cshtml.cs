using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdvocaciaPPFinal.Data;
using AdvocaciaPPFinal.Models;

namespace AdvocaciaPPFinal.Views.Clientes
{
    public class DetailsModel : PageModel
    {
        private readonly AdvocaciaPPFinal.Data.AdvocaciaPPFinalContext _context;

        public DetailsModel(AdvocaciaPPFinal.Data.AdvocaciaPPFinalContext context)
        {
            _context = context;
        }

        public Cliente Cliente { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cliente = await _context.Cliente.FirstOrDefaultAsync(m => m.Id_Cliente == id);

            if (Cliente == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
