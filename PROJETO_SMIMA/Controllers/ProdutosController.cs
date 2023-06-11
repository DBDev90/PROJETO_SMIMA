using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROJETO_SMIMA;
using PROJETO_SMIMA.Entidades;

namespace PROJETO_SMIMA.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly Contexto _context;
        private string path;

        public ProdutosController(Contexto context, IWebHostEnvironment server)
        {
            _context = context;
            path = server.WebRootPath+"\\uploads\\";
        }

        public IActionResult IndexProduto()
        {
            return View();
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
              return _context.PRODUTOS != null ? 
                          View(await _context.PRODUTOS.ToListAsync()) :
                          Problem("Entity set 'Contexto.PRODUTOS'  is null.");
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PRODUTOS == null)
            {
                return NotFound();
            }

            var produto = await _context.PRODUTOS
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }


        [Authorize(Roles ="Administrador")]
        public IActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,Valor,Imagem")] Produto produto, IFormFile imagem)
        {
           
                if(imagem.Length > 0)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using(var stream = System.IO.File.Create(path + imagem.FileName))
                    {
                        await imagem.CopyToAsync(stream);
                    }
                }
                produto.Imagem = imagem.FileName;

                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           
           
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PRODUTOS == null)
            {
                return NotFound();
            }

            var produto = await _context.PRODUTOS.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,Valor,Imagem")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
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
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PRODUTOS == null)
            {
                return NotFound();
            }

            var produto = await _context.PRODUTOS
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PRODUTOS == null)
            {
                return Problem("Entity set 'Contexto.PRODUTOS'  is null.");
            }
            var produto = await _context.PRODUTOS.FindAsync(id);
            if (produto != null)
            {
                _context.PRODUTOS.Remove(produto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
          return (_context.PRODUTOS?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
