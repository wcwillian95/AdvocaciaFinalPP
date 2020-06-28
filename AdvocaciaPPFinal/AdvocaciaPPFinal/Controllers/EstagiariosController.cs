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
    public class EstagiariosController : Controller
    {
        private readonly AdvocaciaPPFinalContext _context;

        public EstagiariosController(AdvocaciaPPFinalContext context)
        {
            _context = context;
        }

        // GET: Estagiarios
        public async Task<IActionResult> Index(string searchOption, string searchString)
        {
            var estagiarios = from m in _context.Estagiario
                              select m;
            if (searchOption == "Nome")
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    estagiarios = estagiarios.Where(s => s.Nome_Estagiario.Contains(searchString));
                }
            }
            else if (searchOption == "CPF")
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    estagiarios = estagiarios.Where(s => s.CPF_Estagiario.Contains(searchString));
                }
            }
            else if (searchOption == "Responsavel")
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    estagiarios = estagiarios.Where(s => s.Nome_Resonsavel.Contains(searchString));
                }
            }
            return View(await estagiarios.ToListAsync());
        }

        // GET: Estagiarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estagiario = await _context.Estagiario
                .FirstOrDefaultAsync(m => m.Id_Estagiario == id);
            if (estagiario == null)
            {
                return NotFound();
            }

            return View(estagiario);
        }

        // GET: Estagiarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estagiarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Estagiario,Nome_Estagiario,CPF_Estagiario,DataNascimento_Estagiario,Telefone_Estagiario,Email_Estagiario,Funcao,DataCadastro_Estagiario,Data_fim_contrato,Comentario,Nome_Resonsavel,Telefone_Responsalvel")] Estagiario estagiario)
        {
            estagiario.DataCadastro_Estagiario = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(estagiario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estagiario);
        }

        // GET: Estagiarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estagiario = await _context.Estagiario.FindAsync(id);
            if (estagiario == null)
            {
                return NotFound();
            }
            return View(estagiario);
        }

        // POST: Estagiarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Estagiario,Nome_Estagiario,CPF_Estagiario,DataNascimento_Estagiario,Telefone_Estagiario,Email_Estagiario,Funcao,DataCadastro_Estagiario,Data_fim_contrato,Comentario,Nome_Resonsavel,Telefone_Responsalvel")] Estagiario estagiario)
        {
            if (id != estagiario.Id_Estagiario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estagiario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstagiarioExists(estagiario.Id_Estagiario))
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
            return View(estagiario);
        }

        // GET: Estagiarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estagiario = await _context.Estagiario
                .FirstOrDefaultAsync(m => m.Id_Estagiario == id);
            if (estagiario == null)
            {
                return NotFound();
            }

            return View(estagiario);
        }

        // POST: Estagiarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estagiario = await _context.Estagiario.FindAsync(id);
            _context.Estagiario.Remove(estagiario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstagiarioExists(int id)
        {
            return _context.Estagiario.Any(e => e.Id_Estagiario == id);
        }
    }
}
