using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using VideoGames.Application.Exceptions;
using VideoGames.Application.Requests.VideoGame.Commands.DeleteVideoGame;

using Xunit;

namespace VideoGames.Application.Tests.Requests.VideoGame.Commands.DeleteVideoGame;

public class DeleteVideoGameCommandHandlerTests : BaseCommandHandlerTests
{
    private readonly DeleteVideoGameCommandHandler _handler;

    public DeleteVideoGameCommandHandlerTests(BaseRequestTestsFixture fixture) : base(fixture)
    {
        _handler = new DeleteVideoGameCommandHandler(DbContext);
    }

    [Fact]
    public async Task Handle_RequestIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DeleteVideoGameCommand? command = null;

        // Act
        async Task action() => await _handler.Handle(command);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(action);
    }

    [Fact]
    public async Task Handle_DbContextIsEmpty_ThrowsEntityNotFoundException()
    {
        var command = new DeleteVideoGameCommand();
        (command.Id) = (BaseVideoGameRequestTestsConstants.VideoGames.A.Id);

        async Task action() => await _handler.Handle(command);

        await Assert.ThrowsAsync<EntityNotFoundException>(action);
    }

    [Fact]
    public async Task Handle_RequestContainsWrongGameId_ThrowsEntityNotFoundException()
    {
        try
        {
            await AddVideoGameAAsync();
            var command = new DeleteVideoGameCommand();
            (command.Id) = (BaseVideoGameRequestTestsConstants.WrongVideoGameId);

            async Task action() => await _handler.Handle(command);

            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }
        finally
        {
            await RemoveVideoGamesAsync();
        }
    }

    [Fact]
    public async Task Handle_RequestContainsCorrectGameId_DbContextNotContainsDeletedEntity()
    {
        try
        {
            await AddVideoGameAAsync();
            var command = new DeleteVideoGameCommand();
            (command.Id) = (BaseVideoGameRequestTestsConstants.VideoGames.A.Id);

            await _handler.Handle(command);

            Assert.Null(await DbContext.VideoGames.SingleOrDefaultAsync(
                x => x.Id == BaseVideoGameRequestTestsConstants.VideoGames.A.Id));
        }
        finally
        {
            await RemoveVideoGamesAsync();
        }
    }
}
