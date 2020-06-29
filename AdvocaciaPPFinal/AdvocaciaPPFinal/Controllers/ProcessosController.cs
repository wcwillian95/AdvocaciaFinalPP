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
using System.IO;
using PdfSharpCore.Drawing;

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
        public async Task<IActionResult> Index(string searchOption, string searchString)
        {
            var processos = from m in _context.Processo
                            select m;
            if (searchOption == "Status")
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    processos = processos.Where(s => s.Status.Contains(searchString));
                }
            }
            else if (searchOption == "Descricao")
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    processos = processos.Where(s => s.Descricao.Contains(searchString));
                }
            }
            return View(await processos.ToListAsync());
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
        public async Task<IActionResult> Create()
        {
            var advogados = await _context.Advogado.ToListAsync();
            var clientes = await _context.Cliente.ToListAsync();
            ViewBag.Advogados = new SelectList(advogados, "Nome_Advogado", "Nome_Advogado");
            ViewBag.Clientes = new SelectList(clientes, "Nome_Cliente", "Nome_Cliente");
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

        public async Task<FileResult> GerarRelatorio()
        {
            using (var doc = new PdfSharpCore.Pdf.PdfDocument())
            {
                var page = doc.AddPage();
                page.Size = PdfSharpCore.PageSize.A4;
                page.Orientation = PdfSharpCore.PageOrientation.Portrait;
                var graphics = PdfSharpCore.Drawing.XGraphics.FromPdfPage(page);
                var corFonte = PdfSharpCore.Drawing.XBrushes.Black;

                var textFormatter = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);
                var fonteOrganzacao = new PdfSharpCore.Drawing.XFont("Arial", 10);
                var fonteDescricao = new PdfSharpCore.Drawing.XFont("Arial", 8, PdfSharpCore.Drawing.XFontStyle.BoldItalic);
                var titulodetalhes = new PdfSharpCore.Drawing.XFont("Arial", 14, PdfSharpCore.Drawing.XFontStyle.Bold);
                var fonteDetalhesDescricao = new PdfSharpCore.Drawing.XFont("Arial", 7);

                var logo = @"wwwroot\imagens\logo.jpg";



                var qtdPaginas = doc.PageCount;

                textFormatter.DrawString(qtdPaginas.ToString(), new PdfSharpCore.Drawing.XFont("Arial", 10), corFonte, new PdfSharpCore.Drawing.XRect(575, 825, page.Width, page.Height));

                // Impressão do LogoTipo
                XImage imagem = XImage.FromFile(logo);
                graphics.DrawImage(imagem, 50, 10, 500, 100);

                // Titulo Exibição
                textFormatter.DrawString(" Nome : ", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(20, 130, page.Width, page.Height));
                textFormatter.DrawString(" Agência de Advocacia ", fonteOrganzacao, corFonte, new PdfSharpCore.Drawing.XRect(80, 130, page.Width, page.Height));

                textFormatter.DrawString(" Cliente : ", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(20, 150, page.Width, page.Height));
                textFormatter.DrawString(" Unibrasil2020 ", fonteOrganzacao, corFonte, new PdfSharpCore.Drawing.XRect(80, 150, page.Width, page.Height));

                textFormatter.DrawString(" Processo : ", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(20, 170, page.Width, page.Height));
                textFormatter.DrawString(DateTime.Now.ToString(), fonteOrganzacao, corFonte, new PdfSharpCore.Drawing.XRect(80, 170, page.Width, page.Height));


                // Titulo maior 
                var tituloDetalhes = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);
                tituloDetalhes.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                tituloDetalhes.DrawString(" Detalhes ", titulodetalhes, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));


                // titulo das colunas
                var alturaTituloDetalhesY = 240;
                var detalhes = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);

                detalhes.DrawString("Advogado", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(35, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Cliente", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(135, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Descrição", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(225, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Data de Inicio", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(315, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Data de Termino", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(405, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Status", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(495, alturaTituloDetalhesY, page.Width, page.Height));



                //dados do relatório 
                var alturaDetalhesItens = 260;
                var processos = await _context.Processo.ToListAsync();
                foreach (var a in processos)
                {
                    textFormatter.DrawString(a.NomeAdvogado_Processo, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(20, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.NomeCliente_Processo, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(125, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.Descricao, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(225, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.Data_de_Inicio.ToString("dd/MM/yyyy"), fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(315, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.Data_de_termino.ToString("dd/MM/yyyy"), fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(405, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.Status, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(485, alturaDetalhesItens, page.Width, page.Height));
                    alturaDetalhesItens += 20;
                }


                using (MemoryStream stream = new MemoryStream())
                {
                    var contantType = "application/pdf";
                    doc.Save(stream, false);

                    var nomeArquivo = "RelatorioAdvocaciaPPFinal.pdf";

                    return File(stream.ToArray(), contantType, nomeArquivo);
                }
            }
        }

    }
}
