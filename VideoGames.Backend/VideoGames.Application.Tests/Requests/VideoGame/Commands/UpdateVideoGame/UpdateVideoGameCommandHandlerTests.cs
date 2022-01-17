using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using VideoGames.Application.Exceptions;
using VideoGames.Application.Requests.VideoGame.Commands.UpdateVideoGame;

using Xunit;

namespace VideoGames.Application.Tests.Requests.VideoGame.Commands.UpdateVideoGame;

public class UpdateVideoGameCommandHandlerTests : BaseCommandHandlerTests
{
    private readonly UpdateVideoGameCommandHandler _handler;

    private const string GameAUpdatedName = "VideoGame A Updated Name";
    private static readonly DateTime GameAUpdatedReleaseDate = DateTime.Parse("01/01/1900");
    private const int GameAUpdatedDeveloperStudioId = BaseRequestTestsConstants.DeveloperStudios.B.Id;

    public UpdateVideoGameCommandHandlerTests(BaseRequestTestsFixture fixture) : base(fixture)
    {
        _handler = new UpdateVideoGameCommandHandler(DbContext);
    }

    [Fact]
    public async Task Handle_RequestIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        UpdateVideoGameCommand? request = null;

        // Act
        async Task action() => await _handler.Handle(request);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(action);
    }

    [Fact]
    public async Task Handle_DbContextIsEmpty_ThrowsEntityNotFoundException()
    {
        try
        {
            var command = new UpdateVideoGameCommand();
            (command.Id, command.Name, command.ReleaseDate, command.DeveloperStudioId, command.GenreIds) =
                (BaseVideoGameRequestTestsConstants.VideoGames.A.Id,
                GameAUpdatedName, GameAUpdatedReleaseDate, GameAUpdatedDeveloperStudioId,
                BaseVideoGameRequestTestsConstants.VideoGames.A.GenreIds);

            async Task action() => await _handler.Handle(command);

            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }
        finally
        {
            await RemoveVideoGamesAsync();
        }
    }

    [Fact]
    public async Task Handle_RequestContainsWrongVideoGameId_ThrowsEntityNotFoundException()
    {
        try
        {
            await AddVideoGameAAsync();
            var command = new UpdateVideoGameCommand();
            (command.Id, command.Name, command.ReleaseDate, command.DeveloperStudioId, command.GenreIds) =
                (BaseVideoGameRequestTestsConstants.WrongVideoGameId,
                GameAUpdatedName, GameAUpdatedReleaseDate, GameAUpdatedDeveloperStudioId,
                BaseVideoGameRequestTestsConstants.VideoGames.A.GenreIds);

            async Task action() => await _handler.Handle(command);

            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }
        finally
        {
            await RemoveVideoGamesAsync();
        }
    }

    [Fact]
    public async Task Handle_RequestContainsUpdatedVideoGame_DbContextContainsUpdatedEntity()
    {
        try
        {
            await AddVideoGameAAsync();
            var command = new UpdateVideoGameCommand();
            (command.Id, command.Name, command.ReleaseDate, command.DeveloperStudioId, command.GenreIds) =
                (BaseVideoGameRequestTestsConstants.VideoGames.A.Id,
                GameAUpdatedName, GameAUpdatedReleaseDate, GameAUpdatedDeveloperStudioId,
                BaseVideoGameRequestTestsConstants.VideoGames.A.GenreIds);

            await _handler.Handle(command);

            Assert.NotNull(await DbContext.VideoGames.SingleOrDefaultAsync(
                x => x.Id == BaseVideoGameRequestTestsConstants.VideoGames.A.Id &&
                x.Name == GameAUpdatedName &&
                x.ReleaseDate.Date == GameAUpdatedReleaseDate.Date &&
                x.DeveloperStudio.Id == GameAUpdatedDeveloperStudioId));
        }
        finally
        {
            await RemoveVideoGamesAsync();
        }
    }
}
