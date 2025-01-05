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
        var publisher = await _context.Publishers
            .FirstOrDefaultAsync(p => p.Id == id);

        if (publisher == null)
        {
            return NotFound();
        }

        return View(publisher);
    }
}
