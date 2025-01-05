namespace WebApp.Models.VideoGames;

public class VideoGamesViewModel
{
    public List<Game> Games { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}