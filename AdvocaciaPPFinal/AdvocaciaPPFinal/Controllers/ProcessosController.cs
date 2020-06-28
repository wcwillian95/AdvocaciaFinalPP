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
    public class ProcessosController : Controller
    {
        private readonly AdvocaciaPPFinalContext _context;

        public ProcessosController(AdvocaciaPPFinalContext context)
        {
            _context = context;
        }

        // GET: Processos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Processo.ToListAsync());
        }

        // GET: Processos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processo = await _context.Processo
                .FirstOrDefaultAsync(m => m.Id_Processo == id);
            if (processo == null)
            {
                return NotFound();
            }

            return View(processo);
        }

        // GET: Processos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Processos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Processo,NomeAdvogado_Processo,NomeCliente_Processo,Descricao,Data_de_Inicio,Data_de_termino,Status")] Processo processo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(processo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(processo);
        }

        // GET: Processos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processo = await _context.Processo.FindAsync(id);
            if (processo == null)
            {
                return NotFound();
            }
            return View(processo);
        }

        // POST: Processos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Processo,NomeAdvogado_Processo,NomeCliente_Processo,Descricao,Data_de_Inicio,Data_de_termino,Status")] Processo processo)
        {
            if (id != processo.Id_Processo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(processo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcessoExists(processo.Id_Processo))
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
            return View(processo);
        }

        // GET: Processos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processo = await _context.Processo
                .FirstOrDefaultAsync(m => m.Id_Processo == id);
            if (processo == null)
            {
                return NotFound();
            }

            return View(processo);
        }

        // POST: Processos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var processo = await _context.Processo.FindAsync(id);
            _context.Processo.Remove(processo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcessoExists(int id)
        {
            return _context.Processo.Any(e => e.Id_Processo == id);
        }
    }
}
