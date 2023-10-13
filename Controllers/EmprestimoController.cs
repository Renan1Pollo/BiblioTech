using BiblioTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUsuario,IdLivro,DataRetirada,Devolvido")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var userExists = await _context.Usuarios.AnyAsync(u => u.Id == emprestimo.IdUsuario);
                    if (!userExists)
                    {
                        ModelState.AddModelError("IdUsuario", "O usuário selecionado não existe.");
                        return View(emprestimo);
                    }

                    var livro = await _context.Livros.FindAsync(emprestimo.IdLivro);
                    if (livro == null)
                    {
                        ModelState.AddModelError("IdLivro", "O livro selecionado não existe.");
                        return View(emprestimo);
                    }

                    if (livro.Quantidade <= 0)
                    {
                        ModelState.AddModelError("IdLivro", "O livro selecionado não está disponível.");
                        return View(emprestimo);
                    }

                    _context.Add(emprestimo);
                    livro.Quantidade--;
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                return View(emprestimo);

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
                var userExists = await _context.Usuarios.AnyAsync(u => u.Id == emprestimo.IdUsuario);

                if (!userExists)
                {
                    ModelState.AddModelError("IdUsuario", "O usuário selecionado não existe.");
                    return View(emprestimo);
                }

                var livroExists = await _context.Livros.AnyAsync(l => l.Id == emprestimo.IdLivro);

                if (!livroExists)
                {
                    ModelState.AddModelError("IdLivro", "O livro selecionado não existe.");
                    return View(emprestimo);
                }

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

                var livro = await _context.Livros.FindAsync(emprestimo.IdLivro);

                if (livro != null)
                {
                    livro.Quantidade++;
                }
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
