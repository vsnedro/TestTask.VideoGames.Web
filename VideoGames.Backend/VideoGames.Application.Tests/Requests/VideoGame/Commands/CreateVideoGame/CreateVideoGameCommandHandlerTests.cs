using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using VideoGames.Application.Requests.VideoGame.Commands.CreateVideoGame;

using Xunit;

namespace VideoGames.Application.Tests.Requests.VideoGame.Commands.CreateVideoGame;

public class CreateVideoGameCommandHandlerTests : BaseCommandHandlerTests
{
    private readonly CreateVideoGameCommandHandler _handler;

    public CreateVideoGameCommandHandlerTests(BaseRequestTestsFixture fixture) : base(fixture)
    {
        _handler = new CreateVideoGameCommandHandler(DbContext);
    }

    [Fact]
    public async Task Handle_RequestIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        CreateVideoGameCommand? command = null;

        // Act
        async Task action() => await _handler.Handle(command);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(action);
    }

    [Fact]
    public async Task Handle_RequestContainsNewVideoGame_DbContextContainsSingleEntity()
    {
        try
        {
            var command = new CreateVideoGameCommand()
            {
                Name = BaseVideoGameRequestTestsConstants.VideoGames.A.Name,
                ReleaseDate = BaseVideoGameRequestTestsConstants.VideoGames.A.ReleaseDate,
                DeveloperStudioId = BaseVideoGameRequestTestsConstants.VideoGames.A.DeveloperStudioId,
                GenreIds = BaseVideoGameRequestTestsConstants.VideoGames.A.GenreIds,
            };

            await _handler.Handle(command);

            //DbContext.VideoGames.Count().ShouldBe(1);
            Assert.Single(DbContext.VideoGames);
        }
        finally
        {
            await RemoveVideoGamesAsync();
        }
    }

    [Fact]
    public async Task Handle_RequestContainsNewVideoGame_DbContextContainsInsertedEntity()
    {
        try
        {
            var command = new CreateVideoGameCommand()
            {
                Name = BaseVideoGameRequestTestsConstants.VideoGames.A.Name,
                ReleaseDate = BaseVideoGameRequestTestsConstants.VideoGames.A.ReleaseDate,
                DeveloperStudioId = BaseVideoGameRequestTestsConstants.VideoGames.A.DeveloperStudioId,
                GenreIds = BaseVideoGameRequestTestsConstants.VideoGames.A.GenreIds,
            };

            var gameId = await _handler.Handle(command);

            Assert.NotNull(await DbContext.VideoGames.SingleOrDefaultAsync(
                x => x.Id == gameId &&
                x.Name == BaseVideoGameRequestTestsConstants.VideoGames.A.Name &&
                x.ReleaseDate.Date == BaseVideoGameRequestTestsConstants.VideoGames.A.ReleaseDate.Date &&
                x.DeveloperStudio.Id == BaseVideoGameRequestTestsConstants.VideoGames.A.DeveloperStudioId &&
                x.Genres.Any(x => BaseVideoGameRequestTestsConstants.VideoGames.A.GenreIds.Contains(x.Id))));
        }
        finally
        {
            await RemoveVideoGamesAsync();
        }
    }
}
