using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.VideoGames;
namespace WebApp.Controllers;

public class VideoGamesController : Controller
{
    private readonly VideoGamesDbContext _context;
    private const int PageSize = 20;
    
    public VideoGamesController(VideoGamesDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context)); 
    }
    
    // GET /VideoGames
    public async Task<IActionResult> Index(int page = 1)
    {
        var totalGamesCount = await _context.Games.CountAsync();
        var totalPages = (int)Math.Ceiling(totalGamesCount / (double)PageSize);

        // Include related entities properly, ensuring publishers are loaded
        var games = await _context.Games
            .Include(g => g.Genre)  // Include Genre if needed
            .Include(g => g.GamePublishers)
            .ThenInclude(gp => gp.Publisher)  // Include Publisher in the join table
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        var model = new VideoGamesViewModel
        {
            Games = games,
            CurrentPage = page,
            TotalPages = totalPages
        };

        return View(model);
    }

}