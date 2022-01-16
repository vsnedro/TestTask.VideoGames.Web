using System.ComponentModel.DataAnnotations;

namespace VideoGames.WebApi.Models.VideoGame;

public class CreateVideoGameDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public DateTime ReleaseDate { get; set; }

    [Required]
    public int DeveloperStudioId { get; set; }

    [Required]
    public IEnumerable<int> GenresId { get; set; } = new List<int>();
}
