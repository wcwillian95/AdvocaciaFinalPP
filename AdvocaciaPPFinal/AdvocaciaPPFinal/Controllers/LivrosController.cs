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
    public class LivrosController : Controller
    {
        private readonly AdvocaciaPPFinalContext _context;

        public LivrosController(AdvocaciaPPFinalContext context)
        {
            _context = context;
        }

        // GET: Livros
        public async Task<IActionResult> Index(string searchOption, string searchString)
        {
            var livros = from m in _context.Livro
                         select m;
            if (searchOption == "Nome")
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    livros = livros.Where(s => s.Nome_Livro.Contains(searchString));
                }
            }
            else if (searchOption == "Editora")
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    livros = livros.Where(s => s.Editora_Livro.Contains(searchString));
                }
            }
            else if (searchOption == "Genero")
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    livros = livros.Where(s => s.Genero_Livro.Contains(searchString));
                }
            }
            else if (searchOption == "Autor")
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    livros = livros.Where(s => s.Autor_Livro.Contains(searchString));
                }
            }
            return View(await livros.ToListAsync());
        }

        // GET: Livros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro
                .FirstOrDefaultAsync(m => m.Id_Livro == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // GET: Livros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Livro,Nome_Livro,Genero_Livro,Autor_Livro,Editora_Livro,Ano_Publicacao,Status_Livro")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(livro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(livro);
        }

        // GET: Livros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        // POST: Livros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Livro,Nome_Livro,Genero_Livro,Autor_Livro,Editora_Livro,Ano_Publicacao,Status_Livro")] Livro livro)
        {
            if (id != livro.Id_Livro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.Id_Livro))
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
            return View(livro);
        }

        // GET: Livros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro
                .FirstOrDefaultAsync(m => m.Id_Livro == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _context.Livro.FindAsync(id);
            _context.Livro.Remove(livro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivroExists(int id)
        {
            return _context.Livro.Any(e => e.Id_Livro == id);
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

                detalhes.DrawString("Gênero", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(175, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Autor", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(285, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Editora", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(395, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Ano de Publicação", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(485, alturaTituloDetalhesY, page.Width, page.Height));



                //dados do relatório 
                var alturaDetalhesItens = 260;
                var livros = await _context.Livro.ToListAsync();
                foreach (var a in livros)
                {
                    textFormatter.DrawString(a.Nome_Livro, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(20, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.Genero_Livro, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(165, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.Autor_Livro, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(275, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.Editora_Livro, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(385, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.Ano_Publicacao.ToString("dd/MM/yyyy"), fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(495, alturaDetalhesItens, page.Width, page.Height));
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
