namespace VideoGames.Domain.Entities;

public class VideoGameGenre
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<VideoGame> VideoGames { get; set; }
}
