namespace VideoGames.Domain.Entities;

public class DeveloperStudio
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<VideoGame> VideoGames { get; set; }
}
