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

                detalhes.DrawString("Nome", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(30, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("CPF", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(130, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Inscrição", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(180, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Faculdade", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(230, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Nº de Casos", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(290, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Data de Nascimento", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(350, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Data de Admissão", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(440, alturaTituloDetalhesY, page.Width, page.Height));



                //dados do relatório 
                var alturaDetalhesItens = 260;
                var advogados = await _context.Advogado.ToListAsync();
                foreach (var a in advogados)
                {
                    textFormatter.DrawString(a.Nome_Advogado, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(20, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.CPF_Advogado, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(120, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.Inscricao_Advogado, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(180, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.Instituicao_Advogado, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(230, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.Numero_casos.ToString(), fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(305, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.Data_de_nascimento.ToString("dd/MM/yyyy"), fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(360, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.DataAdmissao_Advogado.ToString("dd/MM/yyyy"), fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(440, alturaDetalhesItens, page.Width, page.Height));
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
