namespace VideoGames.Domain.Entities;

public class VideoGame
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime ReleaseDate { get; set; }

    public DeveloperStudio DeveloperStudio { get; set; }

    public ICollection<VideoGameGenre> Genres { get; set; } = new List<VideoGameGenre>();
}
