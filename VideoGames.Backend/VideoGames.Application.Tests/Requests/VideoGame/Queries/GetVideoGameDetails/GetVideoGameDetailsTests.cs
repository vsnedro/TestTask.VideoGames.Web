using System;
using System.Threading.Tasks;

using Shouldly;

using VideoGames.Application.Exceptions;
using VideoGames.Application.Requests.VideoGame.Queries.GetVideoGameDetails;

using Xunit;

namespace VideoGames.Application.Tests.Requests.VideoGame.Queries.GetVideoGameDetails;

public class GetVideoGameDetailsTests : BaseQueryHandlerTests
{
    private readonly GetVideoGameDetailsQueryHandler _handler;

    public GetVideoGameDetailsTests(BaseRequestTestsFixture fixture) : base(fixture)
    {
        _handler = new GetVideoGameDetailsQueryHandler(DbContext, MemoryCache);
    }

    [Fact]
    public async Task Handle_QueryIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        GetVideoGameDetailsQuery? query = null;

        // Act
        async Task action() => await _handler.Handle(query);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(action);
    }

    [Fact]
    public async Task Handle_DbContextIsEmpty_ThrowsEntityNotFoundException()
    {
        try
        {
            await RemoveVideoGamesAsync();
            var query = new GetVideoGameDetailsQuery();
            (query.Id) = (BaseVideoGameRequestTestsConstants.VideoGames.A.Id);

            async Task action() => await _handler.Handle(query);

            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }
        finally
        {
            await RemoveVideoGamesAsync();
        }
    }

    [Fact]
    public async Task Handle_QueryContainsWrongGameId_ThrowsEntityNotFoundException()
    {
        try
        {
            await AddVideoGamesAsync();
            var query = new GetVideoGameDetailsQuery();
            (query.Id) = (BaseVideoGameRequestTestsConstants.WrongVideoGameId);

            async Task action() => await _handler.Handle(query);

            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }
        finally
        {
            await RemoveVideoGamesAsync();
        }
    }

    [Fact]
    public async Task Handle_QueryContainsCorrectGameId_ReturnsGameDetailsVm()
    {
        try
        {
            await AddVideoGamesAsync();
            var query = new GetVideoGameDetailsQuery();
            (query.Id) = (BaseVideoGameRequestTestsConstants.VideoGames.A.Id);

            var result = await _handler.Handle(query);

            result.ShouldBeOfType<VideoGameDetailsVm>();
        }
        finally
        {
            await RemoveVideoGamesAsync();
        }
    }

    [Fact]
    public async Task Handle_QueryContainsCorrectGameId_ReturnsCorrectGameDetailsVm()
    {
        try
        {
            await AddVideoGamesAsync();
            var query = new GetVideoGameDetailsQuery();
            (query.Id) = (BaseVideoGameRequestTestsConstants.VideoGames.A.Id);

            var result = await _handler.Handle(query);

            result.Id.ShouldBe(query.Id);
            result.Name.ShouldBe(BaseVideoGameRequestTestsConstants.VideoGames.A.Name);
            result.ReleaseDate.Date.ShouldBe(BaseVideoGameRequestTestsConstants.VideoGames.A.ReleaseDate.Date);
            result.DeveloperStudioName.ShouldBe(BaseVideoGameRequestTestsConstants.VideoGames.A.DeveloperStudioName);
        }
        finally
        {
            await RemoveVideoGamesAsync();
        }
    }
}
