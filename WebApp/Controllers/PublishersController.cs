using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.VideoGames;

namespace WebApp.Controllers;

public class PublishersController : Controller
{
    private readonly VideoGamesDbContext _context;

    public PublishersController(VideoGamesDbContext context)
    {
        _context = context;
    }

    // GET
    public async Task<IActionResult> Details(int id)
    {
        var regionSales = await _context.RegionSales
            .Include(rs => rs.GamePlatform)
            .ThenInclude(gp => gp.Platform) 
            .Include(rs => rs.Region)
            .Where(rs => rs.GamePlatform.GamePublisher.GameId == id)
            .ToListAsync();

        if (regionSales == null || !regionSales.Any())
        {
            return NotFound();
        }

        return View(regionSales);
    }

}
