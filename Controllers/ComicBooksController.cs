using ComicSystem.Data;
using ComicSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers;


public class ComicBooksController : Controller
{
    private readonly ComicSystemContext _context;

    public ComicBooksController(ComicSystemContext context)
    {
        _context = context;
    }


    public async Task<IActionResult> Index()
    {
        return View(await _context.ComicBooks.ToListAsync());
    }

    private IActionResult View(object toListAsync) => throw new NotImplementedException();

    
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Author,PricePerDay")] ComicBook comicBook)
    {
        if (ModelState.IsValid)
        {
            _context.Add(comicBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(comicBook);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.ComicBooks == null)
        {
            return Problem("Entity set 'ComicSystemContext.ComicBooks'  is null.");
        }

        var comicBook = await _context.ComicBooks.FindAsync(id);
        if (comicBook != null)
        {
            _context.ComicBooks.Remove(comicBook);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
}