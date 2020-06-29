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

                detalhes.DrawString("Nome", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(45, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("CPF", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(135, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Data Nascimento", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(210, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Telefone", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(315, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Data Admissão", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(395, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Responsável", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(495, alturaTituloDetalhesY, page.Width, page.Height));



                //dados do relatório 
                var alturaDetalhesItens = 260;
                var estagiarios = await _context.Estagiario.ToListAsync();
                foreach (var a in estagiarios)
                {
                    textFormatter.DrawString(a.Nome_Estagiario, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(20, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.CPF_Estagiario, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(125, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.DataNascimento_Estagiario.ToString("dd/MM/yyyy"), fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(215, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.Telefone_Estagiario.ToString(), fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(305, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.DataCadastro_Estagiario.ToString("dd/MM/yyyy"), fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(395, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.Nome_Resonsavel, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(485, alturaDetalhesItens, page.Width, page.Height));
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
