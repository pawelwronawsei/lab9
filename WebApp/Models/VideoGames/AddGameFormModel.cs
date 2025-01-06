using System.ComponentModel.DataAnnotations;
namespace WebApp.Models.VideoGames;

public class AddGameFormModel
{
    [Required(ErrorMessage = "Wpisz nazwę gry.")]
    public string GameName { get; set; }

    [Required(ErrorMessage = "Wybierz gatunek gry.")]
    public int GenreId { get; set; }
}