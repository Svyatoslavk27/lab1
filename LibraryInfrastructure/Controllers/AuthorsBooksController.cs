using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryDomain.Model;
using LibraryInfrastructure;

namespace LibraryInfrastructure.Controllers
{
    public class AuthorsBooksController : Controller
    {
        private readonly DblibraryContext _context;

        public AuthorsBooksController(DblibraryContext context)
        {
            _context = context;
        }

        // GET: AuthorsBooks
        public async Task<IActionResult> Index()
        {
            var dblibraryContext = _context.AuthorsBooks.Include(a => a.Author).Include(a => a.Book);
            return View(await dblibraryContext.ToListAsync());
        }

        // GET: AuthorsBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorsBook = await _context.AuthorsBooks
                .Include(a => a.Author)
                .Include(a => a.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorsBook == null)
            {
                return NotFound();
            }

            return View(authorsBook);
        }

        // GET: AuthorsBooks/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "FirstName");
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name");
            return View();
        }

        // POST: AuthorsBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,AuthorId,Id")] AuthorsBook authorsBook)
        {
            var author = _context.Authors.FirstOrDefault(g => g.Id == authorsBook.AuthorId);
            authorsBook.Author = author;
            var book = _context.Books.Include(b => b.Category).Where(b => b.Id == authorsBook.BookId).FirstOrDefault();
            authorsBook.Book = book;
            ModelState.Clear();
            TryValidateModel(authorsBook);
            if (ModelState.IsValid)
            {
                _context.Add(authorsBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "FirstName", authorsBook.AuthorId);
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", authorsBook.BookId);
            return View(authorsBook);
        }

        // GET: AuthorsBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorsBook = await _context.AuthorsBooks.FindAsync(id);
            if (authorsBook == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "FirstName", authorsBook.AuthorId);
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", authorsBook.BookId);
            return View(authorsBook);
        }

        // POST: AuthorsBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,AuthorId,Id")] AuthorsBook authorsBook)
        {
            if (id != authorsBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorsBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorsBookExists(authorsBook.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "FirstName", authorsBook.AuthorId);
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", authorsBook.BookId);
            return View(authorsBook);
        }

        // GET: AuthorsBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorsBook = await _context.AuthorsBooks
                .Include(a => a.Author)
                .Include(a => a.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorsBook == null)
            {
                return NotFound();
            }

            return View(authorsBook);
        }

        // POST: AuthorsBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorsBook = await _context.AuthorsBooks.FindAsync(id);
            if (authorsBook != null)
            {
                _context.AuthorsBooks.Remove(authorsBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorsBookExists(int id)
        {
            return _context.AuthorsBooks.Any(e => e.Id == id);
        }
    }
}
