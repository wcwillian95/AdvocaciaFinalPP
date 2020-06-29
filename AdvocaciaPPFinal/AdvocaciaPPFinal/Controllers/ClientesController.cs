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
    public class ClientesController : Controller
    {
        private readonly AdvocaciaPPFinalContext _context;

        public ClientesController(AdvocaciaPPFinalContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index(string searchOption, string searchString)
        {
            var clientes = from m in _context.Cliente
                           select m;
            if (searchOption == "Nome")
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    clientes = clientes.Where(s => s.Nome_Cliente.Contains(searchString));
                }
            }
            else if (searchOption == "CPF")
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    clientes = clientes.Where(s => s.CPF_Cliente.Contains(searchString));
                }
            }
            else if (searchOption == "Cidade")
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    clientes = clientes.Where(s => s.Cidade.Contains(searchString));
                }
            }
            return View(await clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id_Cliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Cliente,Nome_Cliente,CPF_Cliente,DataNascimento_Cliente,Telefone_Cliente,Email_Cliente,CEP,Rua,Numero,Bairro,Cidade,Estado,DataCadastro_Cliente")] Cliente cliente)
        {
            cliente.DataCadastro_Cliente = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Cliente,Nome_Cliente,CPF_Cliente,DataNascimento_Cliente,Telefone_Cliente,Email_Cliente,CEP,Rua,Numero,Bairro,Cidade,Estado,DataCadastro_Cliente")] Cliente cliente)
        {
            if (id != cliente.Id_Cliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id_Cliente))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id_Cliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id_Cliente == id);
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

                detalhes.DrawString("Nome", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(67, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("CPF", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(200, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("Telefone", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(325, alturaTituloDetalhesY, page.Width, page.Height));

                detalhes.DrawString("E-mail", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(470, alturaTituloDetalhesY, page.Width, page.Height));



                //dados do relatório 
                var alturaDetalhesItens = 260;
                var clientes = await _context.Cliente.ToListAsync();
                foreach (var a in clientes)
                {
                    textFormatter.DrawString(a.Nome_Cliente, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(60, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.CPF_Cliente, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(190, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.Telefone_Cliente.ToString(), fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(325, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString(a.Email_Cliente, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(460, alturaDetalhesItens, page.Width, page.Height));
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
