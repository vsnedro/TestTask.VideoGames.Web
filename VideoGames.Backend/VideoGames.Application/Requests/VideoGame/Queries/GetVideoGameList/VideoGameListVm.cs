namespace VideoGames.Application.Requests.VideoGame.Queries.GetVideoGameList;

public class VideoGameListVm
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public int TotalPages { get; set; }

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;

    public IEnumerable<VideoGameLookupDto> VideoGames { get; set; }
}
