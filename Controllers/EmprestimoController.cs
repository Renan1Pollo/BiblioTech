using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BiblioTech.Models;

namespace BiblioTech.Controllers
{
    public class EmprestimoController : Controller
    {
        private readonly Contexto _context;

        public EmprestimoController(Contexto context)
        {
            _context = context;
        }

        // GET: Emprestimo
        public async Task<IActionResult> Index()
        {
              return _context.Emprestimos != null ? 
                          View(await _context.Emprestimos.ToListAsync()) :
                          Problem("Entity set 'Contexto.Emprestimos'  is null.");
        }

        // GET: Emprestimo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Emprestimos == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        // GET: Emprestimo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Emprestimo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUsuario,IdLivro,DataRetirada,Devolvido")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emprestimo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emprestimo);
        }

        // GET: Emprestimo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Emprestimos == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimos.FindAsync(id);
            if (emprestimo == null)
            {
                return NotFound();
            }
            return View(emprestimo);
        }

        // POST: Emprestimo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUsuario,IdLivro,DataRetirada,Devolvido")] Emprestimo emprestimo)
        {
            if (id != emprestimo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emprestimo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmprestimoExists(emprestimo.Id))
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
            return View(emprestimo);
        }

        // GET: Emprestimo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Emprestimos == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        // POST: Emprestimo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Emprestimos == null)
            {
                return Problem("Entity set 'Contexto.Emprestimos'  is null.");
            }
            var emprestimo = await _context.Emprestimos.FindAsync(id);
            if (emprestimo != null)
            {
                _context.Emprestimos.Remove(emprestimo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmprestimoExists(int id)
        {
          return (_context.Emprestimos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
