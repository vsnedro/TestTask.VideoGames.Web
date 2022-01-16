namespace VideoGames.Application.Requests.VideoGame.Queries.GetVideoGameDetails;

public class VideoGameDetailsVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string DeveloperStudioName { get; set; }
    public IEnumerable<string> GenreNames { get; set; } = new List<string>();
}
