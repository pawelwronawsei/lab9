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

    // GET: Publishers/Details/5
    public async Task<IActionResult> Details(int id)
    {
        // Fetch RegionSales where the GamePlatform belongs to the given game ID
        var regionSales = await _context.RegionSales
            .Include(rs => rs.GamePlatform)
            .ThenInclude(gp => gp.Platform) // Include Platform details
            .Include(rs => rs.Region) // Include Region details
            .Where(rs => rs.GamePlatform.GamePublisher.GameId == id) // Filter by Game ID
            .ToListAsync();

        if (regionSales == null || !regionSales.Any())
        {
            return NotFound();
        }

        return View(regionSales);
    }

}
