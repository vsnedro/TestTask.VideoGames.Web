using System.ComponentModel.DataAnnotations;

namespace VideoGames.WebApi.Models.VideoGame;

public class UpdateVideoGameDto
{
    [Required]
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime ReleaseDate { get; set; }

    public int DeveloperStudioId { get; set; }

    public IEnumerable<int> GenresId { get; set; } = new List<int>();
}
