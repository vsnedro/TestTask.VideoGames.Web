using System.ComponentModel.DataAnnotations;

namespace VideoGames.WebApi.Models.VideoGame;

public class GetVideoGameListDto
{
    [Required]
    public int PageIndex { get; set; }

    [Required]
    public int PageSize { get; set; }

    public int? ReleaseYear { get; set; }

    public int? DeveloperStudioId { get; set; }

    public int? VideoGameGenreId { get; set; }
}
