﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdvocaciaPPFinal.Data;
using AdvocaciaPPFinal.Models;

namespace AdvocaciaPPFinal.Views.Advogados
{
    public class EditModel : PageModel
    {
        private readonly AdvocaciaPPFinal.Data.AdvocaciaPPFinalContext _context;

        public EditModel(AdvocaciaPPFinal.Data.AdvocaciaPPFinalContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Advogado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvogadoExists(Advogado.Id_Advogado))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AdvogadoExists(int id)
        {
            return _context.Advogado.Any(e => e.Id_Advogado == id);
        }
    }
}
