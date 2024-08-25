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
    public class ReadersBooksController : Controller
    {
        private readonly DblibraryContext _context;

        public ReadersBooksController(DblibraryContext context)
        {
            _context = context;
        }

        // GET: ReadersBooks
        public async Task<IActionResult> Index()
        {
            var dblibraryContext = _context.ReadersBooks.Include(r => r.Book).Include(r => r.Reader).Include(r => r.Status);
            return View(await dblibraryContext.ToListAsync());
        }

        // GET: ReadersBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var readersBook = await _context.ReadersBooks
                .Include(r => r.Book)
                .Include(r => r.Reader)
                .Include(r => r.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (readersBook == null)
            {
                return NotFound();
            }

            return View(readersBook);
        }

        // GET: ReadersBooks/Create
        public IActionResult Create()
        {

            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name");
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "Username");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name");
            return View();
        }

        // POST: ReadersBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReaderId,BookId,Issue,PlanReturn,StatusId,FactReturn")] ReadersBook readersBook)
        {
            var reader = _context.Readers.FirstOrDefault(g => g.Id == readersBook.ReaderId);
            readersBook.Reader = reader;
            var book = _context.Books.Include(b => b.Category).Where(b => b.Id == readersBook.BookId).FirstOrDefault();
            readersBook.Book = book;
            var status = _context.Statuses.FirstOrDefault(g => g.Id == readersBook.StatusId);
            readersBook.Status = status;
            ModelState.Clear();
            TryValidateModel(readersBook);
            if (ModelState.IsValid)
            {
                _context.Add(readersBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", readersBook.BookId);
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "Username", readersBook.ReaderId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", readersBook.StatusId);
            return View(readersBook);
        }

        // GET: ReadersBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var readersBook = await _context.ReadersBooks.FindAsync(id);
            if (readersBook == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", readersBook.BookId);
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "Username", readersBook.ReaderId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", readersBook.StatusId);
            return View(readersBook);
        }

        // POST: ReadersBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReaderId,BookId,Issue,PlanReturn,StatusId,FactReturn")] ReadersBook readersBook)
        {
            if (id != readersBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(readersBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReadersBookExists(readersBook.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", readersBook.BookId);
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "Username", readersBook.ReaderId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", readersBook.StatusId);
            return View(readersBook);
        }

        // GET: ReadersBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var readersBook = await _context.ReadersBooks
                .Include(r => r.Book)
                .Include(r => r.Reader)
                .Include(r => r.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (readersBook == null)
            {
                return NotFound();
            }

            return View(readersBook);
        }

        // POST: ReadersBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var readersBook = await _context.ReadersBooks.FindAsync(id);
            if (readersBook != null)
            {
                _context.ReadersBooks.Remove(readersBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReadersBookExists(int id)
        {
            return _context.ReadersBooks.Any(e => e.Id == id);
        }
    }
}
