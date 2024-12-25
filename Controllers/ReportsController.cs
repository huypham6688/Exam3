using ComicSystem.Data;
using Microsoft.AspNetCore.Mvc;
using ComicSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers;

public class ReportsController : Controller
{
    private readonly ComicSystemContext _context;

    public ReportsController(ComicSystemContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> RentalReport(DateTime? startDate, DateTime? endDate)
    {
        
        var query = from rd in _context.RentalDetails
            join r in _context.Rentals on rd.RentalID equals r.RentalID
            join c in _context.Customers on r.CustomerID equals c.CustomerID
            join b in _context.ComicBooks on rd.ComicBookID equals b.ComicBookID
            where (!startDate.HasValue || r.RentalDate >= startDate) &&
                  (!endDate.HasValue || r.ReturnDate <= endDate)
            select new RentalReportViewModel
            {
                BookName = b.Title,
                RentalDate = r.RentalDate,
                ReturnDate = r.ReturnDate,
                CustomerName = c.FullName,
                Quantity = rd.Quantity
            };

        return View(await query.ToListAsync());
    }
}