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
    public class FuncionariosController : Controller
    {
        private readonly AdvocaciaPPFinalContext _context;

        public FuncionariosController(AdvocaciaPPFinalContext context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index(string searchOption, string searchString)
        {
            var funcionarios = from m in _context.Funcionario
                               select m;
            if (searchOption == "Nome")
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    funcionarios = funcionarios.Where(s => s.Nome_Funcionario.Contains(searchString));
                }
            }
            else if (searchOption == "CPF")
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    funcionarios = funcionarios.Where(s => s.CPF_Funcionario.Contains(searchString));
                }
            }
            else if (searchOption == "Cargo")
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    funcionarios = funcionarios.Where(s => s.Cargo_Funcionario.Contains(searchString));
                }
            }
            return View(await funcionarios.ToListAsync());
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .FirstOrDefaultAsync(m => m.Id_Funcionario == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Funcionario,Nome_Funcionario,CPF_Funcionario,DataNascimento_Funcionario,Cargo_Funcionario,CEP_Funcionario,Rua_Funcionario,Numero_Funcionario,Cidade_Funcionario,Bairro_Funcionario,DataCadastro_Funcionario")] Funcionario funcionario)
        {
            funcionario.DataCadastro_Funcionario = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Funcionario,Nome_Funcionario,CPF_Funcionario,DataNascimento_Funcionario,Cargo_Funcionario,CEP_Funcionario,Rua_Funcionario,Numero_Funcionario,Cidade_Funcionario,Bairro_Funcionario,DataCadastro_Funcionario")] Funcionario funcionario)
        {
            if (id != funcionario.Id_Funcionario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.Id_Funcionario))
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
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .FirstOrDefaultAsync(m => m.Id_Funcionario == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionario.FindAsync(id);
            _context.Funcionario.Remove(funcionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionario.Any(e => e.Id_Funcionario == id);
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

                detalhes.DrawString("Nome", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(70, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Cargo", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(190, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Data Nascimento", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(320, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("CPF", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(470, alturaTituloDetalhesY, page.Width, page.Height));



                //dados do relatório 
                var alturaDetalhesItens = 260;
                var funcionarios = await _context.Funcionario.ToListAsync();
                foreach (var a in funcionarios)
                {
                    textFormatter.DrawString(a.Nome_Funcionario, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(55, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.Cargo_Funcionario, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(190, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.DataNascimento_Funcionario.ToString("dd/MM/yyyy"), fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(325, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.CPF_Funcionario.ToString(), fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(460, alturaDetalhesItens, page.Width, page.Height));
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
