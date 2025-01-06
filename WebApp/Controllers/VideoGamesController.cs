using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.VideoGames;
using Microsoft.AspNetCore.Authorization;
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
        
        var games = await _context.Games
            .Include(g => g.Genre)  
            .Include(g => g.GamePublishers)
            .ThenInclude(gp => gp.Publisher) 
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
    
    // GET przenosi do Add.cshtml
    [Authorize]
    public IActionResult Add()
    {
        ViewBag.Genres = new SelectList(_context.Genres, "Id", "GenreName");
        return View(new AddGameFormModel());
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Add(AddGameFormModel inputModel)
    {
        if (ModelState.IsValid)
        {
            // Find the maximum existing `id`
            var maxId = await _context.Games.MaxAsync(g => (int?)g.Id) ?? 0;

            var game = new Game
            {
                Id = maxId + 1, // Assign a new unique `id`
                GameName = inputModel.GameName,
                GenreId = inputModel.GenreId
            };

            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Genres = new SelectList(_context.Genres, "Id", "GenreName");
        return View(inputModel);
    }
}