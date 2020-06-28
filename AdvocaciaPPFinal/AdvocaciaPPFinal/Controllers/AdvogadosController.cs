using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdvocaciaPPFinal.Data;
using AdvocaciaPPFinal.Models;
using Microsoft.AspNetCore.Authorization;

namespace AdvocaciaPPFinal.Controllers
{
    [Authorize]
    public class AdvogadosController : Controller
    {
        private readonly AdvocaciaPPFinalContext _context;

        public AdvogadosController(AdvocaciaPPFinalContext context)
        {
            _context = context;
        }

        // GET: Advogados
        public async Task<IActionResult> Index(string searchOption, string searchString)
        {
            var advogados = from m in _context.Advogado
                            select m;
            if (searchOption == "Nome")
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    advogados = advogados.Where(s => s.Nome_Advogado.Contains(searchString));
                }
            }
            else if (searchOption == "CPF")
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    advogados = advogados.Where(s => s.CPF_Advogado.Contains(searchString));
                }
            }
            else if (searchOption == "Inscricao")
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    advogados = advogados.Where(s => s.Inscricao_Advogado.Contains(searchString));
                }
            }
            return View(await advogados.ToListAsync());
        }

        // GET: Advogados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advogado = await _context.Advogado
                .FirstOrDefaultAsync(m => m.Id_Advogado == id);
            if (advogado == null)
            {
                return NotFound();
            }

            return View(advogado);
        }

        // GET: Advogados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Advogados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Advogado,Nome_Advogado,CPF_Advogado,Data_de_nascimento,Inscricao_Advogado,Instituicao_Advogado,Especializacao_Advogado,Telefone_Advogado,Email_Advogado,Numero_casos,DataAdmissao_Advogado")] Advogado advogado)
        {
            advogado.DataAdmissao_Advogado = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(advogado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(advogado);
        }

        // GET: Advogados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advogado = await _context.Advogado.FindAsync(id);
            if (advogado == null)
            {
                return NotFound();
            }
            return View(advogado);
        }

        // POST: Advogados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Advogado,Nome_Advogado,CPF_Advogado,Data_de_nascimento,Inscricao_Advogado,Instituicao_Advogado,Especializacao_Advogado,Telefone_Advogado,Email_Advogado,Numero_casos,DataAdmissao_Advogado")] Advogado advogado)
        {
            if (id != advogado.Id_Advogado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advogado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvogadoExists(advogado.Id_Advogado))
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
            return View(advogado);
        }

        // GET: Advogados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advogado = await _context.Advogado
                .FirstOrDefaultAsync(m => m.Id_Advogado == id);
            if (advogado == null)
            {
                return NotFound();
            }

            return View(advogado);
        }

        // POST: Advogados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advogado = await _context.Advogado.FindAsync(id);
            _context.Advogado.Remove(advogado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvogadoExists(int id)
        {
            return _context.Advogado.Any(e => e.Id_Advogado == id);
        }
    }
}
